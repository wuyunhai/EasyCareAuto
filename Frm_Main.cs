using CCWin;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MES.SocketService
{
    public partial class Frm_Main : Skin_DevExpress
    {
        private MesServer mesServer;

        public Frm_Main()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 在缓冲区重绘
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

            DelegateState.ServerStateInfo = ServerShowStateInfo;
            DelegateState.NewSessionConnected = NewSessionConnected; 
            DelegateState.SessionClosed = SessionClosed;

        }

        #region  <启动服务模块>

        /// <summary>
        /// 启动服务
        /// </summary>
        private void btnTCP_Click(object sender, EventArgs e)
        {
            btnTCP.Enabled = false;

            if (mesServer == null)
            {
                SetupAppServer();
            }
            if (mesServer.State != ServerState.Running)
            {
                mesServer.Start();
                btnTCP.Text = "MES服务停止";
                PicBoxTCP.BackgroundImage = Properties.Resources._07822;
                txtMsg.AppendText(DateTime.Now + ">> MES服务器启动成功" + Environment.NewLine);  
                lblTCP.Text = "MES服务器地址:" + mesServer.Config.Ip + ":" + mesServer.Config.Port;
            }
            else
            { 
                mesServer.Stop(); 
                btnTCP.Text = "MES服务启动";
                PicBoxTCP.BackgroundImage = Properties.Resources._07821;
                txtMsg.AppendText(DateTime.Now + ">> MES服务器停止" + Environment.NewLine);
                lblTCP.Text = "MES服务器地址:";
            } 
            btnTCP.Enabled = true;
        }

        #region
         
        #region 连接关闭事件

        void SessionClosed(MesSession session, global::SuperSocket.SocketBase.CloseReason value)
        {
            this.Invoke(new ThreadStart(delegate
            {
                ListViewItem item = listAllView.FindItemWithText(session.SessionID);
                if (item != null)
                { 
                    listAllView.Items.Remove(item);

                    TeartbeatShowStateInfo(listAllView.Items.Count, session.RemoteEndPoint + " 已断开连接.");
                    ServerShowStateInfo(" >> " + session.RemoteEndPoint + " 已断开连接.");
                }
            }));
        }

        #endregion

        #region 新连接事件

        void NewSessionConnected(MesSession session)
        {
            this.Invoke(new ThreadStart(delegate
            {
                listAllView.BeginUpdate();
                ListViewItem lvi = new ListViewItem();
                lvi.Text = session.SessionID;
                lvi.SubItems.Add(session.RemoteEndPoint.ToString());
                lvi.SubItems.Add(DateTime.Now.ToString());
                lvi.SubItems.Add(session.Config.Mode.ToString());
                listAllView.Items.Add(lvi);
                listAllView.EndUpdate();

                TeartbeatShowStateInfo(listAllView.Items.Count, session.RemoteEndPoint + " 成功连接至服务器中心.");
                ServerShowStateInfo(" >> " + session.RemoteEndPoint + " 成功连接至服务器中心.");
            }));
        }

        #endregion
         
        /// <summary>
        /// 信息添加
        /// </summary>
        /// <param name="msg"></param>
        void ServerShowStateInfo(string msg)
        {
            this.Invoke(new ThreadStart(delegate
            {
                tpe2txtMsg.AppendText(DateTime.Now + ":" + msg + Environment.NewLine);
            }));
        }

        /// <summary>
        /// 连接计数
        /// </summary>
        void TeartbeatShowStateInfo(int num, string msg)
        {
            this.Invoke(new ThreadStart(delegate
            {
                lbl6.Text = msg;
                lblNum1.NormlBack = ImageListAllUpdate(num / 10 % 10);
                lblNum2.NormlBack = ImageListAllUpdate(num % 10);
            }));
        }

        #endregion

        /// <summary>
        /// 图片更换
        /// </summary>
        Image ImageListAllUpdate(int Num)
        {
            switch (Num)
            {
                case 0:
                    return Properties.Resources._00034_17x25x8BPP_;
                case 1:
                    return Properties.Resources._00035_17x25x8BPP_;
                case 2:
                    return Properties.Resources._00036_17x25x8BPP_;
                case 3:
                    return Properties.Resources._00037_17x25x8BPP_;
                case 4:
                    return Properties.Resources._00038_17x25x8BPP_;
                case 5:
                    return Properties.Resources._00039_17x25x8BPP_;
                case 6:
                    return Properties.Resources._00040_17x25x8BPP_;
                case 7:
                    return Properties.Resources._00041_17x25x8BPP_;
                case 8:
                    return Properties.Resources._00042_17x25x8BPP_;
                case 9:
                    return Properties.Resources._00043_17x25x8BPP_;
                default:
                    return null;
            }
        }

        #endregion

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            SetupAppServer();
        }

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
    }
}
