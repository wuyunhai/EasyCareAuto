using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YUN.Framework.BaseUI;

namespace MES.SocketService
{
    public partial class ServerForm : BaseForm_Normal
    {
        private MesServer mesServer;

        public ServerForm()
        {
            InitializeComponent();
        }

        #region 服务的启停

        private void btnListen_Click(object sender, EventArgs e)
        {
            btnListen.Enabled = false;
            btnStopListen.Enabled = true;

            if (mesServer == null)
            {
                SetupAppServer();
            }
            if (mesServer.State != ServerState.Running)
            {
                mesServer.Start();
                txtMsg.AppendText(DateTime.Now + ">> MES服务器启动成功" + Environment.NewLine);
                txtServerIP.Text = mesServer.Config.Ip;
                txtServerPort.Text = mesServer.Config.Port.ToString();
            }
        }
        private void btnStopListen_Click(object sender, EventArgs e)
        {
            btnListen.Enabled = true;
            btnStopListen.Enabled = false;

            if (mesServer.State == ServerState.Running)
            { 
                mesServer.Stop();
                txtMsg.AppendText(DateTime.Now + ">> MES服务器停止" + Environment.NewLine);
            }
        }
        /// <summary>
        /// 初始化服务器
        /// </summary>
        private void SetupAppServer()
        {
            if (mesServer == null)
            {
                //方法一、采用当前应用程序中的【App.config】文件。
                var bootstrap = BootstrapFactory.CreateBootstrap();
                if (!bootstrap.Initialize())
                {
                    txtMsg.AppendText(DateTime.Now + ">> 初始化服务器失败" + Environment.NewLine);
                    return;
                }
                StartResult startResult = bootstrap.Start();
                if (startResult == StartResult.Success)
                {
                    txtMsg.AppendText(DateTime.Now + ">> 初始化服务器" + Environment.NewLine);
                    mesServer = bootstrap.AppServers.Cast<MesServer>().FirstOrDefault();
                    mesServer.Stop();
                }
                else
                {
                    txtMsg.AppendText(DateTime.Now + ">> 初始化服务器失败" + Environment.NewLine);
                }
            }
        }
        #endregion


    }
}
