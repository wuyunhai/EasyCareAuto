using Bucklematerial;
using DM_API;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    /// <summary>
    /// 完成、断开连接PLC回复
    /// </summary>
    public class ResultData_R : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        { 
            StationCode eStationID = EnumHelper.GetInstance<StationCode>(requestInfo.TData.StationID);

            switch (eStationID)
            {
                case StationCode.MaterialFilling:
                    break;

                case StationCode.MaterialInWarehouse:
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
                    GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "The current input StationID:" + requestInfo.TData.StationID + " is not in station ;///");
                    session.Logger.Error("当前设备[" + requestInfo.TData + "]不应出现在该工站!");
                    break;
            }
        }

    }
}
