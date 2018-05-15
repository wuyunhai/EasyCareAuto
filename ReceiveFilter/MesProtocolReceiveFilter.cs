using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.Facility.Protocol;
using SuperSocket.Common;

namespace MES.SocketService
{
    public class MesProtocolReceiveFilter : FixedHeaderReceiveFilter<MesRequestInfo>
    {
        public MesProtocolReceiveFilter() : base(4)
        {

        }



        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
           // return (int)header[offset + 1] * 256 + (int)header[offset + 1];

            var headerData = new byte[3];
            Array.Copy(header, offset + 1, headerData, 0, 1);
            int i = BitConverter.ToInt32(headerData, 0);
            return BitConverter.ToInt32(headerData, 0);


        }

        protected override MesRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {

            TransmitData res = new TransmitData();
            string entireFrame = BytesToHexStr(header.Array) + BytesToHexStr(bodyBuffer.CloneRange(offset, length));
            res.Func = BytesToHexStr(header.Array);
            res.EquipmentID = BytesToHexStr(bodyBuffer.CloneRange(offset, length));
            //res.DeviceLogicalCode = entireFrame.Substring(2, 8);
            //res.Seq = entireFrame.Substring(10, 4);
            //res.ControlCode = entireFrame.Substring(16, 2);
            //res.Length = entireFrame.Substring(18, 4);
            //int dataLen = int.Parse(HEXtoDEC(ReverseHexString(res.Length)));
            //res.Data = entireFrame.Substring(22, dataLen * 2);
            //res.Cs = entireFrame.Substring(22 + dataLen * 2, 2);
            return new MesRequestInfo(res);
        }

        /// <summary>
        /// 高低对调
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string ReverseHexString(string str)
        {
            char[] buff = new char[str.Length];
            for (int i = 0; i < str.Length; i += 2)
            {
                buff[i] = str[str.Length - i - 2];
                buff[i + 1] = str[str.Length - 1 - i];
            }
            string s = new string(buff);
            return s;
        }

        /// <summary>
        /// 16进制转10进制
        /// </summary>
        /// <param name="HEX"></param>
        /// <returns></returns>
        string HEXtoDEC(string HEX)
        {
            return Convert.ToInt64(HEX, 16).ToString();
        }

        /// <summary>
        /// 转化bytes成16进制的字符
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        string BytesToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
    }
}
