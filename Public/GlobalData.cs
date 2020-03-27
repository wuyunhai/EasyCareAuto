
using MES.SocketService.BLL;
using MES.SocketService.Entity;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YUN.Framework.Commons;
using YUN.Framework.Commons.Collections;
using YUN.Framework.Commons.Threading;
using YUN.Framework.ControlUtil;

namespace MES.SocketService
{
    /// <summary>
    /// 全局数据
    /// </summary> 
    public static class GlobalData
    {
        #region var

        #region 定义常量

        public static readonly string SplitChar = ";";
        public static readonly string Pre_Send = "发送 >> ";
        public static readonly string Pre_Receive = "接收 >> ";
        public static readonly string ServerStart = "MES.SocketService 启动.";
        public static readonly string ServerStop = "MES.SocketService 停止.";
        public static readonly string ClientConnect = "客户端连接事件：";
        public static readonly string ClientDisConnect = "客户端断开事件：";
        public static readonly string UnknownRequest = "未注册功能码，请确认协议内容，当前请求内容：";
        public static readonly string Exception = "异常信息：";

        #endregion

        /// <summary>
        /// UI刷新间隔（单位：ms）
        /// </summary>
        public static int RefreshSecond = 1000;
        /// <summary>
        /// 是否调试模式，调试模式下不校验参数，不与MES服务器进行交互，直接返回OK
        /// </summary>
        public static bool IsDebug = true;

        //程序运行模式【w-桌面程序模式；r-控制台模式；i-服务模式】
        public static string RUN_TYPE;

        //设定本程序内API执行超时的限定时间
        public static int ApiTimeout = 1000;
        //WebService接口用户登录名
        public static string WebUserID = "Administrator";
        //WebService接口用户登录密码
        public static string WebPassword = "123456";

        private static DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();
        //数据采集队列服务
        public static QueueServer<TestItemFlex> queueServerNDQ = new QueueServer<TestItemFlex>();

        //客户端连接的缓存List
        public static SyncList<MesSession> ClientSessionList = new SyncList<MesSession>();

        #endregion

        /// <summary>
        /// 初始化全局数据
        /// </summary>
        public static void InitGlobalData()
        {
            LoadConfig();
        }

        #region 程序超时执行处理
        //
        /// <summary>
        /// 程序超时执行处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TaskAction"></param>
        /// <param name="TimeoutMillisecond"></param>
        /// <returns></returns>
        public static T RunTaskWithTimeout<T>(Func<T> TaskAction, int TimeoutMillisecond)
        {
            Task<T> backgroundTask;

            try
            {
                backgroundTask = Task.Factory.StartNew(TaskAction);
                backgroundTask.Wait(new TimeSpan(0, 0, 0, 0, TimeoutMillisecond));
            }
            catch (AggregateException ex)
            {
                // task failed
                var failMessage = ex.Flatten().InnerException.Message;
                return default(T);
            }
            catch (Exception ex)
            {
                // task failed
                var failMessage = ex.Message;
                return default(T);
            }

            if (!backgroundTask.IsCompleted)
            {
                // task timed out
                return default(T);
            }

            // task succeeded
            return backgroundTask.Result;
        }
        public static T RunTaskWithTimeout<T>(Func<T> TaskAction, int TimeoutMillisecond, MesSession _session, TransData _transData, string actionParam, string caller, bool toPlc = true)
        {
            LogInfo log;
            Task<T> backgroundTask;

            try
            {
                backgroundTask = Task.Factory.StartNew(TaskAction);
                backgroundTask.Wait(new TimeSpan(0, 0, 0, 0, TimeoutMillisecond));
            }
            catch (AggregateException ex)
            {
                // task failed
                var failMessage = ex.Flatten().InnerException.Message;
                log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口超时（>{GlobalData.ApiTimeout}ms），原因：{failMessage}，执行参数：{actionParam}.");
                _transData.ProcessData = $"Mission Timeout({TimeoutMillisecond}ms) , Please check the database connection";
                if (toPlc) EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);

                return default(T);
            }
            catch (Exception ex)
            {
                // task failed
                var failMessage = ex.Message;
                log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口超时（>{GlobalData.ApiTimeout}ms），原因：{failMessage}，执行参数：{actionParam}.");
                _transData.ProcessData = $"Mission Timeout({TimeoutMillisecond}ms) , Please check the database connection";
                if (toPlc) EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return default(T);
            }

