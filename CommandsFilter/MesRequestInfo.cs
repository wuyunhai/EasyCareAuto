using System;
using SuperSocket.SocketBase.Protocol;

namespace MES.SocketService
{
    public class MesRequestInfo : IRequestInfo
    {
        public MesRequestInfo(string header, string body,string fullData, TransData tData)
        { 
            Key = header;
            Header = header;
            Body = body;
            Data = fullData;

            TData = new TransData();
            TData = tData;
        }
        /// <summary>
        /// 服务器返回的字节数据头部
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 服务器返回的字节数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 服务器返回的字符串数据
        /// </summary>
        public string Body { get; set; } 

        public TransData  TData { get; set; }

        /// <summary>
        /// 功能码
        /// </summary>
        public string Key { get; set; }
    }
}
