using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.SocketService
{
    /// <summary>
    /// 测试项数据
    /// </summary>
    public class TestItemFlex
    {
        // 螺丝数据队列
        public TestItemFlex(MesSession mesSession, TransData transData, string equipmentID, string employeeName, string type)
        {

            MesSession = mesSession;
            TransData = transData;
            EquipmentID = equipmentID;

            EmployeeName = employeeName;
            Type = type;
        }
        public TestItemFlex(string sn, string devCode, string equipmentID, List<string> processData, MesSession mesSession)
        {
            SN = sn;
            DevCode = devCode;
            EquipmentID = equipmentID;
            ProcessData = processData;
            MesSession = mesSession;
        }
        public string SN { get; set; }
        public string DevCode { get; set; }
        public string EquipmentID { get; set; }
        public List<string> ProcessData { get; set; }
        public MesSession MesSession { get; set; }
        public TransData TransData { get; set; }

        public string EmployeeName { get; set; }
        public string Type { get; set; }
    }
}