            if (!backgroundTask.IsCompleted)
            {
                // task timed out 
                log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口超时（>{GlobalData.ApiTimeout}ms），请检查网络状况或数据库连接配置文件是否正确，执行参数：{actionParam}.");
                _transData.ProcessData = $"Mission Timeout({TimeoutMillisecond}ms) , Please check the database connection,Check SQLServer Connection. ";
                if (toPlc) EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return default(T);
            }

            // task succeeded
            log = new LogInfo(_session, LogLevel.Info, $"[{_transData.SN}]执行【{caller}】接口成功>> 执行参数：{actionParam}。");
            return backgroundTask.Result;
        }
        public static bool RunTaskWithTimeoutDT<DataTableT>(Func<DataTable> TaskAction, out DataTable dtR, int TimeoutMillisecond, MesSession _session, TransData _transData, string actionParam, string caller, bool toPlc = true)
        {
            LogInfo log;
            Task<DataTable> backgroundTask;
            dtR = null;
            try
            {
                backgroundTask = Task.Factory.StartNew(TaskAction);
                backgroundTask.Wait(new TimeSpan(0, 0, 0, 0, TimeoutMillisecond));
            }
            catch (AggregateException ex)
            {
                var failMessage = ex.Flatten().InnerException.Message;
                log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口超时（>{TimeoutMillisecond}ms），原因：{failMessage}，执行参数：{actionParam}.");
                _transData.ProcessData = $"Mission Timeout({TimeoutMillisecond}ms) , Please check the database connection";
                if (toPlc) EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }
            catch (Exception ex)
            {
                var failMessage = ex.Message;
                log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口超时（>{TimeoutMillisecond}ms），原因：{failMessage}，执行参数：{actionParam}.");
                _transData.ProcessData = $"Mission Timeout({TimeoutMillisecond}ms) , Please check the database connection";
                if (toPlc) EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            if (!backgroundTask.IsCompleted)
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口超时（>{TimeoutMillisecond}ms），请检查网络状况或数据库连接配置文件是否正确，执行参数：{actionParam}.");
                _transData.ProcessData = $"Mission Timeout({TimeoutMillisecond}ms) , Please check the database connection";
                if (toPlc) EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            // task succeeded
            dtR = backgroundTask.Result;
            // 判断API是否执行成功 ---------------------------------
            //string checkStatusR = DataTableHelper.GetCellValueinDT(dtR, 0, "CheckStatus");
            //string checkMsgR = DataTableHelper.GetCellValueinDT(dtR, 0, "ReturnMsg");
            string checkStatusR = DataTableHelper.GetCellValueinDT(dtR, 0, 0);
            string checkMsgR = DataTableHelper.GetCellValueinDT(dtR, 0, 1);
            if (checkStatusR == "-1")
            {
                log = new LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{caller}】接口失败>> 接口返回({checkStatusR}---{checkMsgR})，执行参数：{actionParam}。");
                _transData.ProcessData = $"Error:{checkMsgR}";
                if (toPlc) EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return false;
            }

            log = new LogInfo(_session, LogLevel.Info, $"[{_transData.SN}]执行【{caller}】接口成功>> 执行参数：{actionParam}。");
            return true;
        }

        #endregion

        #region 协议拼装

        /// <summary>
        /// 协议拼装
        /// </summary>
        /// <param name="_transData"></param>
        /// <returns></returns>
        public static string ToTranString(TransData _transData)
        {
            StringBuilder sb = new StringBuilder();

            switch (_transData.FuncCode)
            {
                case "HEA":
                    #region 心跳

                    sb.Append(_transData.FuncCode);
                    sb.Append(GlobalData.SplitChar);
                    sb.Append(_transData.DeviceCode);
                    sb.Append(GlobalData.SplitChar);
                    sb.Append(_transData.OpeIndex);
                    sb.Append(GlobalData.SplitChar);
                    sb.Append(_transData.Status);

                    #endregion
                    break;

                default:
                    #region Normal 

                    sb.Append(_transData.FuncCode);
                    sb.Append(GlobalData.SplitChar);
                    sb.Append(_transData.DeviceCode);
                    sb.Append(GlobalData.SplitChar);
                    sb.Append(_transData.OpeIndex);
                    sb.Append(GlobalData.SplitChar);
                    sb.Append(_transData.SN);
                    sb.Append(GlobalData.SplitChar);
                    sb.Append(_transData.ProcessData);
                    sb.Append(GlobalData.SplitChar);
                    sb.Append(_transData.Status);

                    #endregion
                    break;
            }
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// 读取配置参数
        /// </summary>
        public static void LoadConfig()
        {
            try
            {
                List<META_ParameterInfo> lstParameter = BLLFactory<META_Parameter>.Instance.GetAll();
                META_ParameterInfo info = lstParameter.Find(li => li.Key == "RefreshSecond");
                if (info != null)
                    RefreshSecond = ConvertHelper.ToInt32(info.Value, 1000);

                info = lstParameter.Find(li => li.Key == "ApiTimeout");
                if (info != null)
                    ApiTimeout = ConvertHelper.ToInt32(info.Value, 1000);

                info = lstParameter.Find(li => li.Key == "WebUserID");
                if (info != null)
                    WebUserID = info.Value;

                info = lstParameter.Find(li => li.Key == "WebPassword");
                if (info != null)
                    WebUserID = info.Value;

                info = lstParameter.Find(li => li.Key == "IsDebug");
                if (info != null)
                    IsDebug = info.Value.Equals("是") ? true : false;

                queueServerNDQ.IsBackground = true;
                queueServerNDQ.ProcessItem += QueueServer_ProcessItem;

            }
            catch (Exception e)
            {

            }

        }

        #region 参数采集


        private static void QueueServer_ProcessItem(TestItemFlex item)
        {
            try
            {
                if (string.IsNullOrEmpty(item.Type))
                {
                    InsertNdqData(item);
                }
                else
                {
                    EmployeeComm.WorkingEfficiency(item.MesSession, item.TransData, item.EquipmentID, item.EmployeeName, item.Type);
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }


        //采集数据存入db
        private static void InsertNdqData(TestItemFlex item)
        {
            // 采集类别码
            string type = item.ProcessData[0];
            // 采集类别码中文描述
            string typeDes = string.Empty;
            switch (type)
            {
                case "01":
                    typeDes = "螺丝数据采集";
                    break;
                case "02":
                    typeDes = "气密性测试数据采集";
                    break;
                case "03":
                    typeDes = "预热温度数据采集";
                    break;
                case "04":
                    typeDes = "固化温度数据采集";
                    break;
                case "05":
                    typeDes = "点胶胶量数据采集";
                    break;
            }

            // 采集参数值
            string testitem_NText = string.Empty;
            for (int i = 1; i < item.ProcessData.Count; i++)
            {
                testitem_NText += item.ProcessData[i] + ",";
            }
            testitem_NText.TrimEnd(',');

            try
            {
                Dm_Interface.SFC_DM_CheckRouterInsertTestItemAttache_NText(item.SN, item.EquipmentID, type, testitem_NText);
                LogInfo log = new LogInfo(item.MesSession, LogLevel.Info, $"[{item.DevCode }]-[{item.SN}]{typeDes}成功，参数：{testitem_NText}");
            }
            catch (Exception e)
            {
                LogInfo log = new LogInfo(item.MesSession, LogLevel.Error, $"[{item.DevCode }]-[{item.SN}]{typeDes}失败，参数：{testitem_NText}，原因：{e.ToString()}");
            }

        }


        #endregion

        #region 选择日志显示模式

        public static void ViewLog(LogInfo log)
        {
            if (RUN_TYPE == null)
            {
                return;
            }

            switch (RUN_TYPE.ToLower())
            {
                case "r":
                    ConsoleView(log);
                    break;
                case "w":
                    WinFormView(log);
                    break;
            }
        }

        private static void ConsoleView(LogInfo log)
        {
            Console.WriteLine(log.ToString());
        }

        private static void WinFormView(LogInfo log)
        {
            DelegateState.ServerStateInfo?.Invoke(log);
        }

        #endregion

    }

    /// <summary>
    /// 协议请求状态枚举
    /// </summary>
    public enum CheckResult
    {
        OK_RE,
        NG_RE,
        OK,
        NG,
        ERROR
    }

    public enum Comparator
    {
        [Description("大于")]
        MoreThan,
        [Description("小于")]
        LessThan,
        [Description("等于")]
        Equal,
        [Description("不等于")]
        NotEqual
    }
    public enum DataFrom
    {
        [Description("SQLite处获取")]
        SQLite,
        [Description("SQLServer处获取")]
        SQLServer
    }

}
