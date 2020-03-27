
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
    /// 【前工序校验】
    /// </summary>
    public class PRC : CommandBase<MesSession, MesRequestInfo>
    {
        private string EmployeeName = "前工序校验";
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            ExecutePRC(session, requestInfo.TData);
        }

        #region 【前工序校验】方法

        /// <summary>
        /// 前工序校验
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void ExecutePRC(MesSession _session, TransData _transData)
        {
            //1、参数校验---equipmentID---------------------------------
            string equipmentID = string.Empty;
            if (!EmployeeComm.CheckEquipmentID(_session, _transData, out equipmentID, EmployeeName))
                return;

            if (!GlobalData.IsDebug)
            {
                //3、执行前工序校验
                if (!CheckRouteOnlyCheck(_session, _transData, equipmentID))
                    return;

                //4、执行报工
                EmployeeComm.WorkingEfficiency(_session, _transData, equipmentID, EmployeeName, "103IN");
            }

            //5、API执行成功  ---------------------------------           
            EmployeeComm.SendMsg(_session, _transData, CheckResult.OK);

        }

        /// <summary>
        /// 前工序校验
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        private bool CheckRouteOnlyCheck(MesSession _session, TransData _transData, string equipmentID)
        {
            string actionParam = $"{_transData.SN},空（忽略）,{equipmentID},空（忽略）,PASS,1";

            // 判断API是否执行超时 ---------------------------------
            DataTable dtR;
            bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM_CheckRouteVV4(_transData.SN, "", equipmentID, "PASS", "1"); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, EmployeeName);
            return bRet;
        }

        #endregion



    }
}
