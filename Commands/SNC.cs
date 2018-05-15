using Bucklematerial;
using DM_API;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Data;
using System.Linq;

namespace MES.SocketService
{
    public class SNC : CommandBase<MesSession, StringRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, StringRequestInfo requestInfo)
        {
            // check recv data
            TransmitData recvTransData = new TransmitData();
            recvTransData = GlobalData.CheckRecvData(session, requestInfo, recvTransData);
            if (recvTransData == null) return;

            if (!string.IsNullOrWhiteSpace(recvTransData.EquipmentID))
            {
                if (recvTransData.EquipmentID == GlobalData.SpDeviceID)
                {
                    if (!string.IsNullOrWhiteSpace(recvTransData.SN))
                    {
                        SpDeviceProcess(session, recvTransData);
                    }
                    else
                    {
                        GlobalData.KeyWordIsNullRecv(session, recvTransData, "SN");
                        session.Logger.Error("SN 为空!");
                        return;
                    }
                }
                else
                {
                    GlobalData.KeyWordIsNullRecv(session, recvTransData, "EquipmentID");
                    session.Logger.Error("未能识别该设备!");
                    return;
                }
            }
            else
            {
                GlobalData.KeyWordIsNullRecv(session, recvTransData, "EquipmentID");
                return;
            }

        }

        #region 上瓶机位业务逻辑处理

        /// <summary>
        /// 上瓶机位业务逻辑处理:1-瓶身校验；2-基液灌装剂量下发
        /// </summary>
        /// <param name="sN"></param>
        private void SpDeviceProcess(MesSession _session, TransmitData _transData)
        {
            //1-瓶身校验
            bool specOK = CheckBotSpec(_transData.SN);
            if (specOK)
            {
                _transData.CheckResult = CheckResult.OK.ToString();
                //2-基液灌装查询
                string baseDose = GetBaseDose(_transData.WO);
                _transData.Items.Add("sWeight", baseDose);
            }
            else
            {
                _transData.CheckResult = CheckResult.NG.ToString();
                _transData.Description = "Check failure by sn " + _transData.SN;
            }
            string sendTransData = JsonHelper.SerializeObject(_transData);
            _session.Send(sendTransData);
            _session.Logger.Info(_transData.CheckResult + "---" + _transData.Description);
        }

        /// <summary>
        /// 校验瓶身规格
        /// </summary>
        /// <param name="sN"></param>
        /// <returns></returns>
        private bool CheckBotSpec(string sN)
        {
            //TODO:上瓶，条码（瓶身规格）校验
            return true;
        }

        /// <summary>
        /// 查询基液灌装剂量
        /// </summary>
        /// <param name="wO"></param>
        /// <returns></returns>
        private string GetBaseDose(string wO)
        {
            //TODO:查询灌装剂量
            return "50";
        }

        #endregion
    }
}
