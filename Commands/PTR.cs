
using LabelManager2;
using MES.SocketService.BLL;
using MES.SocketService.Entity;
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
using YUN.Framework.ControlUtil;

namespace MES.SocketService
{
    /// <summary>
    /// 【条码打印请求】
    /// </summary>
    public class PTR : CommandBase<MesSession, MesRequestInfo>
    {
        private string EmployeeName = "条码打印请求";
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            ExecutePTR(session, requestInfo.TData);
        }

        #region 【条码打印请求】方法

        /// <summary>
        /// 条码打印请求
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void ExecutePTR(MesSession _session, TransData _transData)
        {
            EmployeeName = "条码打印请求";

            //1、参数校验---WorkOrder---------------------------------  
            string workOrder = string.Empty;
            if (!EmployeeComm.CheckWorkOrder(_session, _transData, out workOrder, EmployeeName, DataFrom.SQLite))
                return;

            if (_transData.DeviceCode == "OP010")
            {
                EmployeeName = "产品" + EmployeeName;
                //2、获取条码 ---------------------------------
                if (!GetSN(_session, _transData, workOrder))
                    return;

                //4、开始打印打印 ---------------------------------
                if (!Print(_session, _transData))
                    return;

                //5、参数校验---equipmentID---------------------------------
                string equipmentID = string.Empty;
                if (!EmployeeComm.CheckEquipmentID(_session, _transData, out equipmentID, $"EmployeeName <103IN 报工获取设备ID>"))
                    return;

                if (!GlobalData.IsDebug)
                {
                    //5、首站选择在这里获取条码回来后进行执行报工
                    EmployeeComm.WorkingEfficiency(_session, _transData, equipmentID, EmployeeName, "103IN");
                }

            }
            else
            {
                EmployeeName = "成品" + EmployeeName;
                //3、获取条码 ---------------------------------
                if (!GetFinishSN(_session, _transData, workOrder))
                    return;

                //4、开始打印打印 ---------------------------------
                if (!Print(_session, _transData))
                    return;
            }

            //5、API执行成功  ---------------------------------       
            EmployeeComm.SendMsg(_session, _transData, CheckResult.OK);

        }

        /// <summary>
        /// 获取产品条码
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        private bool GetSN(MesSession _session, TransData _transData, string workOrder)
        {
            LogInfo log;
            string actionParam = $"{workOrder}";
            string mEmployeeName = $"{EmployeeName}-获取条码";
            DataTable dtR = null;
            if (!GlobalData.IsDebug)
            {
                bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM__ProvideBlockID_ByWO(workOrder); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, mEmployeeName);
                if (!bRet)
                    return false;
            }
            else
            {
                dtR = GetResidueByWO(workOrder);
            }

            // 判断API是否执行成功 ---------------------------------
            string checkStatusR = DataTableHelper.GetCellValueinDT(dtR, 0, 0);
            _transData.SN = checkStatusR.Substring(4); // 1---DFM201982.93748

            #region 校验明码

            List<string> lstCode = StringHelper.GetStrArray(_transData.SN, '-', false);
            if (lstCode.Count < 2)
            {
                log = new LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行条码拆分失败1。");
                return false;
            }
            try
            {
                string disCode = lstCode[1].Substring(3, 10);
            }
            catch (Exception e)
            {
                log = new LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行条码拆分失败2。");
                return false;
            }

            #endregion

            META_ParameterInfo info2 = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='OP010_SN_CHECK'");
            info2.Value = _transData.SN;
            BLLFactory<META_Parameter>.Instance.Update(info2, info2.ID);// 缓存当前条码留作下一步校验。  
            log = new LogInfo(_session, LogLevel.Info, $"[{_transData.SN}]执行【{mEmployeeName}】接口成功>> SN:[{_transData.SN}]【已缓存】,执行参数：{actionParam}。");

