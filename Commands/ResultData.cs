using Bucklematerial;
using DM_API;
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
    /// 产品AOI校验结果返回请求处理类
    /// </summary>
    public class ResultData : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            if (!GlobalData.CheckMesRequestInfo(session, requestInfo, CheckType.UID)) return;

            StationCode eStationID = EnumHelper.GetInstance<StationCode>(requestInfo.TData.StationID);

            switch (eStationID)
            {
                case StationCode.MaterialFilling:

                    break;

                case StationCode.MaterialInWarehouse:
                    MaterialInWarehouse(session, requestInfo.TData);

                    break;

                case StationCode.MaterialOutWarehouse:
                    break;

                case StationCode.PartFilling:
                    break;

                case StationCode.PartInReadyhouse:
                    break;

                case StationCode.PartInCachehouse:
                    break;

                default:
                    GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "The current input Equipment:" + requestInfo.TData.StationID + " is not in station ;///");
                    session.Logger.Error("当前设备[" + requestInfo.TData + "]不应出现在该工站!");
                    break;
            }
        }

        #region 业务逻辑处理

        #region 面料入库

        /// <summary>
        /// FAST感应到RFID发送握手请求给WMS，WMS答复
        /// </summary>
        /// <param name="sN"></param>
        private void MaterialInWarehouse(MesSession _session, TransmitData _transData)
        {
            _transData.CommuCode = EnumHelper.GetMemberValue<CommunicationCode>(CommunicationCode.ResultData_R.ToString()).ToString();
            _transData.CheckResult = CheckResult.OK.ToString();

            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);

            string logMsg = GlobalData.Pre_Send + msg;
            _session.Logger.Info(logMsg);
            GlobalData.ViewLog(logMsg);  
        }



        #endregion

        #endregion

    }
}
