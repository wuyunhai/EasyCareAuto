
using LabelManager2;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    /// <summary>
    /// 【条码打印请求】
    /// </summary>
    public class PTR : CommandBase<MesSession, MesRequestInfo>
    {
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            CheckRoute(session, requestInfo.TData);
        }

        #region 【条码打印请求】方法

        /// <summary>
        /// 条码打印请求
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
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【条码打印请求】接口失败>> 参数错误，根据机台编码[{1}]与操作序号[{2}]获取设备编码失败，请检查配置文件中的机台映射关系是否正确。", _transData.SerialNumber, _transData.DeviceCode, _transData.OpeIndex));
                SendMsg(_session, _transData);
                return;
            }

            //2、获取WorkOrder 并校验---------------------------------
            string WorkOrder = cfg.AppConfigGet("WorkOrder");
            if (string.IsNullOrEmpty(WorkOrder))
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【条码打印请求】接口失败>> 参数错误，获取工单编码失败，请检查配置文件中的 WorkOrder 是否写入。", _transData.SerialNumber));
                SendMsg(_session, _transData);
                return;
            }

            //4、API执行是否超时 ---------------------------------
            //DataTable dt = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return Dm_Interface.SFC_DM__ProvideBlockID_ByWO(WorkOrder); }, GlobalData.ApiTimeout);
            DataTable dt = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return GetProvideBlockID_ByWO(WorkOrder); }, GlobalData.ApiTimeout);
            if (dt == null)
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【条码打印请求】接口超时（>{1}ms），执行参数：{2}。", _transData.SerialNumber, GlobalData.ApiTimeout, WorkOrder));
                SendMsg(_session, _transData);
                return;
            }

            //5、判断API是否执行成功 ---------------------------------
            string strRet = DataTableHelper.GetCellValueinDT(dt, 0, 0);
            string snBarCode = strRet.Substring(4); // 1---DFM201982.93748
            if (string.IsNullOrEmpty(snBarCode))
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【条码打印请求】接口失败>> 接口返回：{1}，执行参数：{2}。", _transData.SerialNumber, strRet, WorkOrder));
                SendMsg(_session, _transData);
                return;
            }

            cfg.AppConfigSet(_transData.DeviceCode + "_SN_" + _transData.OpeIndex, snBarCode); // 缓存当前条码留作下一步校验。
            _transData.SerialNumber = snBarCode; 

            //6.3、API【装配工序过站】执行是否超时 ---------------------------------
            DataTable dtR = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return PrintCode(snBarCode); }, 5000);
            if (dtR == null)
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【条码打印请求】接口超时（>{1}ms），执行参数：{0}。", _transData.SerialNumber, 5000));
                SendMsg(_session, _transData);
                return;
            }

            //6.4、判断API是否执行成功 ---------------------------------
            string checkStatusR = DataTableHelper.GetCellValueinDT(dtR, 0, "CheckStatus");
            string checkMsgR = DataTableHelper.GetCellValueinDT(dtR, 0, "ReturnMsg");
            if (checkStatusR != "1")
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【条码打印请求】接口失败>> 原因：{1}，执行参数：{0}。", _transData.SerialNumber, checkMsgR));
                SendMsg(_session, _transData);
                return;
            }

            //6.5、API执行成功  ---------------------------------                             
            _transData.Status = CheckResult.OK.ToString();
            log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【条码打印请求】接口成功>> 执行参数：{0}。", _transData.SerialNumber));
            SendMsg(_session, _transData);
            
            //ApplicationClass lbl = null;
            //Document doc = null;
            //try
            //{
            //    lbl = new ApplicationClass();
            //    lbl.Documents.Open(@"E:\DAService\SNCode.Lab");
            //    doc = lbl.ActiveDocument;
            //    doc.Variables.FormVariables.Item("PN_2D").Value = snBarCode;
            //    doc.PrintDocument(1);

            //    cfg.AppConfigSet(_transData.DeviceCode + "_SN_" + _transData.OpeIndex, snBarCode); // 缓存当前条码留作下一步校验。

            //    _transData.Status = CheckResult.OK.ToString();
            //    log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【条码打印请求】接口成功>> 执行参数：{1}，ActivePrinterName：{2}，DefaultFilePath：{3}。", _transData.SerialNumber, snBarCode, lbl.ActivePrinterName, lbl.DefaultFilePath));

            //}
            //catch (Exception e)
            //{
            //    if (lbl != null)
            //        lbl.Quit();

            //    _transData.Status = CheckResult.NG.ToString();
            //    log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【条码打印请求】接口失败>> 原因：{1}，执行参数：{2},ActivePrinterName：{3}，DefaultFilePath：{4}。", _transData.SerialNumber, e.Message, snBarCode, lbl.ActivePrinterName, lbl.DefaultFilePath));

            //}
            //finally
            //{
            //    if (lbl != null)
            //    {
            //        lbl.Documents.CloseAll(true);
            //        lbl.Quit();//退出  
            //        lbl = null;
            //        doc = null;
            //    }
            //    GC.Collect(0);
            //}

            //SendMsg(_session, _transData);

        }

        private DataTable PrintCode(string barCode)
        {
            OP010WebReference.Print printer = new OP010WebReference.Print();
            printer.Credentials = new NetworkCredential("Administrator", "123456");
            DataTable dt = printer.PrintBarCode(barCode);
            return dt;
        }

        // 根据工单获取SN条码
        private DataTable GetProvideBlockID_ByWO(string wo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CheckStatus");
            dt.Rows.Add("1---SN2019082160238");

            //Thread.Sleep(1500);
            return dt;
        }

        private static void SendMsg(MesSession _session, TransData _transData)
        {
            // 发送【条码打印请求】执行结果至PLC
            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);
            LogInfo log = new SocketService.LogInfo(_session, LogLevel.Info, GlobalData.Pre_Send + msg);
        }

        #endregion

    }
}
