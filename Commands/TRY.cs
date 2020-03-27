
using MES.SocketService.BLL;
using MES.SocketService.Entity;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using YUN.Framework.Commons;
using YUN.Framework.ControlUtil;

namespace MES.SocketService
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TRY : CommandBase<MesSession, MesRequestInfo>
    {
        LogInfo log;
        public override void ExecuteCommand(MesSession session, MesRequestInfo requestInfo)
        {

            log = new LogInfo(session, LogLevel.Info, "开始读配置文件【OP010_STP01】值。");

            META_ParameterInfo info = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='WebPassword'");
            string equ = info.Value;
            log = new LogInfo(session, LogLevel.Info, $"读取完成【OP010】值为：{equ},下一步写配置文件。");

            string temp = StringHelper.GetFirstToChart(equ, ".".ToCharArray());

            if (temp == "E")
            {
                equ = equ.Replace($"{temp}.", "P3.");
            }
            else
            {
                equ = equ.Replace($"{temp}.", "E.");
            }

            info.Value = equ;
            BLLFactory<META_Parameter>.Instance.Update(info, info.ID);

            log = new LogInfo(session, LogLevel.Info, $"写完成【OP010】值为：{equ}。");
            requestInfo.TData.Status = CheckResult.OK.ToString();
            string msg = GlobalData.ToTranString(requestInfo.TData);
            session.Send(msg);
            log = new LogInfo(session, LogLevel.Info, GlobalData.Pre_Send + msg);
        }
    }
}
