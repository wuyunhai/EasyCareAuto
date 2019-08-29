using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MES.SocketService
{
    //MES-PLC传输数据格式
    public class TransData
    {
        /// <summary>
        /// 功能码
        /// </summary>
        public string FuncCode { get; set; }
        /// <summary>
        /// 机台编码
        /// </summary>
        public string DeviceCode { get; set; }
        /// <summary>
        /// 操作序号 
        /// </summary>
        public string OpeIndex { get; set; }
        /// <summary>
        /// 产品条码
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 过程数据
        /// </summary> 
        public string ProcessData { get; set; }
        /// <summary>
        /// 状态码
        /// </summary> 
        public string Status { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (FuncCode != null)
            {
                sb.Append(FuncCode + GlobalData.SplitChar);
            }
            if (DeviceCode != null)
            {
                sb.Append(DeviceCode + GlobalData.SplitChar);
            }
            if (OpeIndex != null)
            {
                sb.Append(OpeIndex + GlobalData.SplitChar);
            }
            if (SerialNumber != null)
            {
                sb.Append(SerialNumber + GlobalData.SplitChar);
            } 
            if (ProcessData != null)
            {
                sb.Append(ProcessData + GlobalData.SplitChar);
            }

            string nS = sb.ToString().TrimEnd(';');
            nS += Environment.NewLine;

            return nS;
        }
    }

}
