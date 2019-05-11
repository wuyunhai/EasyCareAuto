using SuperSocket.SocketBase.Protocol;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System;
using YUN.Framework.Commons;

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
            try
            {
                key = Regex.Split(cmdFullText, "\u003b")[0];//[\u0020]-空格；[\u003b]-分号。
                key = EnumHelper.GetMemberName<CommunicationCode>(key);
                body = string.Join("", cmdFullText.ToArray().Skip(key.ToArray().Length + 1).ToList());
                body = cmdFullText.TrimEnd("\r\n".ToCharArray());
                TData = ToTransData(body, cmdFullText);

                if (TData == null)
                {

                }
            }
            catch (Exception e)
            {
                /// key = "ERROR";
                key = cmdFullText;
            }

            return new MesRequestInfo(key, body, cmdFullText, TData);
        }

        #region 解析函数

        public string SkinPrefix(string fullText, string prefix)
        {
            string str = string.Join("", fullText.ToArray().Skip(prefix.ToArray().Length).ToList());
            return str;
        }

        public TransmitData ToTransData(string body, string cmdFullText)
        {
            try
            {
                TransmitData transData = new SocketService.TransmitData();

                if (body.Length > 0)
                {
                    string[] arrStr = body.Split(';');

                    transData.CommuCode = arrStr[0];
                    transData.COM = arrStr[1];
                    transData.StationID = arrStr[2];

                    CommunicationCode eCommuCode = EnumHelper.GetInstance<CommunicationCode>(transData.CommuCode);
                    StationCode eStationID = EnumHelper.GetInstance<StationCode>(transData.StationID);

                    switch (eStationID)
                    {
                        case StationCode.MaterialFilling:
                            switch (eCommuCode)
                            {
                                case CommunicationCode.Ask:
                                case CommunicationCode.ResultData_R:
                                    transData.UID = SkinPrefix(arrStr[3], GlobalData.UID_Prefix);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case StationCode.MaterialInWarehouse:
                            switch (eCommuCode)
                            {
                                case CommunicationCode.Ask:
                                    transData.UID = SkinPrefix(arrStr[3], GlobalData.UID_Prefix);
                                    break;
                                case CommunicationCode.ProcessData_R:
                                case CommunicationCode.ResultData:
                                    transData.UID = SkinPrefix(arrStr[3], GlobalData.UID_Prefix);
                                    transData.StorageId = SkinPrefix(arrStr[4], GlobalData.StorageId_Prefix);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case StationCode.MaterialOutWarehouse:
                            switch (eCommuCode)
                            {
                                case CommunicationCode.Ask:
                                    break;
                                case CommunicationCode.Ask_R:
                                    break;
                                case CommunicationCode.ProcessData:
                                    break;
                                case CommunicationCode.ProcessData_R:
                                    break;
                                case CommunicationCode.ResultData:
                                    break;
                                case CommunicationCode.ResultData_R:
                                    break;
                                case CommunicationCode.Heart:
                                    break;
                                case CommunicationCode.Error:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case StationCode.PartFilling:
                            switch (eCommuCode)
                            {
                                case CommunicationCode.Ask:
                                    break;
                                case CommunicationCode.Ask_R:
                                    break;
                                case CommunicationCode.ProcessData:
                                    break;
                                case CommunicationCode.ProcessData_R:
                                    break;
                                case CommunicationCode.ResultData:
                                    break;
                                case CommunicationCode.ResultData_R:
                                    break;
                                case CommunicationCode.Heart:
                                    break;
                                case CommunicationCode.Error:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case StationCode.PartInReadyhouse:
                            switch (eCommuCode)
                            {
                                case CommunicationCode.Ask:
                                    break;
                                case CommunicationCode.Ask_R:
                                    break;
                                case CommunicationCode.ProcessData:
                                    break;
                                case CommunicationCode.ProcessData_R:
                                    break;
                                case CommunicationCode.ResultData:
                                    break;
                                case CommunicationCode.ResultData_R:
                                    break;
                                case CommunicationCode.Heart:
                                    break;
                                case CommunicationCode.Error:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case StationCode.PartInCachehouse:
                            switch (eCommuCode)
                            {
                                case CommunicationCode.Ask:
                                    break;
                                case CommunicationCode.Ask_R:
                                    break;
                                case CommunicationCode.ProcessData:
                                    break;
                                case CommunicationCode.ProcessData_R:
                                    break;
                                case CommunicationCode.ResultData:
                                    break;
                                case CommunicationCode.ResultData_R:
                                    break;
                                case CommunicationCode.Heart:
                                    break;
                                case CommunicationCode.Error:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }


                    if (arrStr[3].Contains(GlobalData.UID_Prefix))
                    {
                        transData.UID = string.Join("", arrStr[3].ToArray().Skip(GlobalData.UID_Prefix.ToArray().Length).ToList());
                    }
                    else if (arrStr[3].Contains(GlobalData.StorageId_Prefix))
                    {
                        transData.StorageId = string.Join("", arrStr[3].ToArray().Skip(GlobalData.StorageId_Prefix.ToArray().Length).ToList());
                    }

                    string logMsg = GlobalData.Pre_Receive + body + ".";
                    Session.Logger.Info(logMsg);
                    GlobalData.ViewLog(logMsg);

                    return transData;
                }

            }
            catch (Exception e)
            {
                string logMsg = "协议解析出错，发送协议内容：" + body + ".";
                Session.Logger.Error(logMsg);
                GlobalData.ViewLog(logMsg);
            }

            return null;
        }

        #endregion
    }
}
