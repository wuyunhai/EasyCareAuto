
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System;
using YUN.Framework.Commons;
using SuperSocket.SocketBase.Protocol;

namespace MES.SocketService
{
    public class MesTerminatorReceiveFilter : TerminatorReceiveFilter<MesRequestInfo>
    {
        public MesTerminatorReceiveFilter() : base(Encoding.ASCII.GetBytes("\r\n"))
        {
            //Encoding.ASCII.GetBytes("\r\n")
        }

        #region 解析函数

        public string SkinPrefix(string fullText, string prefix)
        {
            string str = string.Join("", fullText.ToArray().Skip(prefix.ToArray().Length).ToList());
            return str;
        }

        public TransData ToTransData(string key, string body, string cmdFullText)
        {
            try
            {
                LogInfo log = null;
                if (body.Length == 0)
                {
                    log = new SocketService.LogInfo(this.Session as MesSession, LogLevel.Error, "协议解析出错，原因：发送协议内容为空.");
                    return null;
                }

                string[] arrStr = body.Split(';');
                TransData transData = new SocketService.TransData();
                transData.FuncCode = arrStr[0];

                switch (key)
                {
                    case "HEA":
                        #region 心跳
                        if (arrStr.Length != 4)
                        {
                            log = new SocketService.LogInfo(this.Session as MesSession, LogLevel.Error, string.Format("协议解析出错，原因：协议长度不对，（按分号分割，正确为4段），当前请求内容：{0}。", body));
                            return null;
                        }

                        transData.DeviceCode = arrStr[1];
                        transData.OpeIndex = arrStr[2];
                        transData.Status = arrStr[3];
                        #endregion
                        break;
                    default:
                        #region 业务数据 
                        if (arrStr.Length != 6)
                        {
                            log = new SocketService.LogInfo(this.Session as MesSession, LogLevel.Error, string.Format("协议解析出错，原因：协议长度不对，（按分号分割，正确为6段），当前请求内容：{0}。", body));
                            return null;
                        }
                        transData.DeviceCode = arrStr[1];
                        transData.OpeIndex = arrStr[2];
                        transData.SerialNumber = arrStr[3];
                        transData.ProcessData = arrStr[4];
                        transData.Status = arrStr[5];
                        #endregion
                        break;
                }
                log = new SocketService.LogInfo(this.Session as MesSession, LogLevel.Info, GlobalData.Pre_Receive + body);
                return transData;

            }
            catch (Exception e)
            {
                LogInfo log = new SocketService.LogInfo(this.Session as MesSession, LogLevel.Error, string.Format("协议解析出错，当前请求内容：{0}。", body));
                return null;
            }
        }
        protected override MesRequestInfo ProcessMatchedRequest(byte[] data, int offset, int length)
        {
            TransData TData = new TransData();
            string cmdFullText = string.Empty;
            string key = string.Empty;
            string body = string.Empty;
            string strTData = string.Empty;

            cmdFullText = System.Text.UTF8Encoding.UTF8.GetString(data, offset, length);// data.ReadString(data.Length, Encoding.UTF8);
            try
            {
                key = Regex.Split(cmdFullText, "\u003b")[0];//[\u0020]-空格；[\u003b]-分号。
                body = string.Join("", cmdFullText.ToArray().Skip(key.ToArray().Length + 1).ToList());
                body = cmdFullText.TrimEnd("\r\n".ToCharArray());
                TData = ToTransData(key, body, cmdFullText);

                if (TData == null)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                key = cmdFullText;
            }

            return new MesRequestInfo(key, body, cmdFullText, TData);
        }


        #endregion
    }
}
