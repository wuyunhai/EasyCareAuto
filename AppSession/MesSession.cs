using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Protocol;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    public class MesSession : AppSession<MesSession, MesRequestInfo>
    {
        /// <summary>
        /// 是否已经在UI中挂载显示
        /// </summary>
        public bool IsShowinUI { get; set; }

        /// <summary>
        /// 远程别名
        /// </summary> 
        public string RemoteDeviceName { get; set; }

        /// <summary>
        /// 本地别名
        /// </summary> 
        public string LocalDeviceName { get; set; }

        /// <summary>
        /// 是否显示日志
        /// </summary> 
        public bool IsShowLog { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
        }
        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
        }
        protected override void HandleException(Exception e)
        {
            base.HandleException(e);

            LogInfo log = new SocketService.LogInfo(this, LogLevel.Error, GlobalData.Exception + e.ToString());
        }

        protected override void HandleUnknownRequest(MesRequestInfo requestInfo)
        {
            base.HandleUnknownRequest(requestInfo);

            LogInfo log = new SocketService.LogInfo(this, LogLevel.Error, $"未知请求，该功能码[{requestInfo.Key}]在服务中未注册，当前请求内容：{requestInfo.Body}。");
        }

        public override void Initialize(IAppServer<MesSession, MesRequestInfo> appServer, ISocketSession socketSession)
        {
            base.Initialize(appServer, socketSession);

            IsShowLog = true;
            RemoteDeviceName = $"Normal client [{RemoteEndPoint}]";
            LocalDeviceName = $"Listen server [{LocalEndPoint}]";
        }

        /// <summary>
        /// 连接关闭
        /// </summary>
        /// <param name="reason"></param>
        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }

    }
}
