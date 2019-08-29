using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    public class LogInfo
    {

        #region 属性

        /// <summary>
        /// 日志时间
        /// </summary>
        private string logTime;
        public string LogTime
        {
            get
            {
                return logTime;
            }

            set
            {
                logTime = value;
            }
        }
        /// <summary>
        /// 日志等级
        /// </summary>
        private string level;
        public string Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }
        /// <summary>
        /// 本地地址
        /// </summary>
        private string localAddress;
        public string LocalAddress
        {
            get
            {
                return localAddress;
            }

            set
            {
                localAddress = value;
            }
        }
        /// <summary>
        /// 本地端口
        /// </summary>
        private string localPort;
        public string LocalPort
        {
            get
            {
                return localPort;
            }

            set
            {
                localPort = value;
            }
        }
        /// <summary>
        /// 本地别名
        /// </summary>
        private string localDeviceName;
        public string LocalDeviceName
        {
            get
            {
                return localDeviceName;
            }

            set
            {
                localDeviceName = value;
            }
        }
        /// <summary>
        /// 远程地址
        /// </summary>
        private string remoteAddress;
        public string RemoteAddress
        {
            get
            {
                return remoteAddress;
            }

            set
            {
                remoteAddress = value;
            }
        }
        /// <summary>
        /// 远程端口
        /// </summary>
        private string remotePort;
        public string RemotePort
        {
            get
            {
                return remotePort;
            }

            set
            {
                remotePort = value;
            }
        }
        /// <summary>
        /// 远程别名
        /// </summary>
        private string remoteDeviceName;
        public string RemoteDeviceName
        {
            get
            {
                return remoteDeviceName;
            }

            set
            {
                remoteDeviceName = value;
            }
        }
        /// <summary>
        /// 消息内容
        /// </summary>
        private string message;
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        private bool isView;
        public bool IsView
        {
            get
            {
                return isView;
            }

            set
            {
                isView = value;
            }
        }

        #endregion

        private void BaseInfo(LogLevel logLevel, string msg)
        {
            IsView = true;
            logTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            level = logLevel.ToString();
            message = msg.TrimEnd("\r\n".ToCharArray());
        }

        public LogInfo(MesSession session, LogLevel logLevel, string msg)
        {
            BaseInfo(logLevel, msg);
            isView = session.IsView;
            localAddress = session.LocalEndPoint.Address.ToString();
            localPort = session.LocalEndPoint.Port.ToString();
            localDeviceName = session.LocalDeviceName;
            remoteAddress = session.RemoteEndPoint.Address.ToString();
            remotePort = session.RemoteEndPoint.Port.ToString();
            remoteDeviceName = session.RemoteDeviceName;

            ILog iLog = session.Logger;
            WriteLog(iLog, logLevel);
        }

        public LogInfo(MesServer server, LogLevel logLevel, string msg)
        {
            BaseInfo(logLevel, msg);
            localAddress = "server";
            localPort = "";
            localDeviceName = "server";
            remoteAddress = "";
            remotePort = "";
            remoteDeviceName = "-/-";

            ILog iLog = server.Logger;
            WriteLog(iLog, logLevel);

        }

        private void WriteLog(ILog iLog, LogLevel logLevel)
        {
            string logMsg  = LocalDeviceName + " " + RemoteDeviceName + " " + Message;

            switch (logLevel)
            {
                case LogLevel.Fatal:
                    iLog.Fatal(logMsg);
                    break;
                case LogLevel.Error:
                    iLog.Error(logMsg);
                    break;
                case LogLevel.Warn:
                    iLog.Warn(logMsg);
                    break;
                case LogLevel.Info:
                    iLog.Info(logMsg);
                    break;
                case LogLevel.Debug:
                    iLog.Debug(logMsg);
                    break;
                case LogLevel.Success:
                    iLog.Info(logMsg);
                    break;
                default:
                    iLog.Info(logMsg);
                    break;
            }

            GlobalData.ViewLog(this);
        }

        public override string ToString()
        {
            //心跳服务器[172.16.140.40:5000]<---Normal client[127.0.0.1:65238] 
            return logTime + " " + Level + " " + LocalDeviceName + " " + RemoteDeviceName + " " + Message;
        }
    }

    public enum LogLevel
    {
        Fatal = 1,
        Error = 2,
        Warn = 3,
        Info = 4,
        Debug = 5,
        Success = 6
    }
}
