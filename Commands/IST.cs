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
    /// 产品是否进工位请求处理类
    /// </summary>
    public class IST : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            if (!GlobalData.CheckMesRequestInfo(session, requestInfo, CheckType.DeviceSN)) return;
             
            if (requestInfo.TData.EquipmentID == GlobalData.CH_3_DeviceID)//彩盒线齐套---[是否进该工位请求] 
            { 
                DelegateState.ServerStateInfo?.Invoke(" 接收 >> " + requestInfo.Data);
                Console.WriteLine(DateTime.Now.ToString() + " 接收>> " + requestInfo.Data); 
                IsIntoStation(session, requestInfo.TData); 
            }
            else
            {
                GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "The current input Equipment:" + requestInfo.TData.EquipmentID + " is not in station ;///");
                session.Logger.Error("当前设备[" + requestInfo.TData + "]不应出现在该工站!");
                return;
            } 
        }

        #region 彩盒线业务逻辑处理

        /// <summary>
        /// 彩盒是否进入该工位
        /// </summary>
        /// <param name="sN"></param>
        private void IsIntoStation(MesSession _session, TransmitData _transData)
        {
            string stationID = _transData.TestItems["stationID"];
            // TODO：彩盒线：01 - 是否进该工位检查
            bool bInto = IntoStation(_transData.SN, stationID);
            if (bInto)
            {
                _transData.CheckResult = CheckResult.OK.ToString();
            }
            else
            {
                _transData.CheckResult = CheckResult.NG.ToString();
            }
            string msg = MethodBase.GetCurrentMethod().DeclaringType.Name + " " + JsonHelper.Serialize(_transData) + Environment.NewLine;
            _session.Send(msg);
            _session.Logger.Info(" 发送 >> " + msg);
            DelegateState.ServerStateInfo?.Invoke(" 发送 >> " + msg);
            Console.WriteLine(DateTime.Now.ToString() + " 发送>> " + msg);
        }

        private bool IntoStation(string sN, string stationID)
        {
            return true;
        }


        #endregion
    }
}
