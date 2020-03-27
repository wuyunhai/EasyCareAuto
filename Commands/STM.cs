
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
    /// 【料箱与栈位关联关系校验】
    /// </summary>
    public class STM : CommandBase<MesSession, MesRequestInfo>
    {
        private string EmployeeName = "料箱与栈位关联关系校验";
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            ExecuteSTM(session, requestInfo.TData);
        }

        #region 【料箱与栈位关联关系校验】方法

        /// <summary>
        /// 料箱与栈位关联关系校验
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void ExecuteSTM(MesSession _session, TransData _transData)
        {
            //1、参数校验---equipmentID---------------------------------
            string equipmentID = string.Empty;
            if (!EmployeeComm.CheckEquipmentID(_session, _transData, out equipmentID, EmployeeName))
                return;

            //2、参数校验---ProcessData = 2项------------------------------
            List<string> lstProcessData = new List<string>();
            if (!EmployeeComm.CheckProcessData(_session, _transData, out lstProcessData, EmployeeName, Comparator.Equal, 2))
                return;

            if (!GlobalData.IsDebug)
            {
                //5、执行过站 ---------------------------------
                if (!ExecuteCheckLink(_session, _transData, equipmentID, lstProcessData))
                    return;
            }

            //6、API执行成功  ---------------------------------     
            EmployeeComm.SendMsg(_session, _transData, CheckResult.OK);
        }

        /// <summary>
        /// 执行料箱与栈位条码验证（1、获取料栈信息，2、验证料箱）
        /// </summary>
        /// <returns></returns>
        private bool ExecuteCheckLink(MesSession _session, TransData _transData, string equipmentID, List<string> lstProcessData)
        {
            //1、获取料栈信息
            string actionParam = $"{lstProcessData[0]},{_transData.SN},{lstProcessData[1]},{equipmentID}";
            DataTable dtR;
            bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM_BoxCheck(lstProcessData[0], _transData.SN, lstProcessData[1], equipmentID); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, EmployeeName);
            return bRet;
        }

        #endregion

    }
}
