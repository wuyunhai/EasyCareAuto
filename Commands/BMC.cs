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
    /// 产品装配子件装配校验请求处理类
    /// </summary>
    public class BMC : CommandBase<MesSession, MesRequestInfo>
    {
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            if (!GlobalData.CheckMesRequestInfo(session, requestInfo, CheckType.DeviceSN)) return;
 
            DelegateState.ServerStateInfo?.Invoke(" 接收 >> " + requestInfo.Data);
            Console.WriteLine(DateTime.Now.ToString() + " 接收 >> " + requestInfo.Data);
            if (requestInfo.TData.EquipmentID == GlobalData.CH_3_DeviceID)//彩盒线齐套---[个性化瓶装配校验请求] 
            {
                MaterialPositionCheck(session, requestInfo.TData);
            }
            else if (requestInfo.TData.EquipmentID == GlobalData.GXH_2_DeviceID)//个性化线---[旋盖结果告知请求] 
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

        private void GXHProcess(MesSession session, TransmitData data)
        {
            try
            {
                //TODO：个性化线：06 - 旋盖结果告知，{NG-MES系统做标志}
                if (data.CheckResult != CheckResult.OK.ToString())
                {
                    bool bResult = SetSNNG();
                    if (!bResult)
                    { 
                        data.CheckResult = CheckResult.NG.ToString();
                        data.Description = "Set Error.";
                    }
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

        /// <summary>
        /// 标志该条码为NG条码方法
        /// </summary>
        /// <returns></returns>
        private bool SetSNNG()
        {
            return true;
        }

        #endregion

        #region 彩盒线业务逻辑处理

        /// <summary>
        /// 个性化瓶装配校验请求
        /// </summary>
        /// <param name="sN"></param>
        private void MaterialPositionCheck(MesSession _session, TransmitData _transData)
        {
            //1-瓶身校验
            string pos = _transData.TestItems["pos"];
            string partSN = _transData.PartCode;
            bool checkResult = MaterialPositionCheck(_transData.SN, pos, partSN);
            if (checkResult) //  
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

        /// <summary>
        /// TODO：彩盒线：03 - 个性化瓶装配校验请求
        /// </summary>
        /// <param name="sN"></param>
        /// <param name="pos"></param>
        /// <param name="partSN"></param>
        /// <returns></returns>
        private bool MaterialPositionCheck(string sN, string pos, string partSN)
        {
            try
            {
                DM_SFCInterface DM_SFC = new DM_SFCInterface();
                DataTable dt = DM_SFC.SFC_DM_CheckRoute(sN, GlobalData.CH_3_DeviceID, "", "PASS");//FAIL
                string CheckStatus = dt.Rows[0][0].ToString().ToString();
                string ReturnMsg = dt.Rows[0][1].ToString().ToString();
                if (CheckStatus == "1") //过站成功，开始扣料 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }


        #endregion
    }
}
