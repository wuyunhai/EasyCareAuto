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
    /// 尾数判断请求类
    /// </summary>
    public class ISE : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            if (!GlobalData.CheckMesRequestInfo(session, requestInfo, CheckType.DeviceSN)) return;

            DelegateState.ServerStateInfo?.Invoke(" 接收 >> " + requestInfo.Data);
            Console.WriteLine(DateTime.Now.ToString() + " 接收 >> " + requestInfo.Data);
            if (requestInfo.TData.EquipmentID == GlobalData.GXH_4_DeviceID)
            {
                GXHProcess(session, requestInfo.TData);
            }
            else
            {
                GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "EquipmentID");
                session.Logger.Error("未能识别该设备!");
                return;
            }
        }

        #region 个性化注液业务逻辑处理

        /// <summary>
        /// 个性化线：03 - 尾数判断
        /// </summary>
        /// <param name="sN"></param>        
        private void GXHProcess(MesSession session, TransmitData data)
        {
            try
            {
                //TODO：个性化线：03 - 尾数判断
                bool bIsEnd = BottleIsEnd(data.SN);
                if (bIsEnd) // 成功 
                {
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

        private bool BottleIsEnd(string sN)
        {
            return false;
        }

        #endregion
    }
}
