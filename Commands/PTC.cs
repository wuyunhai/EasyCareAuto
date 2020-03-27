
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
        private string EmployeeName = "产品条码校验";
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            ExecutePTC(session, requestInfo.TData);
        }

        #region 【产品条码校验】方法

        /// <summary>
        /// 产品条码校验
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        private void ExecutePTC(MesSession _session, TransData _transData)
        {
            //1、参数校验---缓存产品条码---------------------------------
            string cacheSN = string.Empty;
            if (!EmployeeComm.CheckNormalParam(_session, _transData, "OP010_SN_CHECK", out cacheSN, "缓存产品条码", EmployeeName))
                return;

            //2、判断条码是否匹配 --------------------------------- 
            LogInfo log = null;
            if (cacheSN != _transData.SN)
            {
                log = new SocketService.LogInfo(_session, LogLevel.Error, $"[{_transData.SN}]执行【{EmployeeName}】接口失败>> 请求校验条码：{_transData.SN}，缓存条码：{cacheSN}。");
                _transData.ProcessData = $"Bar Code validation failed, request validation BarCode: {_transData.SN}, Cache BarCode: {cacheSN}";
                EmployeeComm.SendMsg(_session, _transData, CheckResult.NG);
                return;
            }

            //3、API执行成功  ---------------------------------      
            log = new SocketService.LogInfo(_session, LogLevel.Info, $"[{_transData.SN}]执行【{EmployeeName}】接口成功>> 请求校验条码：{_transData.SN}，缓存条码：{cacheSN}。");
            EmployeeComm.SendMsg(_session, _transData, CheckResult.OK);

        }

        #endregion



    }
}
