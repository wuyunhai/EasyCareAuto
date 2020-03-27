
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
    /// 【装配工序校验】
    /// </summary>
    public class BRC : CommandBase<MesSession, MesRequestInfo>
    {
        private string EmployeeName = "装配工序校验";
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            ExecuteBRC(session, requestInfo.TData);
        }

        #region 【装配工序校验】方法

        /// <summary>
        /// 装配工序校验
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void ExecuteBRC(MesSession _session, TransData _transData)
        {
            //1、参数校验---equipmentID---------------------------------
            string equipmentID = string.Empty;
            if (!EmployeeComm.CheckEquipmentID(_session, _transData, out equipmentID, EmployeeName))
                return;
              
            //3、参数校验-获取料箱条码与PCBA条码 并校验---------------------------------
            List<string> lstProcessData = StringHelper.GetStrArray(_transData.ProcessData, ',', false);
            if (!EmployeeComm.CheckProcessData(_session, _transData, out lstProcessData, EmployeeName, Comparator.Equal, 2))
                return;

            if (!GlobalData.IsDebug)
            {
                string boxCode = lstProcessData[0];
                string pcbaCode = lstProcessData[1];
                //4、PCBA条码验证(是否使用过)
                if (!PCBACheck(_session, _transData, pcbaCode, equipmentID))
                    return;

                //5、PCBA与料箱关联
                if (!PCBALinkBox(_session, _transData, pcbaCode, boxCode))
                    return;

                //6、PCBA与产品关联
                if (!PCBALinkSN(_session, _transData, pcbaCode, equipmentID))
                    return;
            }
            //7、API执行成功  ---------------------------------    
            EmployeeComm.SendMsg(_session, _transData, CheckResult.OK);
        }

        /// <summary>
        /// PCBA与产品关联
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="pcbaCode"></param>
        /// <param name="equipmentNumber"></param>
        /// <returns></returns>
        private bool PCBALinkSN(MesSession _session, TransData _transData, string pcbaCode, string equipmentNumber)
        {
            string mEmployeeName = $"{EmployeeName}-PCBA与产品关联";
            string actionParam = $"{_transData.SN},{pcbaCode},{equipmentNumber}";

            // 判断API是否执行超时 ---------------------------------
            DataTable dtR;
            bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>((Func<DataTable>)delegate { return Dm_Interface.SFC_DM_Insert_Assembly_METAAttachedPath(_transData.SN, pcbaCode, equipmentNumber); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, mEmployeeName);
            return bRet;
        }

        /// <summary>
        /// PCBA与料箱关联
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="pcbaCode"></param>
        /// <param name="boxID"></param>
        /// <returns></returns>
        private bool PCBALinkBox(MesSession _session, TransData _transData, string pcbaCode, string boxID)
        {
            string mEmployeeName = $"{EmployeeName}-PCBA与料箱关联";
            string actionParam = $"{boxID},{pcbaCode},{_transData.SN},{pcbaCode},3001";

            // 判断API是否执行超时 ---------------------------------
            DataTable dtR;
            bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>((Func<DataTable>)delegate { return Dm_Interface.SFC_DM_BuildRelationship_AttachedEventInsert(boxID, pcbaCode, _transData.SN, 3001); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, mEmployeeName);
            return bRet;
        }

        /// <summary>
        /// PCBA校验是否可用
        /// </summary>
        /// <returns></returns>
        private bool PCBACheck(MesSession _session, TransData _transData, string pcbaCode, string equipmentID)
        {
            string mEmployeeName = $"{EmployeeName}-PCBA校验";
            string actionParam = $"{_transData.SN},{pcbaCode},{equipmentID}";

            // 判断API是否执行超时 ---------------------------------
            DataTable dtR;
            bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>((Func<DataTable>)delegate { return Dm_Interface.SFC_DM_Insert_Assembly_METAAttachedPathOnlyCheck(_transData.SN, pcbaCode, equipmentID); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, mEmployeeName);
            return bRet;
        }

        #endregion

    }
}
