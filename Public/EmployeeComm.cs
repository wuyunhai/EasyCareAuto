using MES.SocketService.BLL;
using MES.SocketService.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using YUN.Framework.Commons;
using YUN.Framework.Commons.Threading;
using YUN.Framework.ControlUtil;

namespace MES.SocketService
{
    public static class EmployeeComm
    {


        /// <summary>
        /// 反馈消息至PLC
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="checkR"></param>
        public static void SendMsg(MesSession _session, TransData _transData, CheckResult checkR)
        {
            _transData.Status = checkR.ToString();
            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);
            LogInfo log = new SocketService.LogInfo(_session, LogLevel.Info, GlobalData.Pre_Send + msg);
        }

        /// <summary>
        /// 校验WorkOrder并返回
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="workOrder"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public static bool CheckNormalParam(MesSession _session, TransData _transData, string paramKey, out string paramValue, string paramDisplay, string caller)
        {
            META_ParameterInfo info = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='{paramKey}'");
            if (info == null && string.IsNullOrEmpty(info.Value))
            {
                paramValue = "ERROR";
                LogInfo log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口失败>> 参数错误，获取{paramDisplay}失败，请检查SQLLite中是否缓存{paramDisplay},(请求参数：{paramKey})。");
                _transData.ProcessData = $"Failed to get the cache barcode by [{paramKey}],Check SQLite DB.";
                EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            paramValue = info.Value;
            return true;
        }

        /// <summary>
        /// 校验EquipmentID并返回
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        public static bool CheckEquipmentID(MesSession _session, TransData _transData, out string equipmentID, string caller)
        {
            caller += "-获取设备编码";
            equipmentID = string.Empty;
            META_DevLinkInfo info = BLLFactory<META_DevLink>.Instance.FindSingle($"DevCode='{_transData.DeviceCode}'");
            if (info == null)
            {
                LogInfo log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口失败>> 参数错误，根据机台编码[{_transData.DeviceCode}] 获取设备编码失败，请检查SQLLite中是否配置机台映射关系。");
                _transData.ProcessData = $"Failed to get the equipmentID by {_transData.DeviceCode},Check SQLite DB.";
                EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            //前工序校验是获取产品工艺类型
            if (!GlobalData.IsDebug && _transData.FuncCode == "PRC")
            {
                DataTable dtR;
                DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();
                caller += "-获取工艺类型<用于识别设备编码>";
                string[] arrSN = _transData.SN.Trim().Split('-');
                bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM_SelectProductionProcess(arrSN[0]); }, out dtR, GlobalData.ApiTimeout, _session, _transData, arrSN[0], caller);
                if (!bRet)
                    return false;

                string type = DataTableHelper.GetCellValueinDT(dtR, 0, 1);
                info.ProcessType = type;
                BLLFactory<META_DevLink>.Instance.Update(info, info.ID);
                LogInfo log = new SocketService.LogInfo(_session, LogLevel.Info, $"[{_transData.SN}]获取当前机台工艺类型成功，工艺类型[{info.ProcessType}]【已缓存】 ");

            }

            switch (info.ProcessType)
            {
                case "PHEV-3.3KW":
                    equipmentID = info.EquipmentID2;
                    break;
                case "PHEV-6.6KW":
                    equipmentID = info.EquipmentID3;
                    break;
                default:
                    equipmentID = info.EquipmentID1;
                    break;
            }
            return true;
        }

