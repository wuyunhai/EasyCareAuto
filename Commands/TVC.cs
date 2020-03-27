
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    /// <summary>
    /// 【检验工序过站（值校验）】
    /// </summary>
    public class TVC : CommandBase<MesSession, MesRequestInfo>
    {
        private string EmployeeName = "检验工序过站（值校验）";
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            ExecuteTVC(session, requestInfo.TData);
        }

        #region 【检验工序过站（值类型）】方法

        /// <summary>
        /// 检验工序过站（值类型）
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void ExecuteTVC(MesSession _session, TransData _transData)
        {
            LogInfo log = null;

            //1、参数校验---EquipmentNumber---------------------------------
            string equipmentID = string.Empty;
            if (!EmployeeComm.CheckEquipmentID(_session, _transData, out equipmentID, EmployeeName))
                return;

            //2、参数校验---WorkOrder---------------------------------  
            string workOrder = string.Empty;
            if (!EmployeeComm.CheckWorkOrder(_session, _transData, out workOrder, EmployeeName, DataFrom.SQLite))
                return;

            //3、参数校验---获取待检验数据并校验有效性 ---------------------------------
            decimal weight = ConvertHelper.ToDecimal(_transData.ProcessData, 0);
            if (weight <= 0)
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{EmployeeName}】之称重检验接口失败>> 协议错误，请检查协议中过程数据是否为有效值（数值需大于0）,当前过程数据：{_transData.ProcessData}。");
                EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return;
            }

            if (!GlobalData.IsDebug)
            {
                //4、执行称重校验 ---------------------------------
                string apiStatus = string.Empty;
                CheckWeight(_session, _transData, weight, out apiStatus);

                //5、执行过站
                if (!EmployeeComm.CheckRoute(_session, _transData, workOrder, equipmentID, apiStatus, EmployeeName))
                    return;

                //5.1、队列方式进行报工(如果记录了不良则无需进行第二次报工，因为记录不良时已经进行第一次报工了)
                if (apiStatus != "FAIL")
                    GlobalData.queueServerNDQ.EnqueueItem(new TestItemFlex(_session, _transData, equipmentID, EmployeeName, "103OUT"));
            }
            //6、API执行成功  ---------------------------------          
            EmployeeComm.SendMsg(_session, _transData, CheckResult.OK);
        }

        /// <summary>
        /// 执行称重校验
        /// </summary>
        /// <returns></returns>
        private bool CheckWeight(MesSession _session, TransData _transData, decimal weight, out string apiStatus)
        {
            apiStatus = "PASS";
            string actionParam = $"{_transData.SN},{weight}";
            string mEmployeeName = $"{EmployeeName}-称重检验";

            // 判断API是否执行超时 ---------------------------------
            DataTable dtR;
            bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM_CheckWeight(_transData.SN, weight.ToString()); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, mEmployeeName, false);
            if (!bRet)
                apiStatus = "FAIL";

            return bRet;
        }

        /// <summary>
        /// 执行过站
        /// </summary>
        /// <returns></returns>
        private bool CheckRoute(MesSession _session, TransData _transData, string workOrder, string equipmentID, string apiStatus)
        {
            string actionParam = $"{_transData.SN},{equipmentID},{workOrder},{apiStatus}";
            string mEmployeeName = $"{EmployeeName}-通用过站";

            // 判断API是否执行超时 --------------------------------- 
            DataTable dtR;
            bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM_CheckRoute(_transData.SN, equipmentID, workOrder, apiStatus); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, mEmployeeName);
            return bRet;
        }

        #endregion



    }
}
