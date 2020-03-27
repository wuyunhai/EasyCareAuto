
using SuperSocket.SocketBase;
using System;

namespace MES.SocketService
{
    /// <summary>
    /// 委托回调函数 this.Invoke(new ThreadStart(delegate{})) 实现与UI交换
    /// </summary>
    public class DelegateState
    {
        //信息显示 
        public delegate void SocketStateCallBack(LogInfo log); 
        public delegate void SockeTeartbeatStateCallBack(int num);
        public delegate void SocketConnStateCallBack(string RemoteIp,string TCPUDP);
         
        /// <summary>
        /// 信息显示
        /// </summary>
        public static SocketStateCallBack ServerStateInfo;

        /// <summary>
        /// 心跳检测信息
        /// </summary>
        public static SockeTeartbeatStateCallBack TeartbeatServerStateInfo;

        /// <summary>
        /// 信息显示
        /// </summary>
        public static SocketConnStateCallBack ServerConnStateInfo;
        
        #region TCP服务
        
        public delegate void SocketTCPStateCallBack(string msg);
        public delegate void SocketAddTCPuserStateCallBack(MesSession mesSession); 
        public delegate void NewSessionConnectedCallBack(MesSession mesSession);
        public delegate void SessionClosedCallBack(MesSession mesSession, CloseReason reason);

        /// <summary>
        /// TCP信息显示
        /// </summary>
        public static SocketTCPStateCallBack ServerTCPStateInfo;
        /// <summary>
        /// TCP添加用户
        /// </summary>
        public static SocketAddTCPuserStateCallBack AddTCPuserStateInfo;
        /// <summary>
        /// 新的连接
        /// </summary>
        public static NewSessionConnectedCallBack NewSessionConnected;
        /// <summary>
        /// 连接关闭
        /// </summary>
        public static SessionClosedCallBack SessionClosed;

        #endregion
         
    }
}
