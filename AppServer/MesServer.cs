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
            Logger.Info("Mes Server 启动");
        }

        protected override void OnStopped()
        {
            base.OnStopped();
            Logger.Info("Mes Server 停止");
        }

        /// <summary>
        /// 新的连接
        /// </summary>
        /// <param name="session"></param>
        protected override void OnNewSessionConnected(MesSession session)
        {
            base.OnNewSessionConnected(session);

            Logger.Info("加入新的连接:" + session.RemoteEndPoint.ToString());
            
        }

    }
}
