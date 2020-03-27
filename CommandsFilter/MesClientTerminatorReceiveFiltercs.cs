using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MES.SocketService
{
    /*
      *  Socket里面的协议解析是Socket通讯程序设计中最复杂的地方，如果你的应用层协议设计或实现不佳，Socket通讯中常见的粘包，分包就难以避免。
      *  SuperSocket内置了命令行格式的协议CommandLineProtocol，如果你使用了其它格式的协议，就必须自行实现自定义协议CustomProtocol。
      *  你可能会觉得用 SuperSocket 来实现你的自定义协议并不简单。 为了让这件事变得更容易一些, SuperSocket 提供了一些通用的协议解析工具, 你可以用他们简单而且快速的实现你自己的通信协议:
         TerminatorReceiveFilter (SuperSocket.SocketBase.Protocol.TerminatorReceiveFilter, SuperSocket.SocketBase) ---结束符协议
         CountSpliterReceiveFilter (SuperSocket.Facility.Protocol.CountSpliterReceiveFilter, SuperSocket.Facility)---固定数量分隔符协议
         FixedSizeReceiveFilter (SuperSocket.Facility.Protocol.FixedSizeReceiveFilter, SuperSocket.Facility)---固定请求大小协议
         BeginEndMarkReceiveFilter (SuperSocket.Facility.Protocol.BeginEndMarkReceiveFilter, SuperSocket.Facility)---带起止符协议
         FixedHeaderReceiveFilter (SuperSocket.Facility.Protocol.FixedHeaderReceiveFilter, SuperSocket.Facility)---头部格式固定并包含内容长度协议
      */
    public class MesClientTerminatorReceiveFiltercs : TerminatorReceiveFilter<StringPackageInfo>
    {
        public MesClientTerminatorReceiveFiltercs()
            : base(Encoding.ASCII.GetBytes("\r\n"))
        {

        }
        public override StringPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            string cmdFullText = string.Empty;
            string key = string.Empty;
            string body = string.Empty;
            string[] parameters = null;
            cmdFullText = bufferStream.ReadString((int)bufferStream.Length, Encoding.UTF8);

            key = Regex.Split(cmdFullText, "\u003b")[0];//[\u0020]-空格；[\u003b]-分号。
            body = string.Join("", cmdFullText.ToArray().Skip(key.ToArray().Length + 1).ToList());
            body = cmdFullText.TrimEnd("\r\n".ToCharArray());
            parameters = body.Split(';');
            return new StringPackageInfo(key, body, parameters);
        }


    }
}
