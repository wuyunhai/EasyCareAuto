using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Protocol;

namespace MES.SocketService
{
    public abstract class ReceiveFilterHelper<TRequestInfo> : ReceiveFilterBase<TRequestInfo>
       where TRequestInfo : IRequestInfo
    {

        private bool m_FoundBegin = false;
        protected TRequestInfo NullRequestInfo = default(TRequestInfo);

        /// <summary>
        /// 初始化实例
        /// </summary>
        protected ReceiveFilterHelper()
        {

        }

        public override TRequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {

        }

        protected abstract TRequestInfo ProcessMatchedRequest(byte[] readBuffer, int offset, int length);

        public override void Reset()
        {
            m_FoundBegin = false;
            base.Reset();
        }
    }
}
