
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    /// <summary>
    /// 【通用数据采集】
    /// </summary>
    public class NDQ : CommandBase<MesSession, MesRequestInfo>
    {
        private string EmployeeName = "通用数据采集";
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            ExecuteNDQ(session, requestInfo.TData);
        }

        #region 【通用数据采集】方法

        /// <summary>
        /// 通用数据采集
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void ExecuteNDQ(MesSession _session, TransData _transData)
        {
            //1、参数校验---equipmentID---------------------------------
            string equipmentID = string.Empty;
            if (!EmployeeComm.CheckEquipmentID(_session, _transData, out equipmentID, EmployeeName))
                return;

            //2、参数校验---ProcessData > 2项------------------------------
            List<string> lstProcessData = new List<string>();
            if (!EmployeeComm.CheckProcessData(_session, _transData, out lstProcessData, EmployeeName, Comparator.MoreThan, 2))
                return;

            if (!GlobalData.IsDebug)
            {
                if ((lstProcessData[0]) == "02")
                {
                    string nEmployeeName = $"气密性测试数据采集-通用过站";

                    //2.1、参数校验---apiStatus
                    string apiStatus = string.Empty;
                    if (!EmployeeComm.CheckApiStatus(_session, _transData, out apiStatus, nEmployeeName))
                        return;

                    //2.2、【气密性测试数据采集】需要提前执行【通过过站】操作 //不需要传制令单号
                    if (!EmployeeComm.CheckRoute(_session, _transData, "", equipmentID, apiStatus, nEmployeeName))
                        return;

                    //2.3、队列方式进行报工(如果记录了不良则无需进行第二次报工，因为记录不良时已经进行第一次报工了)
                    if (apiStatus != "FAIL")
                        GlobalData.queueServerNDQ.EnqueueItem(new TestItemFlex(_session, _transData, equipmentID, nEmployeeName, "103OUT")); 
                }

                //3、采集数据参数校验
                if (!CheckNdqParam(_session, _transData, equipmentID, lstProcessData))
                    return;
            }

            //4、API执行成功  ---------------------------------
            EmployeeComm.SendMsg(_session, _transData, CheckResult.OK);

            //5、队列方式保存采集数据
            GlobalData.queueServerNDQ.EnqueueItem(new TestItemFlex(_transData.SN, _transData.DeviceCode, equipmentID, lstProcessData, _session));
        }

        private bool CheckNdqParam(MesSession _session, TransData _transData, string equipmentNumber, List<string> lstProcessData)
        {
            LogInfo log;

            #region 参数校验[螺丝/气密性/预热/固话/点胶]及包含参数是否正确

            string ndqType = string.Empty;
            bool paramCheck = true;
            int paramCount = GetNDQParamCount(lstProcessData, out ndqType); //确认采集类别及参数数量 
            if (lstProcessData.Count != paramCount)
                paramCheck = false;

            string mEmployeeName = $"{EmployeeName}-{ndqType}";
            if (!paramCheck)
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{mEmployeeName}】接口失败>> 协议错误，请检查协议中是否至少包含采集类别码，业务数据（{paramCount}项）,执行参数：{_transData.ProcessData}。");
                _transData.ProcessData = $"Protocol error, procedure data should be {paramCount} items, current is {lstProcessData.Count} items, procedure data: {_transData.ProcessData}";
                EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            #endregion

            return true;
        }

        private static int GetNDQParamCount(List<string> lstProcessData, out string ndqType)
        {
            int paramCount = 0;
            switch (lstProcessData[0])
            {
                case "01":
                    paramCount = 6;
                    ndqType = "螺丝数据采集";
                    break;

                case "02":
                    paramCount = 3;
                    ndqType = "气密性测试数据采集";
                    break;

                case "03":
                    paramCount = 5;
                    ndqType = "预热炉温度数据采集";
                    break;

                case "04":
                    paramCount = 5;
                    ndqType = "固化炉温度数据采集";
                    break;

                case "05":
                    paramCount = 5;
                    ndqType = "点胶胶量数据采集";
                    break;

                default:
                    paramCount = 999999;
                    ndqType = "未知类别数据采集";
                    break;
            }

            return paramCount;
        }

        #endregion

    }
}
