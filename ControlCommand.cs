using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.SocketService
{ 
    public class ControlCommand
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<IBootstrap, string[], bool> Handler { get; set; }
    }
}
