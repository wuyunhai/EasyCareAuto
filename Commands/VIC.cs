using Bucklematerial;
using DM_API;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MES.SocketService
{
    public class VIC : CommandBase<MesSession, StringRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, StringRequestInfo requestInfo)
        {
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
            }
            else
            {
                GlobalData.KeyWordIsNullRecv(session, recvTransData, "EquipmentID");
                session.Logger.Error("未能识别该设备!");
                return;
            }

        }

        #region 上瓶机位业务逻辑处理

        /// <summary>
        /// 上瓶机位业务逻辑处理:3-称重校验；4-是否继续上瓶判断
        /// </summary>
        /// <param name="sN"></param>
        private void SpDeviceProcess(MesSession _session, TransmitData _transData)
        {
            //3-称重校验
            bool weightIsOK = CheckWeight(_transData.SN, _transData.Items["aWeight"]);
            if (weightIsOK)
            {
                _transData.CheckResult = CheckResult.OK.ToString();

                //4-是否继续上瓶判断
                bool isContinue = IsContinue(_transData.WO); 
                _transData.Items.Add("sWeight", isContinue.ToString());


                //5-处理完成，过站
                DM_SFCInterface DM_SFC = new DM_SFCInterface();
                DataTable dt = DM_SFC.SFC_DM_CheckRoute(_transData.SN, _transData.EquipmentID, _transData.WO, "PASS");//FAIL
                string CheckStatus = dt.Rows[0][0].ToString().ToString();
                string ReturnMsg = dt.Rows[0][1].ToString().ToString();
                if (CheckStatus == "1") //过站成功，开始扣料 
                {
                    DM_Bucklematerial bucklematerial = new DM_Bucklematerial();
                    bucklematerial.BuckleMaterialIn(_transData.SN, "ASM", _transData.EquipmentID + "-01");
                }
                else
                {
                    _transData.CheckResult = "ERROR";
                    _transData.Description = ReturnMsg;
                }
            }
            else
            {
                _transData.CheckResult = "NG";
                _transData.Description = "error---query fail,please check SN.";
            }
            string sendTransData = JsonHelper.SerializeObject(_transData);
            _session.Send(sendTransData);
            _session.Logger.Info(_transData.CheckResult + "---" + _transData.Description);
        }

        /// <summary>
        /// 是否继续上瓶
        /// </summary>
        /// <param name="wO"></param>
        /// <returns></returns>
        private bool IsContinue(string wO)
        {
            // TODO:是否继续上瓶
            return true;
        }

        /// <summary>
        /// 基液称重校验
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        private bool CheckWeight(string sn, string weight)
        {
            // TODO:校验基液重量是否合格
            return true;
        }


        #endregion
    }
}
