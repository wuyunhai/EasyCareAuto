using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using System.Net;

namespace MES.SocketService
{
    public class MesReceiveFilterFactory : IReceiveFilterFactory<MesRequestInfo>
    {
        public IReceiveFilter<MesRequestInfo> CreateFilter(IAppServer appServer, IAppSession appSession, IPEndPoint remoteEndPoint)
        {
            return new MesProtocolReceiveFilter();
        } 
    }
}
