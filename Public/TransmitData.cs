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
        public string SN { get; set; }
        /// <summary>
        /// 过程数据
        /// </summary> 
        public string ProcessData { get; set; }
        /// <summary>
        /// 状态码
        /// </summary> 
        public string Status { get; set; }

        public static TransData GetInstance(string[] parameters)
        {
            TransData td = new SocketService.TransData();
            if (parameters.Length == 6)
            {
                td.FuncCode = parameters[0];
                td.DeviceCode = parameters[1];
                td.OpeIndex = parameters[2];
                td.SN = parameters[3];
                td.ProcessData = parameters[4];
                td.Status = parameters[5];
            }
            else
            {
                td.FuncCode = "";
                td.DeviceCode = "";
                td.OpeIndex = "";
                td.SN = "";
                td.ProcessData = "";
                td.Status = "";
            }
            return td;
        }


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
            if (SN != null)
            {
                sb.Append(SN + GlobalData.SplitChar);
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
