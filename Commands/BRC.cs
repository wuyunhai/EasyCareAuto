
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
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            CheckRoute(session, requestInfo.TData);
        }

        #region 【装配工序校验】方法

        /// <summary>
        /// 装配工序校验
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void CheckRoute(MesSession _session, TransData _transData)
        {
            LogInfo log = null;

            //1、参数校验-获取EquipmentNumber 并校验--------------------------------- 
            AppConfig cfg = new AppConfig();
            string EquipmentNumber = cfg.AppConfigGet(_transData.DeviceCode + "_" + _transData.OpeIndex);
            if (string.IsNullOrEmpty(EquipmentNumber))
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【装配工序过站】接口失败>> 参数错误，根据机台编码[{1}]与操作序号[{2}]获取设备编码失败，请检查配置文件中的机台映射关系是否正确。", _transData.SerialNumber, _transData.DeviceCode, _transData.OpeIndex));
                SendMsg(_session, _transData, CheckResult.NG);
                return;
            }

            //2、参数校验-获取料箱条码与PCBA条码 并校验---------------------------------
            List<string> lstProcessData = StringHelper.GetStrArray(_transData.ProcessData, ',', false);
            if (lstProcessData == null || lstProcessData.Count != 2)
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【装配工序校验】接口失败>> 协议错误，请检查协议中过程数据是否为[料箱条码,PCBA条码],当前过程数据：{1}。", _transData.SerialNumber, _transData.ProcessData));
                SendMsg(_session, _transData, CheckResult.NG);
                return;
            }

            string boxCode = lstProcessData[0];
            string pcbaCode = lstProcessData[1];
            //3、PCBA条码验证(是否使用过)
            if (!PCBACheck(_session, _transData, pcbaCode, EquipmentNumber))
                return;

            //4、PCBA与料箱关联
            if (!PCBALinkBox(_session, _transData, pcbaCode, boxCode))
                return;

            //5、PCBA与产品关联
            if (!PCBALinkSN(_session, _transData, pcbaCode, EquipmentNumber))
                return;


            //6、执行成功  ---------------------------------   
            log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【装配工序过站】接口成功>> 产品(SN[{0}])完成PASS过站。", _transData.SerialNumber));
            SendMsg(_session, _transData, CheckResult.OK);

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
            LogInfo log;
            // 判断API是否执行超时 ---------------------------------
            DataTable dtR = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return Dm_Interface.SFC_DM_Insert_Assembly_METAAttachedPath(_transData.SerialNumber, pcbaCode, equipmentNumber); }, GlobalData.ApiTimeout);
            if (dtR == null)
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【装配工序过站】-【PCBA与产品关联】接口超时（>{1}ms），执行参数：{0}，{2}，{3}。", _transData.SerialNumber, GlobalData.ApiTimeout, pcbaCode, equipmentNumber));
                SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            // 判断API是否执行成功 ---------------------------------
            string checkStatusR = DataTableHelper.GetCellValueinDT(dtR, 0, "CheckStatus");
            string checkMsgR = DataTableHelper.GetCellValueinDT(dtR, 0, 1);
            if (checkStatusR != "1")
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【装配工序过站】-【PCBA与产品关联】接口失败>> 接口返回(状态码[{1}---{2}])，执行参数：{0}，{3}，{4}。", _transData.SerialNumber, checkStatusR, checkMsgR, pcbaCode, equipmentNumber));
                SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【装配工序过站】-【PCBA与产品关联】接口成功>> 执行参数：{0}，{1}，{2}。", _transData.SerialNumber, pcbaCode, equipmentNumber));
            return true;
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
            LogInfo log;
            // 判断API是否执行超时 ---------------------------------
            DataTable dtR = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return Dm_Interface.SFC_DM_BuildRelationship_AttachedEventInsert(boxID, pcbaCode, _transData.SerialNumber, 3001); }, GlobalData.ApiTimeout);
            if (dtR == null)
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【装配工序过站】-【PCBA与料箱关联】接口超时（>{1}ms），执行参数：{2}，{3}，{0}，{4}。", _transData.SerialNumber, GlobalData.ApiTimeout, boxID, pcbaCode, 3001));
                SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            // 判断API是否执行成功 ---------------------------------
            string checkStatusR = DataTableHelper.GetCellValueinDT(dtR, 0, "CheckStatus");
            string checkMsgR = DataTableHelper.GetCellValueinDT(dtR, 0, 1);
            if (checkStatusR != "1")
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【装配工序过站】-【PCBA与料箱关联】接口失败>> 接口返回(状态码[{1}---{2}])，执行参数：{3}，{4}，{0}，{5}。", _transData.SerialNumber, checkStatusR, checkMsgR, boxID, pcbaCode, 3001));
                SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【装配工序过站】-【PCBA与料箱关联】接口成功>> 执行参数：{1}，{2}，{0}，{3}。", _transData.SerialNumber, boxID, pcbaCode, 3001));
            return true;
        }

        /// <summary>
        /// PCBA校验是否可用
        /// </summary>
        /// <returns></returns>
        private bool PCBACheck(MesSession _session, TransData _transData, string pcbaCode, string equipmentID)
        {
            LogInfo log;
            // 判断API是否执行超时 ---------------------------------
            DataTable dtR = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return Dm_Interface.SFC_DM_Insert_Assembly_METAAttachedPathOnlyCheck(_transData.SerialNumber, pcbaCode, equipmentID); }, GlobalData.ApiTimeout);
            if (dtR == null)
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【装配工序过站】-【PCBA校验】接口超时（>{1}ms），执行参数：{0}，{2}，{3}。", _transData.SerialNumber, GlobalData.ApiTimeout, pcbaCode, equipmentID));
                SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            // 判断API是否执行成功 ---------------------------------
            string checkStatusR = DataTableHelper.GetCellValueinDT(dtR, 0, "CheckStatus");
            string checkMsgR = DataTableHelper.GetCellValueinDT(dtR, 0, 1);
            if (checkStatusR != "1")
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【装配工序过站】-【PCBA校验】接口失败>> 接口返回(状态码[{1}---{2}])，执行参数：{0}，{3}，{4}。", _transData.SerialNumber, checkStatusR, checkMsgR, pcbaCode, equipmentID));
                SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }


            log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【装配工序过站】-【PCBA校验】接口成功>> 执行参数：{0}，{1}，{2}。", _transData.SerialNumber, pcbaCode, equipmentID));
            return true;
        }

        /// <summary>
        /// 反馈消息至PLC
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="checkR"></param>
        private static void SendMsg(MesSession _session, TransData _transData, CheckResult checkR)
        {
            _transData.Status = checkR.ToString();
            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);
            LogInfo log = new SocketService.LogInfo(_session, LogLevel.Info, GlobalData.Pre_Send + msg);
        }

        #endregion

    }
}
