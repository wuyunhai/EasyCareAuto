using DM_API;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MES.SocketService
{
    public static class GlobalData
    {
        #region var

        /// <summary>
        ///  个性化上瓶
        /// </summary>
        public static string GXH_1_DeviceID;
        /// <summary>
        ///  个性化基液灌装
        /// </summary>
        public static string GXH_2_DeviceID;
        /// <summary>
        ///  个性化基液称重
        /// </summary>
        public static string GXH_3_DeviceID;
        /// <summary>
        /// 个性化灌装
        /// </summary>
        public static string GXH_4_DeviceID;
        /// <summary>
        /// 个性化搅拌
        /// </summary>
        public static string GXH_5_DeviceID;
        /// <summary>
        /// 个性化称重
        /// </summary>
        public static string GXH_6_DeviceID;
        /// <summary>
        /// 个性化旋盖
        /// </summary>
        public static string GXH_7_DeviceID;
        /// <summary>
        /// 个性化分流
        /// </summary>
        public static string GXH_8_DeviceID;
        /// <summary>
        /// 个性化仓库分配
        /// </summary>
        public static string GXH_9_DeviceID;
        /// <summary>
        /// 个性化入库
        /// </summary>
        public static string GXH_10_DeviceID;


        /// <summary>
        ///  彩盒线_上彩盒_设备ID
        /// </summary>
        public static string CH_1_DeviceID;
        /// <summary>
        ///  彩盒线_通用料_设备ID
        /// </summary>
        public static string CH_2_DeviceID;
        /// <summary>
        ///  彩盒线_齐套_设备ID
        /// </summary>
        public static string CH_3_DeviceID;

        #endregion

        //配置文件助手
        public static ConfigHelper CfgHelper = null;

        /// <summary>
        /// 初始化全局数据
        /// </summary>
        public static void InitGlobalData()
        {
            //配置文件对象
            CfgHelper = new ConfigHelper();
            LoadConfig();
        }

        public const string ProtocolFormalError = "通讯协议错误(正确格式为：三位功能码 + 空格 + json字符串 + 回车换行符号).";

        #region 校验接收数据是否正确

        /// <summary>
        /// 校验接收数据是否正确
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        /// <param name="recvTransData"></param>
        /// <returns></returns>
        public static bool CheckMesRequestInfo(MesSession session, MesRequestInfo requestInfo, CheckType checkType)
        {
            if (requestInfo.TData == null)
            {
                GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "RequestInfo");
                session.Logger.Error("RequestInfo is not json format !");
                return false;
            }

            if (string.IsNullOrWhiteSpace(requestInfo.TData.EquipmentID))
            {
                GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "EquipmentID");
                session.Logger.Error("EquipmentID error !");
                return false;
            }
            if (checkType == CheckType.DeviceSN && string.IsNullOrWhiteSpace(requestInfo.TData.SN))
            {
                GlobalData.KeyWordIsNullRecv(session, requestInfo.TData, "SN");
                session.Logger.Error("SN error !");
                return false;
            }
            return true;
        }

        #endregion

        #region 关键字段为空时处理方法

        /// <summary>
        /// 关键字段为空时处理方法
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="_keyWord"></param>
        public static void KeyWordIsNullRecv(MesSession _session, TransmitData _transData, string _keyWord)
        {

            _transData.CheckResult = CheckResult.ERROR.ToString();
            _transData.Description = _keyWord + " is null or white space or format error ?";
            string msg = _transData.Func + " " + JsonHelper.Serialize(_transData) + Environment.NewLine;
            _session.Send(msg);
            _session.Logger.Error(_transData.CheckResult + "---" + _transData.Description);
        }

        #endregion

        #region 【上工序校验】

        /// <summary>
        /// 上工序校验
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool CheckRouteOnlyCheck(TransmitData data)
        {
            try
            {
                //上工序校验
                DM_SFCInterface DM_SFC = new DM_SFCInterface();
                DataTable dt = DM_SFC.SFC_DM_CheckRouteOnlyCheck(data.SN, data.EquipmentID, "", "");//FAIL
                string CheckStatus = dt.Rows[0][0].ToString().ToString();
                string ReturnMsg = dt.Rows[0][1].ToString().ToString();
                if (CheckStatus == "1") // 成功 
                {
                    data.CheckResult = CheckResult.OK.ToString();
                    return true;
                }
                else
                {
                    data.CheckResult = CheckResult.ERROR.ToString();
                    data.Description = ReturnMsg;
                    return false;
                }
            }
            catch (Exception e)
            {
                data.CheckResult = CheckResult.ERROR.ToString();
                data.Description = e.Message;
                return false;
            }
        }

        #endregion 

        #region 【ByPass工序过站】

        /// <summary>
        /// 上工序校验
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool CheckRoute(TransmitData data, string deviceID)
        {
            try
            {
                //上工序校验
                DM_SFCInterface DM_SFC = new DM_SFCInterface();
                DataTable dt = DM_SFC.SFC_DM_CheckRoute(data.SN, string.IsNullOrWhiteSpace(deviceID) ? data.EquipmentID : deviceID, "", "PASS");//FAIL
                string CheckStatus = dt.Rows[0][0].ToString().ToString();
                string ReturnMsg = dt.Rows[0][1].ToString().ToString();
                if (CheckStatus == "1") // 成功 
                {
                    data.CheckResult = CheckResult.OK.ToString();
                    return true;
                }
                else
                {
                    data.CheckResult = CheckResult.ERROR.ToString();
                    data.Description = ReturnMsg;
                    return false;
                }
            }
            catch (Exception e)
            {
                data.CheckResult = CheckResult.ERROR.ToString();
                data.Description = e.Message;
                return false;
            }
        }

        #endregion 

        /// <summary>
        /// 读取配置参数
        /// </summary>
        public static void LoadConfig()
        {
            //读取配置参数
            GXH_4_DeviceID = CfgHelper.GetKeyValue("GXH_4_DeviceID");
            GXH_5_DeviceID = CfgHelper.GetKeyValue("GXH_5_DeviceID");
            GXH_6_DeviceID = CfgHelper.GetKeyValue("GXH_6_DeviceID");
            GXH_7_DeviceID = CfgHelper.GetKeyValue("GXH_7_DeviceID");
            GXH_8_DeviceID = CfgHelper.GetKeyValue("GXH_8_DeviceID");
            GXH_9_DeviceID = CfgHelper.GetKeyValue("GXH_9_DeviceID");
            GXH_10_DeviceID = CfgHelper.GetKeyValue("GXH_10_DeviceID");

            CH_1_DeviceID = CfgHelper.GetKeyValue("CH_1_DeviceID");
            CH_2_DeviceID = CfgHelper.GetKeyValue("CH_2_DeviceID");
            CH_3_DeviceID = CfgHelper.GetKeyValue("CH_3_DeviceID");
        }
    }

    public enum CheckResult
    {
        OK,
        NG,
        ERROR
    }
    public enum CheckType
    {
        DeviceSN,
        Device
    }
}
