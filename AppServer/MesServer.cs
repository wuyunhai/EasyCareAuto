using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    public class MesServer : AppServer<MesSession, MesRequestInfo>
    {
        //MesTerminatorReceiveFilter
        public MesServer() : base(new DefaultReceiveFilterFactory<MesTerminatorReceiveFilter, MesRequestInfo>()) //使用默认的接受过滤器工厂  
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
            LogInfo log = new SocketService.LogInfo(this, LogLevel.Info, GlobalData.ServerStart);

        }

        protected override void OnStopped()
        {
            base.OnStopped();
            LogInfo log = new SocketService.LogInfo(this, LogLevel.Info, GlobalData.ServerStop);
        }

        /// <summary>
        /// 新的连接
        /// </summary>
        /// <param name="session"></param>
        protected override void OnNewSessionConnected(MesSession session)
        {
            base.OnNewSessionConnected(session);

            //设置连接别名w

            AppConfig config = new AppConfig();
            string remoteDeviceName = config.AppConfigGet(session.RemoteEndPoint.Address.ToString());
            string localDeviceName = config.AppConfigGet(session.LocalEndPoint.Port.ToString());
            if (!string.IsNullOrEmpty(remoteDeviceName))
            {
                session.RemoteDeviceName = string.Format("{0} [{1}]", remoteDeviceName, session.RemoteEndPoint);
            }
            if (!string.IsNullOrEmpty(localDeviceName))
            {
                session.LocalDeviceName = string.Format("{0} [{1}]", localDeviceName, session.LocalEndPoint);
            }
            //添加至客户端连接List
            GlobalData.ClientSessionList.Add(session);

            LogInfo log = new SocketService.LogInfo(session, LogLevel.Info, GlobalData.ClientConnect + session.RemoteEndPoint);

            //触发事件
            DelegateState.NewSessionConnected?.Invoke(session);

        }

        protected override void OnSessionClosed(MesSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);

            GlobalData.ClientSessionList.Remove(session);

            LogInfo log = new SocketService.LogInfo(session, LogLevel.Info, GlobalData.ClientDisConnect + session.RemoteEndPoint + ",Reason：" + reason);

            DelegateState.SessionClosed?.Invoke(session, reason);
        }

    }
}