        /// <summary>
        /// 校验WorkOrder并返回（配置文件获取）
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="workOrder"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public static bool CheckWorkOrder(MesSession _session, TransData _transData, out string workOrder, string caller, DataFrom getFrom)
        {

            workOrder = string.Empty;
            caller += "-获取工单编码";

            META_ParameterInfo info = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='WorkOrder'");
            if (info == null && string.IsNullOrEmpty(info.Value))
            {
                LogInfo log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口失败>> 参数错误，请检查SQLLite中是否配置WorkOrder参数。");
                EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            switch (getFrom)
            {
                case DataFrom.SQLite:
                    workOrder = info.Value;
                    break;
                case DataFrom.SQLServer:
                    DataTable dtR;
                    DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();
                    bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return DBHelper.GetWorkOrderRemain(); }, out dtR, GlobalData.ApiTimeout, _session, _transData, "空", caller);
                    if (!bRet)
                        return false;

                    workOrder = DataTableHelper.GetCellValueinDT(dtR, 0, "ReturnMsg");
                    info.Value = workOrder;
                    BLLFactory<META_Parameter>.Instance.Update(info, info.ID);
                    LogInfo log = new SocketService.LogInfo(_session, LogLevel.Info, $"[{_transData.SN}]执行【{caller}】接口成功，数据缓存本地成功，当前工单号：{workOrder}。");

                    break;
            }
            return true;
        }

        /// <summary>
        /// 获取工单方法
        /// </summary>
        /// <param name="wo"></param>
        /// <returns></returns>
        private static DataTable GetWO()
        {
            DataTable dt = DataTableHelper.CreateTable("CheckStatus,ReturnMsg");
            dt.Rows.Add("1", "WO198909290015");
            return dt;
        }

        /// <summary>
        /// 校验ProcessData并返回
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="status"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public static bool CheckProcessData(MesSession _session, TransData _transData, out List<string> lstProcessData, string caller, Comparator comparator, int count)
        {
            lstProcessData = StringHelper.GetStrArray(_transData.ProcessData, ',', false);
            bool bCp = false;
            switch (comparator)
            {
                case Comparator.MoreThan:
                    if (lstProcessData.Count > count)
                        bCp = true;
                    break;
                case Comparator.LessThan:
                    if (lstProcessData.Count < count)
                        bCp = true;
                    break;
                case Comparator.Equal:
                    if (lstProcessData.Count == count)
                        bCp = true;
                    break;
                case Comparator.NotEqual:
                    if (lstProcessData.Count != count)
                        bCp = true;
                    break;
            }

            if (!bCp)
            {
                LogInfo log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】协议错误，当前协议中过程数据项应该是{EnumHelper.GetDescription(typeof(Comparator), comparator)}{count}项,当前过程数据（{lstProcessData.Count} 项）：{_transData.ProcessData}。");
                _transData.ProcessData = $"Protocol error, procedure data should be {comparator} {count} items, current is {lstProcessData.Count} items, procedure data: {_transData.ProcessData}";
                EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
            }

            return bCp;
        }

        /// <summary>
        /// 校验Status并返回
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="status"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public static bool CheckApiStatus(MesSession _session, TransData _transData, out string apiStatus, string caller)
        {
            META_ParameterInfo info = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='{_transData.Status}'");
            apiStatus = "ERROR";
            if (info == null || string.IsNullOrEmpty(info.Value))
            {
                LogInfo log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口失败>> 参数错误，请检查SQLLite中是否配置WorkOrder参数，执行参数：{_transData.Status}。");
                EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }
            apiStatus = info.Value;
            return true;
        }

        #region 通用过站
        
        /// <summary>
        /// 执行过站 [包含操作：1、判断是否记录不良(FAIL时记录)，2、执行生产扣料(不管是否需要扣料都执行)，3、判断是否执行报工(正常是机台最后一道工序，但是如果遇到FAIL则马上执行报工)，4、插过站记录]
        /// </summary>
        /// <returns></returns>
        public static bool CheckRoute(MesSession _session, TransData _transData, string workOrder, string equipmentID, string apiStatus, string employeeName)
        {
            string badName = string.Empty;

            // 1、如果返回FAIL，判断该工序是否可以传FAIL，如果可以则顺带返回不良名称
            if (apiStatus == "FAIL")
            {
                bool bRetBad = GetBadName(_session, _transData, out badName, equipmentID, employeeName);
                if (!bRetBad)
                    return bRetBad;
            }

            //1、 必须先插过站记录 ---------------------------------
            DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();
            string actionParam = $"{_transData.SN},{workOrder},{equipmentID},{apiStatus}, 2";
            DataTable dtR;
            bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM_CheckRouteVV4(_transData.SN, workOrder, equipmentID, apiStatus, "2"); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, employeeName);
            if (!bRet)
                return bRet;

            //2、是否测试失败记录不良（ps:打螺丝失败等）
            if (apiStatus == "FAIL")
            {
                bool bRetBad = BadRecord(_session, _transData, badName, equipmentID, apiStatus, employeeName);
                if (!bRetBad)
                    return bRetBad;
            }

            // 2020年1月7日15:43:49 --> 因为 SAP 报工耗时巨大，暂时托里出来，单独放进队列执行 
            //3、判断是否执行报工(如果记录了不良则无需进行第二次报工，因为记录不良时已经进行第一次报工了)
            //if (apiStatus != "FAIL")
            //    WorkingEfficiency(_session, _transData, equipmentID, employeeName, "103OUT");

            //4、执行生产扣料【2019年11月19日13:59:40 >> 当前没有上料，所以扣料一定失败，暂时取消该API的执行。 】
            //ReduceComponent(_session, _transData, equipmentID, employeeName, false);

            return true;
        }

        /// <summary>
        /// 执行生产扣料(不管需不需要，过站前执行一遍，系统自动判断，如需口料则扣，无需则不扣)
        /// 该方法执行成功与否无需知会PLC
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="workOrder"></param>
        /// <param name="equipmentID"></param>
        /// <param name="apiStatus"></param>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        private static bool ReduceComponent(MesSession _session, TransData _transData, string equipmentID, string employeeName, bool toPlc)
        {
            // 判断API是否执行超时 ---------------------------------
            DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();
            employeeName += "_生产扣料";
            string actionParam = $"{equipmentID},{_transData.SN}";
            DataTable dtR;
            bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM_Standard_BMReduceComponent(equipmentID, _transData.SN); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, employeeName, toPlc);
            return bRet;
        }

        /// <summary>
        /// 判断并执行报工(两种情况：1、正常每个机台最后一道工序报工；2、出现FAIL时不管处于哪个工序都立即报工)
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="equipmentID"></param>
        /// <param name="employeeName"></param>
        /// <param name="isCheck">是否需要校验机台工序是否处于该机台最后一步，默认需要</param>
        /// <param name="toPlc"></param>
        /// <returns></returns>
        public static bool WorkingEfficiency(MesSession _session, TransData _transData, string equipmentID, string employeeName, string INorOUT, bool toPlc = false)
        {
            string caller = string.Empty;
            try
            {
                // true-报工成功，false-报工失败
                DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();
                //E.ATP1002.0020.01 传ATP1002.0020
                string phevEquipID = equipmentID.Substring(equipmentID.Length - 15, 12);
                string actionParam = $"{_transData.SN},{phevEquipID},{INorOUT},产品工站工时效率";
                caller = $"{employeeName}-{_transData.DeviceCode}_{_transData.OpeIndex}MES报工";
                DataTable dtR;
                bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM_UnitItemWorkingEfficiency(_transData.SN, phevEquipID, INorOUT, "产品工站工时效率"); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, caller, toPlc);

                // SAP报工
                if (INorOUT == "103OUT")
                {
                    string status_IN = _transData.Status.Contains("OK") ? "1" : "0";//【PASS】传值<1>,【FAIL】传值<0>
                    actionParam = $"{phevEquipID},{_transData.SN},{status_IN}";
                    caller = $"{employeeName}-{_transData.DeviceCode}_{_transData.OpeIndex}SAP报工";
                    bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM_Proc_MESSet_OrdersConfirm(phevEquipID, _transData.SN, status_IN); }, out dtR, GlobalData.ApiTimeout, _session, _transData, actionParam, caller, toPlc);
                }
                return bRet;
            }
            catch (Exception e)
            {
                LogInfo log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口失败>> Error：{e.Message}");
                return false;
            }
        }

        /// <summary>
        /// 测试失败记录不良（ps:打螺丝失败等）
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        private static bool BadRecord(MesSession _session, TransData _transData, string badItemName, string equipmentID, string apiStatus, string employeeName)
        {
            DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();
            string actionParam = $"{_transData.SN},{badItemName},0.0,0.0,0.0,FAIL";
            string caller = $"{employeeName}-记录不良";
            string sRet = GlobalData.RunTaskWithTimeout<string>(delegate { return Dm_Interface.SFC_DM_InsertTestItem(_transData.SN, badItemName, 0.0, 0.0, 0.0, "FAIL"); }, GlobalData.ApiTimeout, _session, _transData, actionParam, caller);
            if (string.IsNullOrWhiteSpace(sRet))
                return false;

            WorkingEfficiency(_session, _transData, equipmentID, employeeName, "103OUT");

            return true;
        }
        /// <summary>
        /// 获取不良名称 
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        private static bool GetBadName(MesSession _session, TransData _transData, out string badName, string equipmentID, string employeeName)
        {
            badName = string.Empty;
            DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();
            string mEmployeeName = $"{employeeName}-记录不良之获取不良名称";

            //获取缺陷名称
            DataTable dtR;
            bool bRet = GlobalData.RunTaskWithTimeoutDT<DataTable>(delegate { return Dm_Interface.SFC_DM_ProcessQuery(equipmentID); }, out dtR, GlobalData.ApiTimeout, _session, _transData, equipmentID, mEmployeeName);
            if (!bRet)
                return false;

            badName = DataTableHelper.GetCellValueinDT(dtR, 0, "ReturnMsg");
            return true;
        }
        #endregion

    }


}
