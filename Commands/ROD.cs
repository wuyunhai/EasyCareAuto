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
    /// 个性化线瓶子分流请求类
    /// </summary>
    public class ROD : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            if (!GlobalData.CheckMesRequestInfo(session, requestInfo, CheckType.DeviceSN)) return;

            DelegateState.ServerStateInfo?.Invoke(" 接收 >> " + requestInfo.Data);
            Console.WriteLine(DateTime.Now.ToString() + " 接收 >> " + requestInfo.Data);
            if (requestInfo.TData.EquipmentID == GlobalData.GXH_8_DeviceID)
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
                //TODO：个性化线：07 - 瓶子分流请求
                int iType = BottleShunt(data.SN);
                if (iType > 0 && iType < 5) // 成功 
                {
                    data.CheckResult = CheckResult.OK.ToString();
                    data.TestItems.Add("routeType", iType.ToString());
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

            // 虚拟旋盖过站
            GlobalData.CheckRoute(data, GlobalData.GXH_7_DeviceID);
            // 条码校验，瓶子分流过站
            GlobalData.CheckRoute(data, "");

            string msg = MethodBase.GetCurrentMethod().DeclaringType.Name + " " + JsonHelper.Serialize(data) + Environment.NewLine;
            session.Send(msg);
            session.Logger.Info(" 发送 >> " + msg);
            DelegateState.ServerStateInfo?.Invoke(" 发送 >> " + msg);
            Console.WriteLine(DateTime.Now.ToString() + " 发送>> " + msg);
        }

        /// <summary>
        /// 判断瓶子SN条码属性：1-NG类条码；2-抽检类条码；3-尾数类条码；4-正常条码;
        /// </summary>
        /// <param name="sN"></param>
        /// <returns></returns>
        private int BottleShunt(string sN)
        {
            Random r = new Random();
            return r.Next(1, 4);
        }

        private bool IsEnd(string sN)
        {
            return false;
        }

        #endregion
    }
}
