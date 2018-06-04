using System.Collections.Generic;
using System.Text;

namespace MES.SocketService
{
    //MES-EQC传输数据格式
    public class TransmitData
    {
        public TransmitData()
        {
            TestItems = new Dictionary<string, string>();
        }
        /// <summary>
        /// 功能码
        /// </summary>
        public string Func { get; set; }
        /// <summary>
        /// 制令单号
        /// </summary>
        public string WO { get; set; }
        /// <summary>
        /// 设备编码 
        /// </summary>
        public string EquipmentID { get; set; }
        /// <summary>
        /// 产品条码
        /// </summary>
        public string SN { get; set; }
        /// <summary>
        /// 装配件条码
        /// </summary> 
        public string PartCode { get; set; }
        /// <summary>
        /// 结果（校验/装配/...）
        /// </summary> 
        public string CheckResult { get; set; }
        /// <summary>
        /// 描述（如果结果为NG,需描述原因）
        /// </summary> 
        public string Description { get; set; }
        /// <summary>
        /// 存储测试项/检验项等
        /// </summary>
        public Dictionary<string, string> TestItems { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("TransmitData----------------------------------------\r\n");
            sb.Append("EquipmentID:" + EquipmentID + "\r\n");
            sb.Append("SN:" + SN + "\r\n");
            sb.Append("PartCode:" + PartCode + "\r\n");
            sb.Append("CheckResult:" + CheckResult + "\r\n");
            sb.Append("Description:" + Description + "\r\n");
            sb.Append("----------------$-------------------------\r\n");
            foreach (var item in TestItems)
            {

                sb.Append(item.Key + ":" + item.Value + "\r\n");
            }

            return sb.ToString();

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
}
