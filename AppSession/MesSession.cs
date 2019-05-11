using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Protocol;

namespace MES.SocketService
{
    public class MesSession : AppSession<MesSession, MesRequestInfo>
    {

        /// <summary>
        /// 唯一码
        /// </summary>
        public string SN { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
            DelegateState.NewSessionConnected?.Invoke(this);
        }
        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
        }
        protected override void HandleException(Exception e)
        {
            base.HandleException(e);

            string logMsg = GlobalData.Exception + e.Message;
            Logger.Debug(logMsg);
            GlobalData.ViewLog(logMsg);
        }
        protected override void HandleUnknownRequest(MesRequestInfo requestInfo)
        {
            base.HandleUnknownRequest(requestInfo);
            string logMsg = GlobalData.UnknownRequest + requestInfo.Key + requestInfo.Body;
            Logger.Debug(logMsg);
            GlobalData.ViewLog(logMsg);
        }
        

        /// <summary>
        /// 连接关闭
        /// </summary>
        /// <param name="reason"></param>
        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);

            string logMsg = GlobalData.ClientDisConnect + this.RemoteEndPoint + ",原因：" + reason + ".";
            Logger.Debug(logMsg);
            GlobalData.ViewLog(logMsg);

            DelegateState.SessionClosed?.Invoke(this, reason);

        }
    }
}
