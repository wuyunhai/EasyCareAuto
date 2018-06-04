using Bucklematerial;
using DM_API;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MES.SocketService
{
    /// <summary>
    /// 个性化注液数据请求
    /// </summary>
    public class SED : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            if (!GlobalData.CheckMesRequestInfo(session, requestInfo, CheckType.Device)) return;

            DelegateState.ServerStateInfo?.Invoke(" 接收 >> " + requestInfo.Data);
            Console.WriteLine(DateTime.Now.ToString() + " 接收 >> " + requestInfo.Data);
            if (requestInfo.TData.EquipmentID == GlobalData.GXH_4_DeviceID)
            {
                GXHProcess(session, requestInfo.TData);
            }
            else
            {
                GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "The current input Equipment:" + requestInfo.TData.EquipmentID + " is not in station ;///");
                session.Logger.Error("当前设备[" + requestInfo.TData + "]不应出现在该工站!");
                return;
            }
        }

        #region 个性化注液业务逻辑处理

        /// <summary>
        /// 个性化线：02 - 各注液位剂量信息下发
        /// </summary>
        /// <param name="sN"></param>        
        private void GXHProcess(MesSession session, TransmitData data)
        {
            try
            {
                //TODO：个性化线：02 - 各注液位剂量信息下发
                DataTable dtReturn = new DataTable();
                dtReturn.Columns.Add("position");//注液泵位置
                dtReturn.Columns.Add("sn");//对应瓶子条码
                dtReturn.Columns.Add("dose");//注液剂量
                dtReturn.Columns.Add("density");//注液密度
                dtReturn.Columns.Add("absorb");//回吸量

                foreach (var item in data.TestItems)
                {
                    DataRow dr = dtReturn.NewRow();
                    dr[0] = item.Key;
                    dr[1] = item.Value;
                    dtReturn.Rows.Add(dr);
                }

                DataTable dtSend = GetIndividuationDose(dtReturn);
                data.TestItems.Clear();
                foreach (DataRow dr in dtSend.Rows)
                {
                    data.TestItems.Add(dr[0].ToString(), dr[2] + "_" + dr[3] + "_" + dr[4]);
                }
                data.CheckResult = CheckResult.OK.ToString();
            }
            catch (Exception e)
            {
                data.CheckResult = CheckResult.NG.ToString();
                data.Description = e.Message;
            }

            // ByPass过站
            GlobalData.CheckRoute(data, "");

            string msg = MethodBase.GetCurrentMethod().DeclaringType.Name + " " + JsonHelper.Serialize(data) + Environment.NewLine;
            session.Send(msg);
            session.Logger.Info(" 发送 >> " + msg);
            DelegateState.ServerStateInfo?.Invoke(" 发送 >> " + msg);
            Console.WriteLine(DateTime.Now.ToString() + " 发送>> " + msg);
        }

        /// <summary>
        /// 查询个性化液灌装剂量-密度-回吸量
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DataTable GetIndividuationDose(DataTable dt)
        {
            Random r1 = new Random(0);
            foreach (DataRow dr in dt.Rows)
            {
                dr[2] = r1.Next(10, 25);
                dr[3] = r1.Next(1, 3);
                dr[4] = r1.Next(3, 6);
            }
            return dt;
        }

        #endregion
    }
}
