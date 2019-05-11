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
    public class ASK : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            //if (!GlobalData.CheckMesRequestInfo(session, requestInfo, CheckType.UID)) return;

            StationCode eStationID = EnumHelper.GetInstance<StationCode>(requestInfo.TData.StationID);

            switch (eStationID)
            {
                case StationCode.MaterialFilling:
                    MaterialFilling(session, requestInfo.TData);
                    Thread.Sleep(10);
                    MaterialFilling_ResultData(session, requestInfo.TData);
                    break;

                case StationCode.MaterialInWarehouse:
                    MaterialInWarehouse(session, requestInfo.TData);

                    Thread.Sleep(10);
                    MaterialFilling_ProcessData(session, requestInfo.TData);
                    break;

                case StationCode.MaterialOutWarehouse:
                    MaterialOutWarehouse_ASK_R(session, requestInfo.TData);
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

        #region 面料上料

        /// <summary>
        /// TODO：面料上料
        /// </summary>
        /// <param name="sN"></param>
        private void MaterialFilling(MesSession _session, TransmitData _transData)
        {
            _transData.CommuCode = EnumHelper.GetMemberValue<CommunicationCode>(CommunicationCode.Ask_R.ToString()).ToString();
            _transData.CheckResult = CheckResult.OK.ToString();

            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);


            string logMsg = GlobalData.Pre_Send + msg;
            _session.Logger.Info(logMsg);
            GlobalData.ViewLog(logMsg);

        }

        /// <summary>
        /// WMS获取RFID成功
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void MaterialFilling_ResultData(MesSession _session, TransmitData _transData)
        {
            _transData.CommuCode = EnumHelper.GetMemberValue<CommunicationCode>(CommunicationCode.ResultData.ToString()).ToString();
            _transData.CheckResult = null;

            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);

            string logMsg = GlobalData.Pre_Send + msg;
            _session.Logger.Info(logMsg);
            GlobalData.ViewLog(logMsg);

        }

        #endregion

        #region 面料入库

        /// <summary>
        /// FAST感应到RFID发送握手请求给WMS，WMS答复
        /// </summary>
        /// <param name="sN"></param>
        private void MaterialInWarehouse(MesSession _session, TransmitData _transData)
        {
            _transData.CommuCode = EnumHelper.GetMemberValue<CommunicationCode>(CommunicationCode.Ask_R.ToString()).ToString();
            _transData.CheckResult = CheckResult.OK.ToString();

            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);

            string logMsg = GlobalData.Pre_Send + msg;
            _session.Logger.Info(logMsg);
            GlobalData.ViewLog(logMsg);

        }


        /// <summary>
        /// WMS获取RFID成功
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void MaterialFilling_ProcessData(MesSession _session, TransmitData _transData)
        {
            _transData.CommuCode = EnumHelper.GetMemberValue<CommunicationCode>(CommunicationCode.ProcessData.ToString()).ToString();
            _transData.StorageId = "01";//获取栈道号
            _transData.CheckResult = null;

            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);

            string logMsg = GlobalData.Pre_Send + msg;
            _session.Logger.Info(logMsg);
            GlobalData.ViewLog(logMsg);

        }


        #endregion

        #region 面料出库
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void MaterialOutWarehouse_ASK_R(MesSession _session, TransmitData _transData)
        {
            _transData.CommuCode = EnumHelper.GetMemberValue<CommunicationCode>(CommunicationCode.Ask_R.ToString()).ToString();
            _transData.CheckResult = CheckResult.OK.ToString();

            //_session.Send(msg);  
            string msg = GlobalData.ToTranString(_transData);
            int i = 0;
     
            foreach (var session in _session.AppServer.GetAllSessions())
            {
                i++;
                session.Send(i + "---" + msg);

                string logMsg = GlobalData.Pre_Send + i + "---" + session.RemoteEndPoint.ToString() + msg;
                _session.Logger.Info(logMsg);
                GlobalData.ViewLog(logMsg);
            }


        }
        #endregion

        #endregion


    }
}
