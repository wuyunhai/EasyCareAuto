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
    /// 产品AOI校验结果返回请求处理类
    /// </summary>
    public class AOI : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            if (!GlobalData.CheckMesRequestInfo(session, requestInfo, CheckType.DeviceSN)) return;
 
            DelegateState.ServerStateInfo?.Invoke(" 接收 >> " + requestInfo.Data);
            Console.WriteLine(DateTime.Now.ToString() + " 接收 >> " + requestInfo.Data);

            if (requestInfo.TData.EquipmentID == GlobalData.CH_3_DeviceID)//彩盒线齐套---[个性化瓶装配校验请求] 
            {
                MaterialAOICheck(session, requestInfo.TData);
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
        /// TODO：彩盒线：04 - AOI校验
        /// </summary>
        /// <param name="sN"></param>
        private void MaterialAOICheck(MesSession _session, TransmitData _transData)
        { 
            string msg = MethodBase.GetCurrentMethod().DeclaringType.Name + " " + JsonHelper.Serialize(_transData) + Environment.NewLine;
            _session.Send(msg);
            _session.Logger.Info(" 发送 >> " + msg);
            DelegateState.ServerStateInfo?.Invoke(" 发送 >> " + msg);
            Console.WriteLine(DateTime.Now.ToString() + " 发送>> " + msg);
        }

        #endregion
    }
}