            return true;
        }
        /// <summary>
        /// 获取成品条码
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        private bool GetFinishSN(MesSession _session, TransData _transData, string workOrder)
        {
            string actionParam = $"{_transData.SN}";
            string mEmployeeName = $"{EmployeeName}-获取条码";
            DataTable dtR = null;
            if (!GlobalData.IsDebug)
            {
                bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM_PrintProductLabel(_transData.SN); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, mEmployeeName);
                if (!bRet)
                    return false;
            }
            else
            {
                dtR = GetResidueByWO(workOrder);
            }
            _transData.ProcessData = DataTableHelper.GetCellValueinDT(dtR, 0, "ReturnMsg");

            new LogInfo(_session, LogLevel.Info, $"获取原始成品条码数据成功：{_transData.ProcessData}");
            return true;
        }

        /// <summary>
        /// 开始打印(调用WEB打印方法)
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        private bool Print(MesSession _session, TransData _transData)
        {
            DataTable dtR;
            bool bRet = false;
            switch (_transData.DeviceCode)
            {
                case "OP010":
                    List<string> lstCode = StringHelper.GetStrArray(_transData.SN, '-', false);
                    string product = lstCode[1].Substring(3, 10);

                    DataTable dt = Dm_Interface.SFC_DM_SelectProductionProcess(lstCode[0]);
                    string prdType = dt.Rows[0][1].ToString();

                    new LogInfo(_session, LogLevel.Info, $"OP010打印数据准备完成，SN：{_transData.SN}，Product：{product}，PrdType：{prdType}");

                    OP010WebReference.Print printer_OP010 = new OP010WebReference.Print();
                    bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return printer_OP010.PrintBarCodeEX(_transData.SN, product, prdType); }, out dtR, 25000, _session, _transData, _transData.SN, EmployeeName);
                    break;
                default:
                    bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return PrintBarCode(_session, _transData.SN, _transData.ProcessData); }, out dtR, 25000, _session, _transData, $"{_transData.SN},{_transData.ProcessData}", EmployeeName);
                    UpdateBlockList(_session, _transData.SN, _transData.ProcessData);
                    break;
            }
            return bRet;
        }
        /// <summary>
        /// 模拟获取SN条码
        /// </summary>
        /// <param name="wo"></param>
        /// <returns></returns>
        private DataTable GetResidueByWO(string workOrder)
        {
            Random r = new Random();
            int i = r.Next(1, 99);
            DataTable dt = DataTableHelper.CreateTable("CheckStatus,ReturnMsg");
            dt.Rows.Add($"1---SWO.SN.QRCode-{System.DateTime.Now.ToString("fffMMddHHmmss")}", $"A009D3/PHEV.Lab/ECM Pconfirmity label - DFM.Lab/012345678901234/SN{i}/05/06/HW{i}/SW{i}");
            return dt;
        }

        public DataTable PrintBarCode(MesSession _session, string _sn, string processData)
        {
            META_ParameterInfo info = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='LablePath'");
            string lablePath = info.Value;
            info = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='TracePrinterName'");
            string tracePrinterName = info.Value;
            info = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='ConFormPrinterName'");
            string conFormPrinterName = info.Value;
            info = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='CustomerCode'");
            string customerCode = info.Value;
            META_DevLinkInfo devInfo = BLLFactory<META_DevLink>.Instance.FindSingle($"DevCode='OP320'");
            string productType = devInfo.ProcessType;

            new LogInfo(_session, LogLevel.Info, $"打印基础数据准备完成{System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");

            List<string> lstMsg = StringHelper.GetStrArray(processData, '/', false);
            string supplierCode = lstMsg[0];
            string traceLabel = lstMsg[1];
            string conFormLabel = lstMsg[2];
            string traceability = lstMsg[3];
            string imsafeCode = lstMsg[3];

            if (productType == "ECMP" && (supplierCode == "A009D3 01" || supplierCode == "M107190000"))
            {
                imsafeCode = customerCode + supplierCode + lstMsg[3].ToString().Substring(7, 6) + "A";
            }
            else
            {
                imsafeCode = lstMsg[3];
            }
            string sn = lstMsg[4];
            string hw = lstMsg[5];
            string sw = lstMsg[6];

            ApplicationClass lbl_1 = null, lbl_2 = null;
            Document doc_1 = null, doc_2 = null;

            DataTable dt = DataTableHelper.CreateTable("CheckStatus,ReturnMsg");
            dt.TableName = "PrintCheck";
            try
            {
                lbl_1 = new ApplicationClass();
                lbl_1.Documents.Open(lablePath + conFormLabel);
                doc_1 = lbl_1.ActiveDocument;
                doc_1.Printer.SwitchTo(conFormPrinterName);
                string[] arrProduct = _sn.Trim().Split('-');
                doc_1.Variables.FormVariables.Item("Product").Value = arrProduct[0].ToUpper();
                doc_1.Variables.FormVariables.Item("SullperCode").Value = supplierCode.ToUpper();
                doc_1.Variables.FormVariables.Item("Date").Value = System.DateTime.Now.ToString("dd-MM-yy");
                doc_1.Variables.FormVariables.Item("Barcode").Value = sn.ToUpper();
                doc_1.Variables.FormVariables.Item("HW").Value = hw.ToUpper();
                doc_1.Variables.FormVariables.Item("SW").Value = sw.ToUpper();
                new LogInfo(_session, LogLevel.Info, $"产品条码打印：DocName:{lablePath + conFormLabel},Printer:{conFormPrinterName},{doc_1.Printer.FullName}");
                doc_1.PrintDocument(1);

                lbl_2 = new ApplicationClass();
                lbl_2.Documents.Open(lablePath + traceLabel);
                doc_2 = lbl_2.ActiveDocument;
                doc_2.Printer.SwitchTo(tracePrinterName);
                doc_2.Variables.FormVariables.Item("Date").Value = System.DateTime.Now.ToString("dd-MM-yy");
                doc_2.Variables.FormVariables.Item("TraceabilityNumber").Value = traceability.Substring(6, 7).ToUpper();

                if (productType == "ECMP")
                {
                    doc_2.Variables.FormVariables.Item("SerialNumber").Value = customerCode.ToUpper();
                }
                else
                {
                    doc_2.Variables.FormVariables.Item("SerialNumber").Value = arrProduct[1].Substring(0, 13).ToUpper();
                }

                doc_2.Variables.FormVariables.Item("SullperCode").Value = supplierCode.ToUpper();
                doc_2.Variables.FormVariables.Item("Barcode").Value = imsafeCode.ToUpper();
                if (supplierCode == "A009D3 01")
                    doc_2.Variables.FormVariables.Item("NO.").Value = "01";
                else
                    doc_2.Variables.FormVariables.Item("NO.").Value = "";
                doc_2.PrintDocument(1);
                new LogInfo(_session, LogLevel.Info, $"客户追溯条码打印：DocName:{lablePath + traceLabel},Printer:{tracePrinterName},{doc_2.Printer.FullName}");
                dt.Rows.Add("1", "Print Success.");

            }
            catch (Exception e)
            {
                if (lbl_1 != null)
                    lbl_1.Quit();
                if (lbl_2 != null)
                    lbl_2.Quit();

                LogInfo log = new LogInfo(_session, LogLevel.Error, $"打印异常:{e.ToString()}");
                dt.Rows.Add("-1", e.Message);
            }
            finally
            {
                LogInfo log = new LogInfo(_session, LogLevel.Info, $"退出打印:TracePrinterName：{tracePrinterName},ConFormPrinterName:{conFormPrinterName}");
                if (lbl_1 != null)
                {
                    lbl_1.Documents.CloseAll(true);
                    lbl_1.Quit();//退出  
                    lbl_1 = null;
                    doc_1 = null;
                }
                if (lbl_2 != null)
                {
                    lbl_2.Documents.CloseAll(true);
                    lbl_2.Quit();//退出  
                    lbl_2 = null;
                    doc_2 = null;
                }
                GC.Collect(0);
            }

            ProcessHelper.KillProcess("lppa");

            return dt;
        }

        private static void UpdateBlockList(MesSession _session, string _sn, string processData)
        {
            string traceability = string.Empty;
            string imsafeCode = string.Empty;
            try
            {
                List<string> lstMsg = StringHelper.GetStrArray(processData, '/', false);
                traceability = lstMsg[3];
                imsafeCode = lstMsg[3];

                UpdatePrdBarcode.UpdateBarCode updatebarCode = new UpdatePrdBarcode.UpdateBarCode();
                DataTable dt = updatebarCode.UpdateSN(_sn, imsafeCode, traceability);

                if (dt.Rows[0][0].ToString() == "1")
                {
                    new LogInfo(_session, LogLevel.Info, $"更新条码成功：SN:{_sn},imsafeCode:{imsafeCode},traceability:{traceability}");
                }
                else
                {
                    new LogInfo(_session, LogLevel.Info, $"更新条码失败(API返回：{dt.Rows[0][0].ToString()})：SN:{_sn},imsafeCode:{imsafeCode},traceability:{traceability}");
                }
            }
            catch (Exception e)
            {
                new LogInfo(_session, LogLevel.Info, $"更新条码失败：SN:{_sn},imsafeCode:{imsafeCode},traceability:{traceability}\r\n {e.ToString()}");
            }
            finally
            {

            }
        }

        #endregion

    }
}
