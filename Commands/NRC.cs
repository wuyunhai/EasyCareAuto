
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    /// <summary>
    /// 【通用过站】
    /// </summary>

    public class NRC : CommandBase<MesSession, MesRequestInfo>
    {
        private string EmployeeName = "通用过站";
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            ExecuteNRC(session, requestInfo.TData);
        }

        #region 【通用过站】方法

        /// <summary>
        /// 通用过站
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void ExecuteNRC(MesSession _session, TransData _transData)
        {
            //1、参数校验---equipmentID
            string equipmentID = string.Empty;
            if (!EmployeeComm.CheckEquipmentID(_session, _transData, out equipmentID, EmployeeName))
                return;

            //2、参数校验---WorkOrder
            string workOrder = string.Empty;
            if (_transData.DeviceCode == "OP010") //首站需要校验制令单号
            {
                if (!EmployeeComm.CheckWorkOrder(_session, _transData, out workOrder, $"{EmployeeName}-工单获取", DataFrom.SQLite))
                    return;
            }

            //3、参数校验---apiStatus
            string apiStatus = string.Empty;
            if (!EmployeeComm.CheckApiStatus(_session, _transData, out apiStatus, EmployeeName))
                return;

            if (!GlobalData.IsDebug)
            {
                //4、执行过站
                if (!EmployeeComm.CheckRoute(_session, _transData, workOrder, equipmentID, apiStatus, EmployeeName))
                    return;

                //4.1 队列方式进行报工(如果记录了不良则无需进行第二次报工，因为记录不良时已经进行第一次报工了)
                if (apiStatus != "FAIL")
                    GlobalData.queueServerNDQ.EnqueueItem(new TestItemFlex(_session, _transData, equipmentID, EmployeeName, "103OUT"));
            }

            //5、API执行成功 ---------------------------------   
            EmployeeComm.SendMsg(_session, _transData, CheckResult.OK);

        }

        #endregion



    }
}
