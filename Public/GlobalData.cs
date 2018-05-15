using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.SocketService
{
    public static class GlobalData
    {
        #region var

        //当前工序类型
        public static string Process;

        /// <summary>
        ///  上瓶设备ID
        /// </summary>
        public static string SpDeviceID;
        /// <summary>
        ///  个性化设备ID
        /// </summary>
        public static string GXDeviceID;
        /// <summary>
        ///  彩盒设备ID
        /// </summary>
        public static string CHDeviceID;
        /// <summary>
        ///  包装设备ID
        /// </summary>
        public static string BZDeviceID;
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

        public static string ProtocolFormalError = "通讯协议错误(正确格式为：三位功能码 + 空格 + json字符串 + 回车换行符号).";

        #region 校验接收数据是否正确

        /// <summary>
        /// 校验接收数据是否正确
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        /// <param name="recvTransData"></param>
        /// <returns></returns>
        public static TransmitData CheckRecvData(MesSession session, StringRequestInfo requestInfo, TransmitData recvTransData)
        {
            try
            {
                if (requestInfo.Parameters.Count() == 1)
                {
                    recvTransData = JsonHelper.DeserializeJsonToObject<TransmitData>(requestInfo.Body);
                }
                else
                {
                    session.Logger.Error(GlobalData.ProtocolFormalError);
                    return null;
                }
            }
            catch (Exception e)
            {
                session.Logger.Error("json 序列化错误:" + e.Message + "，json字符串：" + requestInfo.Body);
                return null;
            }

            return recvTransData;
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
            _transData.CheckResult = "NG";
            _transData.Description = _keyWord + " is null Or white space.";
            string sendTransData = JsonHelper.SerializeObject(_transData);
            _session.Send(sendTransData);
            _session.Logger.Error(_transData.CheckResult + "---" + _transData.Description);
        }

        #endregion


        /// <summary>
        /// 读取配置参数
        /// </summary>
        public static void LoadConfig()
        {
            //读取配置参数
            SpDeviceID = CfgHelper.GetKeyValue("SpDeviceID");
            GXDeviceID = CfgHelper.GetKeyValue("GXDeviceID");
            CHDeviceID = CfgHelper.GetKeyValue("CHDeviceID");
            BZDeviceID = CfgHelper.GetKeyValue("BZDeviceID");
        }
    }

    public enum CheckResult
    {
        OK,
        NG,
    }
}
