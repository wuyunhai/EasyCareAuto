using DM_API;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    public static class GlobalData
    {
        #region var

        /// <summary>
        /// UID前缀
        /// </summary>
        public static string UID_Prefix;
        /// <summary>
        /// 仓库ID前缀
        /// </summary>
        public static string StorageId_Prefix;
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

        #endregion

        //配置文件助手
        public static ConfigHelper CfgHelper = null;

        /// <summary>
        /// 初始化全局数据
        /// </summary>
        public static void InitGlobalData()
        {
            //配置文件对象
            CfgHelper = new ConfigHelper();
            LoadConfig();
        }

        public const string ProtocolFormalError = "通讯协议错误(正确格式为：三位功能码 + 空格 + json字符串 + 回车换行符号).";

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

            if (string.IsNullOrWhiteSpace(requestInfo.TData.StationID))
            {
                GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "StationID");
                session.Logger.Error("StationID error !");
                return false;
            }
            if (checkType == CheckType.UID && string.IsNullOrWhiteSpace(requestInfo.TData.UID))
            {
                GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "UID");
                session.Logger.Error("UID error !");
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
        public static void KeyWordIsNullRecv(MesSession _session, TransmitData _transData, string _keyWord)
        {

            _transData.CheckResult = CheckResult.ERROR.ToString();
            string errorMsg = _keyWord + " is null or white space or format error ?";
            string msg = _transData.ToString();// _transData.CommuCode + " " + JsonHelper.Serialize(_transData) + Environment.NewLine;
            _session.Send(msg);

            string logMsg = GlobalData.Pre_Send + msg;
            _session.Logger.Info(logMsg);
            GlobalData.ViewLog(logMsg);
        }

        #endregion

        #region 协议拼装

        /// <summary>
        /// 协议拼装
        /// </summary>
        /// <param name="_transData"></param>
        /// <returns></returns>
        public static string ToTranString(TransmitData _transData)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_transData.CommuCode);
            sb.Append(GlobalData.SplitChar);
            sb.Append(_transData.COM);
            sb.Append(GlobalData.SplitChar);
            sb.Append(_transData.StationID);

            if (_transData.UID != null)
            {
                sb.Append(GlobalData.SplitChar);
                sb.Append(GlobalData.UID_Prefix + _transData.UID);
            }
            if (_transData.StorageId != null)
            {
                sb.Append(GlobalData.SplitChar);
                sb.Append(GlobalData.StorageId_Prefix + _transData.StorageId);
            }

            CommunicationCode commuCode = EnumHelper.GetInstance<CommunicationCode>(_transData.CommuCode);
            switch (commuCode)
            {
                case CommunicationCode.Ask:
                case CommunicationCode.ProcessData:
                case CommunicationCode.ResultData:

                    break;
                case CommunicationCode.Ask_R:
                case CommunicationCode.ProcessData_R:
                case CommunicationCode.ResultData_R:
                    sb.Append(GlobalData.SplitChar);
                    sb.Append(_transData.CheckResult);
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
            UID_Prefix = "UID=";
            StorageId_Prefix = "StorageId=";
            SplitChar = ";";

            Pre_Send = " 发送 >> ";
            Pre_Receive = " 接收 >> ";
            ServerStart = "MES.SocketService 启动.";
            ServerStop = "MES.SocketService 停止.";
            ClientConnect = "客户端连接事件：";
            ClientDisConnect = "客户端断开事件：";
            UnknownRequest = "未知请求：";
            Exception = "异常信息：";
            //读取配置参数
            //GXH_4_DeviceID = CfgHelper.GetKeyValue("GXH_4_DeviceID");

        }
        
        #region 

        public static void ViewLog(string msg)
        {
            switch (RUN_TYPE.ToLower())
            {
                case "r":
                    ConsoleView(msg);
                    break;
                case "w":
                    WinFormView(msg);
                    break;
            }
        }

        private static void ConsoleView(string msg)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":" + msg);
        }

        private static void WinFormView(string msg)
        {
            DelegateState.ServerStateInfo?.Invoke(msg);
        }

        #endregion
    }

    public enum CheckResult
    {
        OK,
        NG,
        ERROR
    }
    public enum CheckType
    {
        UID,
        Device
    }
}
