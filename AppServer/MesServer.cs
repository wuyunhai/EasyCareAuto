using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;

namespace MES.SocketService
{
    public class MesServer : AppServer<MesSession, MesRequestInfo>
    {
        public MesServer() : base(new DefaultReceiveFilterFactory<MesReceiveFilter, MesRequestInfo>()) //使用默认的接受过滤器工厂  
        {

        }
        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            base.OnStarted();

            GlobalData.InitGlobalData();//初始化全局数据

            Logger.Info(GlobalData.ServerStart);
            GlobalData.ViewLog(GlobalData.ServerStart);
        }

        protected override void OnStopped()
        {
            base.OnStopped();
            Logger.Info(GlobalData.ServerStop);
            GlobalData.ViewLog(GlobalData.ServerStop);
        }

        /// <summary>
        /// 新的连接
        /// </summary>
        /// <param name="session"></param>
        protected override void OnNewSessionConnected(MesSession session)
        {
            base.OnNewSessionConnected(session);

            string logMsg = GlobalData.ClientConnect + session.RemoteEndPoint + ".";
            Logger.Info(logMsg);
            GlobalData.ViewLog(logMsg);

        }

        protected override void OnSessionClosed(MesSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);

            string logMsg = GlobalData.ClientDisConnect + session.RemoteEndPoint + ".";
            Logger.Info(logMsg);
            GlobalData.ViewLog(logMsg);
        }

    }
}
