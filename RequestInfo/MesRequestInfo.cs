using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Protocol;

namespace MES.SocketService
{
    public class MesRequestInfo : IRequestInfo
    {

        // <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="body">接收的数据体</param>
        public MesRequestInfo(string key, byte[] body)
        {
            this.Key = key;
            this.Body = body;
        }
        public string Key
        {
            get; set;
        }
        /// <summary>
        /// 请求信息缓存
        /// </summary>
        public byte[] Body { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceID { get; set; }
    }
}
