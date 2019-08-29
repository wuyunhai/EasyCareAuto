
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
    /// 【通用数据采集】
    /// </summary>
    public class NDQ : CommandBase<MesSession, MesRequestInfo>
    {
        private DM_API.DM_SFCInterface Dm_Interface = new DM_API.DM_SFCInterface();

        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {
            CheckRoute(session, requestInfo.TData);
        }

        #region 【通用数据采集】方法

        /// <summary>
        /// 通用数据采集
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
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【通用数据采集】接口失败>> 参数错误，根据机台编码[{1}]与操作序号[{2}]获取设备编码失败，请检查配置文件中的机台映射关系是否正确。", _transData.SerialNumber, _transData.DeviceCode, _transData.OpeIndex));
                SendMsg(_session, _transData, CheckResult.NG);
                return;
            }

            //2、获取过程数据并校验---------------------------------
            List<string> lstProcessData = StringHelper.GetStrArray(_transData.ProcessData, ',', false);
            if (lstProcessData.Count < 2)
            { 
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【通用数据采集】接口失败>> 协议错误，请检查协议中是否至少包含采集类别码，采集数据,当前过程数据：{1}。", _transData.SerialNumber, _transData.ProcessData));
                SendMsg(_session, _transData, CheckResult.NG);
                return;
            }

            //2.1 按采集类别校验
            #region check

            bool check = true;
            switch (lstProcessData[0])
            {
                case "01": //螺丝数据采集
                    if (lstProcessData.Count != 6)
                    {
                        check = false;
                        log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【通用数据采集】接口失败>> 协议错误，请检查协议中是否至少包含采集类别码，采集数据（6项）,当前过程数据：{1}。", _transData.SerialNumber, _transData.ProcessData));
                    }
                    break;

                case "02": //气密性测试数据采集 
                    if (lstProcessData.Count != 3)
                    {
                        check = false;
                        log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【通用数据采集】接口失败>> 协议错误，请检查协议中是否至少包含采集类别码，采集数据（3项）,当前过程数据：{1}。", _transData.SerialNumber, _transData.ProcessData));
                    }
                    break;

                case "03": //预热温度数据采集 
                    if (lstProcessData.Count != 3)
                    {
                        check = false;
                        log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【通用数据采集】接口失败>> 协议错误，请检查协议中是否至少包含采集类别码，采集数据（3项）,当前过程数据：{1}。", _transData.SerialNumber, _transData.ProcessData));
                    }
                    break;

                case "04": //固化温度数据采集 
                    if (lstProcessData.Count != 3)
                    {
                        check = false;
                        log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【通用数据采集】接口失败>> 协议错误，请检查协议中是否至少包含采集类别码，采集数据（3项）,当前过程数据：{1}。", _transData.SerialNumber, _transData.ProcessData));
                    }
                    break;

                case "05": //点胶胶量数据采集 
                    if (lstProcessData.Count != 2)
                    {
                        check = false;
                        log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【通用数据采集】接口失败>> 协议错误，请检查协议中是否至少包含采集类别码，采集数据（2项）,当前过程数据：{1}。", _transData.SerialNumber, _transData.ProcessData));
                    }
                    break;

                default: //未知类别
                    check = false;
                    log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【通用数据采集】接口失败>> 协议错误，未知的采集类别码,当前过程数据：{1}。", _transData.SerialNumber, _transData.ProcessData));
                    break;
            }
            if (!check)
            { 
                SendMsg(_session, _transData, CheckResult.NG);
                return;
            }

            #endregion

            //3、API执行是否超时 ---------------------------------
            DataTable dt = GlobalData.RunTaskWithTimeout<DataTable>((Func<DataTable>)delegate { return InsertNDQ(_transData.SerialNumber, EquipmentNumber, lstProcessData); }, GlobalData.ApiTimeout);
            if (dt == null)
            { 
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【通用数据采集】接口超时（>{1}ms），执行参数：[{0}]，[{2}]，[{3}]。", _transData.SerialNumber, GlobalData.ApiTimeout, EquipmentNumber, _transData.ProcessData));
                SendMsg(_session, _transData, CheckResult.NG);
                return;
            }

            //4、判断API是否执行成功 ---------------------------------
            string checkStatus = DataTableHelper.GetCellValueinDT(dt, 0, "CheckStatus");
            if (checkStatus != "1")
            { 
                log = new SocketService.LogInfo(_session, LogLevel.Error, string.Format("[{0}]执行【通用数据采集】接口失败>> 接口返回(状态码[{1}])：产品(SN[{0}])计划执行 {2} 数据采集，执行参数：[{0}]，[{3}]，[{4}]。", _transData.SerialNumber, checkStatus, lstProcessData[1], EquipmentNumber, _transData.ProcessData));
                SendMsg(_session, _transData, CheckResult.NG);
                return;
            }

            //6、执行成功  ---------------------------------   
            log = new SocketService.LogInfo(_session, LogLevel.Info, string.Format("[{0}]执行【通用数据采集】接口成功>> 接口返回(状态码[{1}])：产品(SN[{0}])执行{2}数据采集。", _transData.SerialNumber, checkStatus, lstProcessData[0]));
            SendMsg(_session, _transData, CheckResult.OK);

        }

        //采集数据存入db
        private DataTable InsertNDQ(string serialNumber, string equipmentNumber, List<string> lstProcessData)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CheckStatus");
            dt.Rows.Add(1);
            switch (lstProcessData[0])
            {
                case "01": //螺丝数据采集
                    //dt = 
                    break;

                case "02": //气密性测试数据采集 

                    break;

                case "03": //预热温度数据采集 

                    break;

                case "04": //固化温度数据采集 

                    break;

                case "05": //点胶胶量数据采集 

                    break;
            }
            // Thread.Sleep(1500);
            return dt;
        }

        /// <summary>
        /// 反馈消息至PLC
        /// </summary>
        /// <param name="_session"></param>
        /// <param name="_transData"></param>
        /// <param name="checkR"></param>
        private static void SendMsg(MesSession _session, TransData _transData, CheckResult checkR)
        {
            _transData.Status = checkR.ToString();
            string msg = GlobalData.ToTranString(_transData);
            _session.Send(msg);
            LogInfo log = new SocketService.LogInfo(_session, LogLevel.Info, GlobalData.Pre_Send + msg);
        }

        #endregion



    }
}
