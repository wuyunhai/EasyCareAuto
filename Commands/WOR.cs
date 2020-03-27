
using LabelManager2;
using MES.SocketService.BLL;
using MES.SocketService.Entity;
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
using YUN.Framework.ControlUtil;

namespace MES.SocketService
{
    /// <summary>
    /// 【工单余量请求】
    /// </summary>
    public class WOR : CommandBase<MesSession, MesRequestInfo>
    {
        private string EmployeeName = "工单余量请求";
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            ExecuteWOR(session, requestInfo.TData);
        }

        #region 【工单余量请求】方法

        /// <summary>
        /// 工单余量请求
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void ExecuteWOR(MesSession _session, TransData _transData)
        {
            //1、参数校验---EquipmentNumber---------------------------------
            //string equipmentID = string.Empty;
            //if (!EmployeeComm.CheckEquipmentID(_session, _transData, out equipmentID, EmployeeName))
            //    return;

            //2、参数校验---WorkOrder---------------------------------  
            //string workOrder = string.Empty;
            //if (!EmployeeComm.CheckWorkOrder(_session, _transData, out workOrder, EmployeeName, DataFrom.DB))
            //    return;

            //5、执行获取工单余量
            if (!ExecuteResidueByWO(_session, _transData))
                return;

            //6、API执行成功  ---------------------------------  
            EmployeeComm.SendMsg(_session, _transData, CheckResult.OK);

        }
        /// <summary>
        /// 执行获取工单余量 (成功返回制令单号)
        /// </summary>
        /// <returns></returns>
        private bool ExecuteResidueByWO(MesSession _session, TransData _transData)
        {
            // 判断API是否执行超时 ---------------------------------
            DataTable dtR;

            if (!GlobalData.IsDebug)
            {
                bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return DBHelper.GetWorkOrderRemain(); }, out dtR, GlobalData.ApiTimeout, _session, _transData, "空", EmployeeName);
                if (!bRet)
                {
                    new LogInfo(_session, LogLevel.Error, $"工单剩余数量为0，请重新开工单。");
                    return false;
                }
            }
            else
            {
                dtR = GetResidueByWO_Debug();
            }

            //首次获取工单
            string workOrder = DataTableHelper.GetCellValueinDT(dtR, 0, "ReturnMsg");
            _transData.ProcessData = $"{DataTableHelper.GetCellValueinDT(dtR, 0, "CheckStatus")}---{workOrder}";


            META_ParameterInfo info2 = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='WorkOrder'");
            info2.Value = workOrder;
            BLLFactory<META_Parameter>.Instance.Update(info2, info2.ID);
            LogInfo log = new LogInfo(_session, LogLevel.Info, $"工单：{workOrder}【已缓存】。");


            return true;
        }

        /// <summary>
        /// 模拟获取工单余量
        /// </summary>
        /// <param name="wo"></param>
        /// <returns></returns>
        private DataTable GetResidueByWO_Debug()
        {
            DataTable dt = DataTableHelper.CreateTable("CheckStatus,ReturnMsg");
            dt.Rows.Add("6543", $"SWO{System.DateTime.Now.ToStandardString()}");
            return dt;
        }

        #endregion



    }
}
