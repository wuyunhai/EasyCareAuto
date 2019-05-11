using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MES.SocketService
{
    //MES-PLC传输数据格式
    public class TransmitData
    {
        /// <summary>
        /// 通讯码
        /// </summary>
        public string CommuCode { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public string COM { get; set; }
        /// <summary>
        /// 站号 
        /// </summary>
        public string StationID { get; set; }
        /// <summary>
        /// RFID
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 结果（校验/装配/...）
        /// </summary> 
        public string CheckResult { get; set; }
        /// <summary>
        /// 存储道ID
        /// </summary> 
        public string StorageId { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (CommuCode != null)
            {
                sb.Append(CommuCode + GlobalData.SplitChar);
            }
            if (COM != null)
            {
                sb.Append(COM + GlobalData.SplitChar);
            }
            if (StationID != null)
            {
                sb.Append(StationID + GlobalData.SplitChar);
            }
            if (UID != null)
            {
                sb.Append(GlobalData.UID_Prefix + UID + GlobalData.SplitChar);
            }
            if (StorageId != null)
            {
                sb.Append(GlobalData.StorageId_Prefix + StorageId + GlobalData.SplitChar);
            }
            if (CheckResult != null)
            {
                sb.Append(CheckResult + GlobalData.SplitChar);
            }

            string nS = sb.ToString().TrimEnd(';');
            nS += Environment.NewLine ; 

            return nS ;
        }
    }
    /// <summary>
    /// 检验结果项
    /// </summary>
    public enum CheckResultCode
    {
        OK,
        NG
    }
    /// <summary>
    /// 功能码枚举
    /// </summary>
    public enum FunCode
    {
        /// <summary>
        /// 是否具备开工条件
        /// </summary>
        ISR,
        /// <summary>
        /// 是否继续
        /// </summary>
        ISN,
        /// <summary>
        /// 上工序校验
        /// </summary>
        SNC,
        /// <summary>
        /// 常规过站        
        /// </summary>
        PRC,
        /// <summary>
        /// 常规过站        
        /// </summary>
        BPC,
        /// <summary>
        /// 检验工序过站
        /// </summary>
        VIC,
        /// <summary>
        /// 组装工序过站 
        /// </summary>
        BMC
    }


    /// <summary>
    /// 工站
    /// </summary>
    public enum StationCode
    {
        /// <summary>
        /// 面料上料
        /// </summary>
        [Description("0101")]
        MaterialFilling = 0101,
        /// <summary>
        /// 面料上料
        /// </summary>
        [Description("0102")]
        MaterialInWarehouse = 0102,
        /// <summary>
        /// 面料上料
        /// </summary>
        [Description("0103")]
        MaterialOutWarehouse = 0103,
        /// <summary>
        /// 裁片上料
        /// </summary>
        [Description("0201")]
        PartFilling = 0201,
        /// <summary>
        /// 裁片入备料仓
        /// </summary>
        [Description("0202")]
        PartInReadyhouse = 0202,
        /// <summary>
        /// 裁片入缓存仓
        /// </summary>
        [Description("0203")]
        PartInCachehouse = 0203


    }

    /// <summary>
    /// 通讯代码
    /// </summary>
    public enum CommunicationCode
    {
        /// <summary>
        /// 握手：询问
        /// </summary>
        [Description("1010")]
        Ask = 1010,
        /// <summary>
        /// 握手：回复
        /// </summary>
        [Description("1011")]
        Ask_R = 1011,

        /// <summary>
        /// 数据传输：过程数据
        /// </summary>
        [Description("2030")]
        ProcessData = 2030,
        /// <summary>
        /// 数据传输：回复
        /// </summary>
        [Description("2031")]
        ProcessData_R = 2031,

        /// <summary>
        /// 完成：结果数据
        /// </summary>
        [Description("2010")]
        ResultData = 2010,
        /// <summary>
        /// 完成：回复
        /// </summary>
        [Description("2011")]
        ResultData_R = 2011,

        /// <summary>
        /// 心跳
        /// </summary>
        [Description("999")]
        Heart = 999,

        /// <summary>
        /// 心跳
        /// </summary>
        [Description("0000")]
        Error = 0000
    }
}
