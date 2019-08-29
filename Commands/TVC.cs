﻿
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
    /// 【检验工序过站（值类型）】
    /// </summary>
    public class TVC : CommandBase<MesSession, MesRequestInfo>
    {
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            CheckRoute(session, requestInfo.TData);
        }

        #region 【检验工序过站（值类型）】方法

        /// <summary>
        /// 检验工序过站（值类型）
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
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【检验工序过站（值类型）】接口失败>> 参数错误，根据机台编码[{1}]与操作序号[{2}]获取设备编码失败，请检查配置文件中的机台映射关系是否正确。", _transData.SerialNumber, _transData.DeviceCode, _transData.OpeIndex));
                SendMsg(_session, _transData);
                return;
            }

            //2、获取WorkOrder 并校验---------------------------------
            string WorkOrder = cfg.AppConfigGet("WorkOrder");
            if (string.IsNullOrEmpty(WorkOrder))
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【检验工序过站（值类型）】接口失败>> 参数错误，获取工单编码失败，请检查配置文件中的 WorkOrder 是否写入。", _transData.SerialNumber));
                SendMsg(_session, _transData);
                return;
            }

            //3、获取待检验数据并校验有效性 ---------------------------------
            decimal weight = ConvertHelper.ToDecimal(_transData.ProcessData, 0);
            if (weight <= 0)
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【检验工序过站（值类型）】之称重检验接口失败>> 协议错误，请检查协议中过程数据是否为有效值（数值需大于0）,当前过程数据：{1}。", _transData.SerialNumber, _transData.ProcessData));
                SendMsg(_session, _transData);
                return;
            }

            //4、API执行是否超时 ---------------------------------
            DataTable dt = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return CheckWeight(weight); }, GlobalData.ApiTimeout);
            if (dt == null)
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【检验工序过站（值类型）】之称重检验接口超时（>{1}ms），执行参数：{2}。", _transData.SerialNumber, GlobalData.ApiTimeout, weight));
                SendMsg(_session, _transData);
                return;
            }

            //5、判断API是否执行成功 ---------------------------------
            string checkStatus = DataTableHelper.GetCellValueinDT(dt, 0, "CheckStatus");
            if (checkStatus != "1")
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【检验工序过站（值类型）】之称重检验接口失败>> 接口返回(状态码[{1}])：产品(SN[{0}])计划执行称重校验，执行参数：{2}。", _transData.SerialNumber, checkStatus, weight));
                SendMsg(_session, _transData);
                return;
            }

            //6.1、API【检验工序过站（值类型）】执行是否超时 ---------------------------------
            DataTable dtR = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return Dm_Interface.SFC_DM_CheckRoute(_transData.SerialNumber, EquipmentNumber, WorkOrder, "PASS"); }, GlobalData.ApiTimeout);
            if (dtR == null)
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【检验工序过站（值类型）】接口超时（>{1}ms），执行参数：{0}，{2}，{3}，{4}。", _transData.SerialNumber, GlobalData.ApiTimeout, EquipmentNumber, WorkOrder, "PASS"));
                SendMsg(_session, _transData);
                return;
            }

            //6.2、判断API是否执行成功 ---------------------------------
            string checkStatusR = DataTableHelper.GetCellValueinDT(dtR, 0, "CheckStatus");
            if (checkStatusR != "1")
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【检验工序过站（值类型）】接口失败>> 接口返回(状态码[{1}])：产品(SN[{0}])计划执行 {2} 过站，执行参数：{0}，{3}，{4}，{2}。", _transData.SerialNumber, checkStatus, "PASS", EquipmentNumber, WorkOrder));
                SendMsg(_session, _transData);
                return;
            }

            //6.3、API执行成功  ---------------------------------                             
            _transData.Status = CheckResult.OK.ToString();
            log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【检验工序过站（值类型）】接口成功>> 接口返回(状态码[{1}])：产品(SN[{0}]) {2} 过站。", _transData.SerialNumber, checkStatusR, "PASS"));
            SendMsg(_session, _transData);

        }

        private DataTable CheckWeight(decimal weight)
        {
            Thread.Sleep(1500);
            return new DataTable();
        }

        private static void SendMsg(MesSession _session, TransData _transData)
        {
            // 发送【检验工序过站（值类型）】执行结果至PLC
            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);
            LogInfo log = new SocketService.LogInfo(_session, LogLevel.Info, GlobalData.Pre_Send + msg);
        }

        #endregion



    }
}