
using LabelManager2;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    /// <summary>
    /// 【产品条码校验】
    /// </summary>
    public class PTC : CommandBase<MesSession, MesRequestInfo>
    {
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            CheckRoute(session, requestInfo.TData);
        }

        #region 【产品条码校验】方法

        /// <summary>
        /// 产品条码校验
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void CheckRoute(MesSession _session, TransData _transData)
        {
            LogInfo log = null;

            //1、获取EquipmentNumber 并校验---------------------------------
            AppConfig cfg = new AppConfig();
            string EquipmentNumber = cfg.AppConfigGet(_transData.DeviceCode + "_" + _transData.OpeIndex);
            if (string.IsNullOrEmpty(EquipmentNumber))
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【产品条码校验】接口失败>> 参数错误，根据机台编码[{1}]与操作序号[{2}]获取设备编码失败，请检查配置文件中的机台映射关系是否正确。", _transData.SerialNumber, _transData.DeviceCode, _transData.OpeIndex));
                SendMsg(_session, _transData);
                return;
            }

            //2、获取缓存产品条码 并校验---------------------------------
            string cacheSN = cfg.AppConfigGet(_transData.DeviceCode + "_SN_" + _transData.OpeIndex);
            if (string.IsNullOrEmpty(cacheSN))
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【产品条码校验】接口失败>> 参数错误，获取缓存产品条码失败，请检查配置文件中的缓存产品条码是否写入。", _transData.SerialNumber));
                SendMsg(_session, _transData);
                return;
            }

            //3、判断条码是否匹配 --------------------------------- 
            if (cacheSN != _transData.SerialNumber)
            {
                _transData.Status = CheckResult.NG.ToString();
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【产品条码校验】接口失败>> 请求校验条码：{0}，缓存条码：{1}。", _transData.SerialNumber, cacheSN));
                SendMsg(_session, _transData);
                return;
            }


            _transData.Status = CheckResult.OK.ToString();
            log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【产品条码校验】接口成功>> 请求校验条码：{0}，缓存条码：{1}。", _transData.SerialNumber, cacheSN));
            SendMsg(_session, _transData);

        }
         
        private static void SendMsg(MesSession _session, TransData _transData)
        {
            // 发送【产品条码校验】执行结果至PLC
            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);
            LogInfo log = new SocketService.LogInfo(_session, LogLevel.Info, GlobalData.Pre_Send + msg);
        }

        #endregion



    }
}
