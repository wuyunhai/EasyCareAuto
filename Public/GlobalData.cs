
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUN.Framework.Commons;
using YUN.Framework.Commons.Collections;
using YUN.Framework.Commons.Threading;

namespace MES.SocketService
{
    public static class GlobalData
    {
        #region var

        /// <summary>
        /// 协议分隔符
        /// </summary>
        public static string SplitChar;

        public static string Pre_Send;
        public static string Pre_Receive;
        public static string ServerStart;
        public static string ServerStop;
        public static string ClientConnect;
        public static string ClientDisConnect;
        public static string UnknownRequest;
        public static string Exception;

        public static string RUN_TYPE;

        public static SyncList<MesSession> ClientSessionList = new SyncList<MesSession>();
        public static int ApiTimeout = 1000;

        #endregion

        /// <summary>
        /// 初始化全局数据
        /// </summary>
        public static void InitGlobalData()
        {
            LoadConfig();
        }

        public const string ProtocolFormalError = "通讯协议错误(正确格式为：三位功能码 + 空格 + json字符串 + 回车换行符号).";


        #region 程序超时执行处理

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

        #endregion

        #region 校验接收数据是否正确

        /// <summary>
        /// 校验接收数据是否正确
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        /// <param name="recvTransData"></param>
        /// <returns></returns>
        public static bool CheckMesRequestInfo(MesSession session, MesRequestInfo requestInfo, CheckType checkType)
        {
            if (requestInfo.TData == null)
            {
                GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "RequestInfo");
                session.Logger.Error("RequestInfo is not format !");
                return false;
            }

            if (string.IsNullOrWhiteSpace(requestInfo.TData.DeviceCode))
            {
                GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "DeviceCode");
                session.Logger.Error("DeviceCode ERROR !");
                return false;
            }
            if (checkType == CheckType.SerialNumber && string.IsNullOrWhiteSpace(requestInfo.TData.SerialNumber))
            {
                GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "SerialNumber");
                session.Logger.Error("SerialNumber ERROR !");
                return false;
            }

            return true;
        }

        #endregion

        #region 关键字段为空时处理方法

        /// <summary>
        /// 关键字段为空时处理方法
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="_keyWord"></param>
        public static void KeyWordIsNullRecv(MesSession _session, TransData _transData, string _keyWord)
        {
            _transData.ProcessData = CheckResult.ERROR.ToString();
            string errorMsg = _keyWord + " is null or white space or format error ?";
            string msg = _transData.ToString();
            _session.Send(msg);

            LogInfo log = new SocketService.LogInfo(_session, LogLevel.Info, GlobalData.Pre_Send + msg);
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
                    sb.Append(_transData.SerialNumber);
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
            SplitChar = ";";

            Pre_Send = "发送 >> ";
            Pre_Receive = "接收 >> ";
            ServerStart = "MES.SocketService 启动.";
            ServerStop = "MES.SocketService 停止.";
            ClientConnect = "客户端连接事件：";
            ClientDisConnect = "客户端断开事件：";
            UnknownRequest = "未注册功能码，请确认协议内容，当前请求内容：";
            Exception = "异常信息：";


            AppConfig appconfig = new AppConfig();
            ApiTimeout = ConvertHelper.ToInt32(appconfig.AppConfigGet("ApiTimeout"), 1000);

        }

        #region 

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
    /// 
    /// </summary>
    public enum CheckResult
    {
        OK_RE,
        NG_RE,
        OK,
        NG,
        ERROR
    }
    public enum CheckType
    {
        SerialNumber,
        DeviceCode
    }
    public enum ActionResult
    {
        PASS,
        FAIL
    }
}
