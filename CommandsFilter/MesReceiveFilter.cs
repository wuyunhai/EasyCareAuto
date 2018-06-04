using SuperSocket.SocketBase.Protocol;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System;

namespace MES.SocketService
{
    public class MesReceiveFilter : TerminatorReceiveFilter<MesRequestInfo>
    {

        public MesReceiveFilter() : base(Encoding.ASCII.GetBytes("\r\n"))
        {

        }
         

        protected override MesRequestInfo ProcessMatchedRequest(byte[] data, int offset, int length)
        {
            TransmitData TData = new TransmitData();
            string cmdFullText = string.Empty;
            string key = string.Empty;
            string body = string.Empty;
            string strTData = string.Empty;
            //string[] parameters = null;

            cmdFullText = System.Text.UTF8Encoding.UTF8.GetString(data, offset, length);// data.ReadString(data.Length, Encoding.UTF8);
            key = Regex.Split(cmdFullText, "\u0020")[0];
            body = string.Join("", cmdFullText.ToArray().Skip(key.ToArray().Length + 1).ToList());
            body = body.TrimEnd("\r\n".ToCharArray());
            TData = JsonHelper.Deserialize<TransmitData>(body);
            if (TData != null) TData.Func = key;
            return new MesRequestInfo(key, body, cmdFullText, TData);
        }
    }
}
