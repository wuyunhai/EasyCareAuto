
using LabelManager2;
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
    /// 【工单余量请求】
    /// </summary>
    public class WOR : CommandBase<MesSession, MesRequestInfo>
    {
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            CheckRoute(session, requestInfo.TData);
        }

        #region 【工单余量请求】方法

        /// <summary>
        /// 工单余量请求
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void CheckRoute(MesSession _session, TransData _transData)
        {
            LogInfo log = null;

            //1、获取EquipmentNumber 并校验---------------------------------
            AppConfig cfg = new AppConfig();
            string EquipmentNumber = cfg.AppConfigGet(_transData.DeviceCode + "_" + _transData.OpeIndex);
            if (string.IsNullOrEmpty(EquipmentNumber))
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【工单余量请求】接口失败>> 参数错误，根据机台编码[{1}]与操作序号[{2}]获取设备编码失败，请检查配置文件中的机台映射关系是否正确。", _transData.SerialNumber, _transData.DeviceCode, _transData.OpeIndex));
                SendMsg(_session, _transData);
                return;
            }

            //4、API执行是否超时 ---------------------------------
            //DataTable dt = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return Dm_Interface.SFC_DM__ProvideBlockID_ByWO(WorkOrder); }, GlobalData.ApiTimeout);
            DataTable dt = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return GetResidueByWO(); }, GlobalData.ApiTimeout);
            if (dt == null)
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【工单余量请求】接口超时（>{1}ms），执行参数：{0}。", EquipmentNumber, GlobalData.ApiTimeout));
                SendMsg(_session, _transData);
                return;
            }

            //5、判断API是否执行成功 ---------------------------------
            string checkStatus = DataTableHelper.GetCellValueinDT(dt, 0, "CheckStatus");
            if (checkStatus == "-1")
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【工单余量请求】接口失败>> 接口返回：{1}。", EquipmentNumber, checkStatus));
                SendMsg(_session, _transData);
                return;
            }

            //6、API执行成功  ---------------------------------                             
            _transData.Status = CheckResult.OK.ToString();
            _transData.ProcessData = checkStatus;
            log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【工单余量请求】接口成功>> 接口返回：{1}。", EquipmentNumber, checkStatus));
            SendMsg(_session, _transData);
             
        }


        /// <summary>
        /// 获取工单剩余数量
        /// </summary>
        /// <param name="wo"></param>
        /// <returns></returns>
        private DataTable GetResidueByWO()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CheckStatus");
            dt.Rows.Add("254");

            Thread.Sleep(1500);
            return dt;
        }

        private static void SendMsg(MesSession _session, TransData _transData)
        {
            // 发送【工单余量请求】执行结果至PLC
            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);
            LogInfo log = new SocketService.LogInfo(_session, LogLevel.Info, GlobalData.Pre_Send + msg);
        }

        #endregion



    }
}
