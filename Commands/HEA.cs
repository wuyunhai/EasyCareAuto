
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    /// <summary>
    /// 心跳
    /// </summary>
    public class HEA : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            requestInfo.TData.Status = CheckResult.OK.ToString();
            string msg = GlobalData.ToTranString(requestInfo.TData);
            session.Send(msg);
            LogInfo log = new LogInfo(session, LogLevel.Debug, GlobalData.Pre_Send + msg);
        }
    }
}
