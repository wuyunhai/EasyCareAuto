﻿using Bucklematerial;
using DM_API;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MES.SocketService
{
    // 称重校验
    public class VIC : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            if (!GlobalData.CheckMesRequestInfo(session, requestInfo, CheckType.DeviceSN)) return;

            DelegateState.ServerStateInfo?.Invoke(" 接收 >> " + requestInfo.Data);
            Console.WriteLine(DateTime.Now.ToString() + " 接收 >> " + requestInfo.Data);
            if (requestInfo.TData.EquipmentID == GlobalData.GXH_6_DeviceID)
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
        /// 个性化线：04 - 称重校验判断
        /// </summary>
        /// <param name="sN"></param>        
        private void GXHProcess(MesSession session, TransmitData data)
        {
            try
            {
                //TODO：个性化线：04 - 称重校验
                double dWeight = double.Parse(data.TestItems["weight"]);
                bool bIsOK = WeightCheck(data.SN, dWeight);
                if (bIsOK) // 成功 
                {
                    data.CheckResult = CheckResult.OK.ToString();

                    //TODO：个性化线：05 - 查询瓶盖信息
                    DataTable dt = GetCoverInfo(data.SN);

                    data.TestItems.Clear();

                    data.TestItems.Add("cover", dt.Rows[0][0].ToString());
                    data.TestItems.Add("pumpingCover", dt.Rows[0][1].ToString());
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

            // 虚拟搅拌过站{实际设备并未返回该节点信号}
            GlobalData.CheckRoute(data,GlobalData.GXH_5_DeviceID);
            // 称重过站
            GlobalData.CheckRoute(data, "");

            string msg = MethodBase.GetCurrentMethod().DeclaringType.Name + " " + JsonHelper.Serialize(data) + Environment.NewLine;
            session.Send(msg);
            session.Logger.Info(" 发送 >> " + msg);
            DelegateState.ServerStateInfo?.Invoke(" 发送 >> " + msg);
            Console.WriteLine(DateTime.Now.ToString() + " 发送>> " + msg);
        }

        /// <summary>
        /// 获取瓶盖信息
        /// </summary>
        /// <param name="data"></param>
        private DataTable GetCoverInfo(string data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cover");
            dt.Columns.Add("pumpingCover");

            DataRow dr = dt.NewRow();
            dr["cover"] = "C023568";
            dr["pumpingCover"] = "CP023568";
            dt.Rows.Add(dr);

            return dt;
        }

        /// <summary>
        /// 称重校验
        /// </summary>
        /// <param name="sN"></param>
        /// <param name="dWeight"></param>
        /// <returns></returns>
        private bool WeightCheck(string sN, double dWeight)
        {
            return true;
        }

        #endregion
    }
}
