
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
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            CheckRoute(session, requestInfo.TData);
        }

        #region 【料箱与栈位关联关系校验】方法

        /// <summary>
        /// 料箱与栈位关联关系校验
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
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【料箱与栈位关联关系校验】接口失败>> 参数错误，根据机台编码[{1}]与操作序号[{2}]获取设备编码失败，请检查配置文件中的机台映射关系是否正确。", _transData.SerialNumber, _transData.DeviceCode, _transData.OpeIndex));
                SendMsg(_session, _transData);
                return;
            }

            //2、获取料箱条码与栈位条码 并校验---------------------------------
            List<string> lstProcessData = StringHelper.GetStrArray(_transData.ProcessData, ',', false);
            if (lstProcessData.Count != 2)
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【料箱与栈位关联关系校验】接口失败>> 协议错误，请检查协议中过程数据是否为[料箱条码,栈位条码],当前过程数据：{1}。", _transData.SerialNumber, _transData.ProcessData));
                SendMsg(_session, _transData);
                return;
            }

            //4、API执行是否超时 ---------------------------------
            DataTable dt = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return CheckLink(_transData.SerialNumber, EquipmentNumber, lstProcessData[0], lstProcessData[1]); }, GlobalData.ApiTimeout);
            if (dt == null)
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【料箱与栈位关联关系校验】接口超时（>{1}ms），执行参数：{0}，{2}，{3}，{4}。", _transData.SerialNumber, GlobalData.ApiTimeout, EquipmentNumber, lstProcessData[0], lstProcessData[1]));
                SendMsg(_session, _transData);
                return;
            }

            //5、判断API是否执行成功 ---------------------------------
            string checkStatus = DataTableHelper.GetCellValueinDT(dt, 0, "CheckStatus");
            if (checkStatus != "1")
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【料箱与栈位关联关系校验】接口失败>> 接口返回(状态码[{1}])，执行参数：{0}，{2}，{3}，{4}。", _transData.SerialNumber, checkStatus, EquipmentNumber, lstProcessData[0], lstProcessData[1]));
                SendMsg(_session, _transData);
                return;
            }

            //6、API执行成功  ---------------------------------                             
            _transData.Status = CheckResult.OK.ToString();
            log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【料箱与栈位关联关系校验】接口成功>> 接口返回(状态码[{1}])，执行参数：{0}，{2}，{3}，{4}。", _transData.SerialNumber, checkStatus, EquipmentNumber, lstProcessData[0], lstProcessData[1]));
            SendMsg(_session, _transData);

        }

        // 料箱与栈位条码验证
        private DataTable CheckLink(string serialNumber, string equipmentNumber, string boxCode, string slotCode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CheckStatus");
            dt.Rows.Add(2);

            Thread.Sleep(1500);
            return dt;
        }

        private static void SendMsg(MesSession _session, TransData _transData)
        {
            // 发送【料箱与栈位关联关系校验】执行结果至PLC
            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);
            LogInfo log = new SocketService.LogInfo(_session, LogLevel.Info, GlobalData.Pre_Send + msg);
        }

        #endregion



    }
}
