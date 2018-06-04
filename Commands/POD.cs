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
    /// 产品装配子件位置请求处理类
    /// </summary>
    public class POD : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            if (!GlobalData.CheckMesRequestInfo(session, requestInfo, CheckType.DeviceSN)) return;

            DelegateState.ServerStateInfo?.Invoke(" 接收 >> " + requestInfo.Data);
            Console.WriteLine(DateTime.Now.ToString() + " 接收 >> " + requestInfo.Data);

            if (requestInfo.TData.EquipmentID == GlobalData.CH_3_DeviceID)//彩盒线齐套---[个性化瓶位置请求] 
            {
                GetMaterialPosition(session, requestInfo.TData);
            }
            else if (requestInfo.TData.EquipmentID == GlobalData.GXH_10_DeviceID)//个性化瓶进仓---[个性化瓶位置请求] 
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
        /// 个性化线：06 - 分流判断
        /// </summary>
        /// <param name="sN"></param>        
        private void GXHProcess(MesSession session, TransmitData data)
        {
            try
            {
                //TODO：个性化线：08 - 瓶子进仓仓位请求
                DataTable dt = GetBottlePosition(data.SN);
                if (dt.Rows.Count > 0) // 成功 
                { 
                    data.TestItems.Add("bottleType", dt.Rows[0][0].ToString());
                    data.TestItems.Add("warehouse", dt.Rows[0][1].ToString());
                    data.TestItems.Add("house", dt.Rows[0][2].ToString());
                    data.TestItems.Add("part", dt.Rows[0][3].ToString());
                    data.CheckResult = CheckResult.OK.ToString();
                }

                else
                {
                    data.CheckResult = CheckResult.NG.ToString();
                }
            }
            catch (Exception e)
            {
                data.CheckResult = CheckResult.NG.ToString();
                data.Description = e.Message;
            }

            string msg = MethodBase.GetCurrentMethod().DeclaringType.Name + " " + JsonHelper.Serialize(data) + Environment.NewLine;
            session.Send(msg);
            session.Logger.Info(" 发送 >> " + msg);
            DelegateState.ServerStateInfo?.Invoke(" 发送 >> " + msg);
            Console.WriteLine(DateTime.Now.ToString() + " 发送>> " + msg);
        }

        private DataTable GetBottlePosition(string sN)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("bottleType");
            dt.Columns.Add("warehouse");
            dt.Columns.Add("house");
            dt.Columns.Add("part");

            DataRow dr = dt.NewRow();
            dr[0] = "30ml";
            dr[1] = "1";
            dr[2] = "2";
            dr[3] = "2";

            dt.Rows.Add(dr);
            return dt;
        }

        #endregion

        #region 彩盒线业务逻辑处理

        /// <summary>
        /// 个性化瓶位置请求
        /// </summary>
        /// <param name="sN"></param>
        private void GetMaterialPosition(MesSession _session, TransmitData _transData)
        {
            string stationID = _transData.TestItems["stationID"];

            // TODO：彩盒线：02 - 个性化瓶仓位信息下发
            DataTable dt = GetMaterialPosition(_transData.SN, stationID);
            if (dt.Rows.Count != 0) // 三种状态，该工位无需再取，齐套完成，继续取瓶
            {
                DataRow dr = dt.Rows[0];
                _transData.TestItems.Add("take", "true");
                _transData.TestItems.Add("warehouse", dr["warehouse"].ToString());
                _transData.TestItems.Add("part", dr["part"].ToString());
                _transData.TestItems.Add("pos", dr["pos"].ToString());
                _transData.TestItems.Add("boxType", dr["boxType"].ToString());
                _transData.TestItems.Add("bottleType", dr["bottleType"].ToString());

            }
            else
            {
                _transData.TestItems.Add("take", "false");
            }

            string msg = MethodBase.GetCurrentMethod().DeclaringType.Name + " " + JsonHelper.Serialize(_transData) + Environment.NewLine;
            _session.Send(msg);
            _session.Logger.Info(" 发送 >> " + msg);
            DelegateState.ServerStateInfo?.Invoke(" 发送 >> " + msg);
            Console.WriteLine(DateTime.Now.ToString() + " 发送>> " + msg);
        }

        private DataTable GetMaterialPosition(string sN, string stationID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("warehouse");
            dt.Columns.Add("part");
            dt.Columns.Add("pos");
            dt.Columns.Add("boxType");
            dt.Columns.Add("bottleType");

            DataRow dr = dt.NewRow();
            dr["warehouse"] = "1";
            dr["part"] = "1";
            dr["pos"] = "1";
            dr["boxType"] = "8";
            dr["bottleType"] = "30ml";

            dt.Rows.Add(dr);
            return dt;
        }


        #endregion
    }
}
