using CCWin.SkinControl;
using MES.SocketService.BLL;
using MES.SocketService.Entity;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using YUN.Framework.BaseUI;
using YUN.Framework.Commons;
using YUN.Framework.Commons.Collections;
using YUN.Framework.Commons.Threading;
using YUN.Framework.Commons.Winform;
using YUN.Framework.ControlUtil;

namespace MES.SocketService
{
    public partial class Frm_Main : BaseForm
    {
        private int MainTabIndex = 0;
        private bool LoadFinish = false;  //开启服务,加载完成后开始显示日志
        private bool IsViewHeartLog;      // 当页面切到【实时监控】下的【心跳日志】是才显示线条日志，其他情况心跳日志不输出至UI； 
        private IBootstrap m_Bootstrap;   // 通过BootStrap启动Socket服务
        private MesServer mesServer;
        private ConcurrentQueue<LogInfo> concurrentQueue = new ConcurrentQueue<LogInfo>();
        private QueueServer<LogInfo> queueServer = new QueueServer<LogInfo>();
        private QueueServer<MesSession> sessionClosedQueueServer = new QueueServer<MesSession>();
        private List<ClientInfo> lstClientConnHistory = new List<ClientInfo>();


        public Frm_Main()
        {
            InitializeComponent();
        }

        public override void FormOnLoad()
        {
            base.FormOnLoad();

            Splasher.Status = ">> 正在加载程序样式设置...";
            Thread.Sleep(80);
            FrmSet();
            Application.DoEvents();

            Splasher.Status = ">> 正在加载测试API内容...";
            Thread.Sleep(80);
            LoadMethod();
            Application.DoEvents();

            Splasher.Status = ">> 正在加载测试通讯指令内容...";
            Thread.Sleep(80);
            LoadTxtFile("");
            Application.DoEvents();

            Splasher.Status = ">> 正在加载数据服务器配置文件...";
            Thread.Sleep(80);
            //SetupAppServer();
            Application.DoEvents();

            Splasher.Status = ">> 开始检验网络是否畅通...";
            SetupCheck();
            Splasher.Close();

        }

        private void FrmSet()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 在缓冲区重绘
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

            DelegateState.ServerStateInfo = ServerShowStateInfo;
            DelegateState.NewSessionConnected = NewSessionConnected;
            DelegateState.SessionClosed = SessionClosed;

            #region wgvList

            Dictionary<string, string> columnNameAlias = new Dictionary<string, string>();
            columnNameAlias.Add("SessionID", "会话ID");
            columnNameAlias.Add("RemoteDeviceName", "远端地址");
            columnNameAlias.Add("Time", "操作时间");
            columnNameAlias.Add("Mode", "动作");
            columnNameAlias.Add("Reason", "原因");

            wgvList.ShowLineNumber = false;
            wgvList.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置   
            wgvList.dataGridView1.DataSourceChanged += new EventHandler(wgvList_DataSourceChanged);
            wgvList.dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(wgvList_RowPostPaint);
            wgvList.DisplayColumns = "SessionID,RemoteDeviceName,Time,Mode,Reason";
            wgvList.ColumnNameAlias = columnNameAlias;

            #endregion

            queueServer.IsBackground = true;
            queueServer.ProcessItem += QueueServer_ProcessItem;
            sessionClosedQueueServer.IsBackground = true;
            sessionClosedQueueServer.ProcessItem += SessionClosedQueueServer_ProcessItem;

            MainTabIndex = 0;
            TabControl1.SelectedIndex = 0;

            tPanelSet.DataBindings.Add(new Binding("Enabled", btnTCPStart, "Enabled", true, DataSourceUpdateMode.OnPropertyChanged));
            lblPSetWarm.DataBindings.Add(new Binding("Visible", btnTCPStop, "Enabled", true, DataSourceUpdateMode.OnPropertyChanged));

            META_ParameterInfo info = BLLFactory<META_Parameter>.Instance.FindSingle("Key='ServiceDescription'");
            if (info != null)
                Text = info.Value;


        }

        /// <summary>
        /// 绑定数据后，分配各列的宽度
        /// </summary>
        private void wgvList_DataSourceChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.Columns.Count > 0 && dgv.RowCount > 0)
            {
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    column.Width = 350;
                }
            }
        }
        /// <summary>
        /// 设置字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wgvList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (wgvList.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() == "断开")
            {
                wgvList.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Crimson;    // 选中行
            }
        }

        #region 启动检查

        /// <summary>
        /// 启动检查，检查1、数据交换服务器IP设置，2、WMS服务器IP设置，3、PDA设置，4、3台PLC设置是否都正确。
        /// </summary>
        private void SetupCheck()
        {
            bool checkResult = true; string strStaus = string.Empty;
            CDictionary<string, string> checkIPList = new CDictionary<string, string>();

            checkIPList = AppConfig.Instance().GetAppSettingsByPreKeyIsIP();
            if (checkIPList != null && checkIPList.Count > 0)
            {
                foreach (var item in checkIPList)
                {
                    Splasher.Status = strStaus = $">> 检查{item.Key}网络是否正常.";
                    Application.DoEvents();
                    txtMsg.AppendText(strStaus + Environment.NewLine);
                    if (NetworkUtil.TestNetConnectity(item.Value))
                    {
                        Splasher.Status = strStaus = $">> {item.Key}({item.Value})网络正常.";
                        Application.DoEvents();
                        txtMsg.AppendText(strStaus + Environment.NewLine);
                    }
                    else
                    {
                        checkResult = false;
                        Splasher.Status = strStaus = $">> {item.Key}({item.Value})网络异常,请检查.";
                        Application.DoEvents();
                        txtMsg.AppendRichText(strStaus + Environment.NewLine, new Font("微软雅黑", 9), Color.Crimson);
                        return;
                    }
                }
            }

            if (checkResult)
            {
                Thread.Sleep(50);
                btnTCPStart_Click(null, null);
                Splasher.Status = ">> 启动数据交换服务器MES.SocketService";
                Application.DoEvents();
            }

        }

        /// <summary>
        /// 初始化服务器
        /// </summary>
        private bool SetupAppServer()
        {

            m_Bootstrap = BootstrapFactory.CreateBootstrap();
            if (!m_Bootstrap.Initialize())
            {
                txtMsg.AppendRichText($"{DateTime.Now}>> 初始化服务器配置文件失败，请检查.{Environment.NewLine}", new Font("微软雅黑", 9), Color.Crimson);
                return false;
            }
            txtMsg.AppendRichText($"{DateTime.Now}>> 初始化服务器配置成功.{Environment.NewLine}", new Font("微软雅黑", 9), Color.FromArgb(2, 79, 142));
            return true;
        }

        #endregion

        #region  <启停服务模块>

        /// <summary>
        /// 启动服务
        /// </summary>
        private void btnTCPStart_Click(object sender, EventArgs e)
        {
            SetupAppServer();

            BtnTcpControl("alloff");
            ProcessOperator pOpStartServer = new ProcessOperator();
            pOpStartServer.BackgroundWork = pOPStartServerBackWork;
            pOpStartServer.MessageInfo = "正在启动服务,请稍后...";
            pOpStartServer.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(pOpStartServer_BackgroundWorkerCompleted);
            pOpStartServer.Start();
        }
        private void pOPStartServerBackWork()
        {
            LoadFinish = false;
            if (m_Bootstrap == null)
            {
                MessageUtilSkin.ShowTips("数据服务器配置加载错误，请检查服务器IP，端口是否设置正确。");
                return;
            }

            StartResult startResult = m_Bootstrap.Start();
            if (startResult == StartResult.Success)
            {
                mesServer = m_Bootstrap.AppServers.Cast<MesServer>().FirstOrDefault();
            }
            else
            {
                mesServer = null;
            }
        }
        private void pOpStartServer_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {

            if (mesServer != null)
            {
                ServerLogNotice($"服务器启动成功");
                ServerScrNotice($"服务器启动成功");
                ServerLblNotice($"{mesServer.Listeners[0].EndPoint.Address}");
                LogInfo log = new LogInfo(mesServer, LogLevel.Info, "服务器启动成功.");

                BtnTcpControl("on");
                PicTcpControl("on");

                lblDataIP.Text = mesServer.Listeners[0].EndPoint.ToString();
                lblHeartIP.Text = mesServer.Listeners[1].EndPoint.ToString();

                if (GlobalData.IsDebug)
                    lblIsDebug.Visible = true;
                else
                    lblIsDebug.Visible = false;

                LoadFinish = true;
            }
            else
            {
                BtnTcpControl("off");
                txtMsg.AppendRichText($"{DateTime.Now}>> 服务器启动失败,请检查服务器设置是否正确.{Environment.NewLine}", new Font("微软雅黑", 9), Color.Crimson);
                return;
            }
        }

        #region other

        /// <summary>
        /// 服务器启停日志
        /// </summary>
        /// <param name="msg"></param>
        private void ServerLogNotice(string msg)
        {
            txtMsg.AppendText($"{DateTime.Now}>> {msg}{Environment.NewLine}");
        }

        /// <summary>
        /// 服务器公告
        /// </summary>
        /// <param name="content"></param>
        private void ServerLblNotice(string content)
        {
            //lblTCP.Text = $"MES服务器地址:{content}";
        }

        /// <summary>
        /// 服务器滚动通知
        /// </summary>
        /// <param name="msg"></param>
        private void ServerScrNotice(string content)
        {
            txtScroll.ScrollText = $"{DateTime.Now}>> {content}";
        }

        /// <summary>
        /// 服务开关控制
        /// </summary>
        /// <param name="onoff"></param>
        private void BtnTcpControl(string onoff)
        {
            switch (onoff)
            {
                case "on":
                    btnTCPStop.Enabled = true;
                    btnTCPStart.Enabled = false;
                    break;
                case "off":
                    btnTCPStop.Enabled = false;
                    btnTCPStart.Enabled = true;
                    break;
                case "alloff":
                    btnTCPStop.Enabled = false;
                    btnTCPStart.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// 服务图标控制
        /// </summary>
        /// <param name="onoff"></param>
        private void PicTcpControl(string onoff)
        {
            if (onoff == "on")
            {
                PicBoxTCP.Image = null;
                PicBoxTCP.Image = Properties.Resources.run;

                gifData.Image = null;
                gifData.Image = Properties.Resources.connecting;

                gifHeart.Image = null;
                gifHeart.Image = Properties.Resources.heart128;

                lblDataClientCount.ForeColor = Color.FromArgb(2, 79, 142);
                lblHeartClientCount.ForeColor = Color.FromArgb(2, 79, 142);

            }
            else
            {
                PicBoxTCP.Image = null;
                PicBoxTCP.Image = Properties.Resources.readyRun;

                gifData.Image = null;
                gifData.Image = Properties.Resources.disConnecting;

                gifHeart.Image = null;
                gifHeart.Image = Properties.Resources.heart128_b;

                lblDataClientCount.ForeColor = Color.DimGray;
                lblHeartClientCount.ForeColor = Color.DimGray;

                lblDataIP.Text = "--/--";
                lblHeartIP.Text = "--/--";
            }
        }
        #endregion

        /// <summary>
        /// 关闭服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTCPStop_Click(object sender, EventArgs e)
        {
            BtnTcpControl("alloff");
            ProcessOperator pOpStopServer = new ProcessOperator();
            pOpStopServer.BackgroundWork = pOPBackWork;
            pOpStopServer.MessageInfo = "正在关闭服务,请稍后...";
            pOpStopServer.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(pOpStopServer_BackgroundWorkerCompleted);
            pOpStopServer.Start();
        }

        private void pOPBackWork()
        {
            if (m_Bootstrap != null)
            {
                m_Bootstrap.Stop();
                LogInfo log = new LogInfo(mesServer, LogLevel.Info, "服务器已关闭.");
            }
        }

        private void pOpStopServer_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            BtnTcpControl("off");
            PicTcpControl("off");

            ServerLogNotice($"服务器已关闭");
            ServerScrNotice($"服务器已关闭");
            ServerLblNotice($"");

            lblIsDebug.Visible = false; //调试标志
        }

        #endregion


        #region session连接关闭事件

        #region 连接关闭事件

        int dataCount;
        int heartCount;
        private void SessionClosedQueueServer_ProcessItem(MesSession session)
        {
            this.Invoke(new ThreadStart(delegate
            {
                lstClientConnHistory.Insert(0, new ClientInfo(session.SessionID, session.RemoteDeviceName, session.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), "断开", ""));

                dataCount = GlobalData.ClientSessionList.FindAll(li => li.LocalEndPoint.Port == 5000).Count();
                heartCount = GlobalData.ClientSessionList.FindAll(li => li.LocalEndPoint.Port == 5001).Count();
                UpdateClientCount(dataCount, heartCount);
            }));
        }
        void SessionClosed(MesSession session, global::SuperSocket.SocketBase.CloseReason value)
        {
            sessionClosedQueueServer.EnqueueItem(session);
        }

        #endregion

        #region 新连接事件

        void NewSessionConnected(MesSession session)
        {
            this.Invoke(new ThreadStart(delegate
            {
                SessionConnectUpdate(session);
            }));
        }

        private void SessionConnectUpdate(MesSession session)
        {
            lstClientConnHistory.Insert(0, new ClientInfo(session.SessionID, session.RemoteDeviceName, session.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), "连接", ""));

            //TeartbeatShowStateInfo(GlobalData.ClientSessionList.Count, session.RemoteEndPoint + " 成功连接至服务器中心.");


            dataCount = GlobalData.ClientSessionList.FindAll(li => li.LocalEndPoint.Port == 5000).Count();
            heartCount = GlobalData.ClientSessionList.FindAll(li => li.LocalEndPoint.Port == 5001).Count();
            UpdateClientCount(dataCount, heartCount);
        }

        #region 树节点排序
        public int TreeNodeCompare(TreeNode x, TreeNode y)
        {
            TreeNode tx = x as TreeNode;
            TreeNode ty = y as TreeNode;
            return String.Compare(tx.Text, ty.Text);
        }

        public void SortTreeNode(TreeNode cur_Node)
        {
            Comparison<TreeNode> sorterX = new Comparison<TreeNode>(TreeNodeCompare);
            System.Collections.Generic.List<TreeNode> al = new System.Collections.Generic.List<TreeNode>();

            foreach (TreeNode tn in cur_Node.Nodes)
            {
                al.Add(tn);
            }
            al.Sort(sorterX);

            cur_Node.Nodes.Clear();
            foreach (TreeNode tn in al)
            {
                cur_Node.Nodes.Add(tn);
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// 信息添加
        /// </summary>
        /// <param name="msg"></param>
        void ServerShowStateInfo(LogInfo log)
        {
            if ((mesServer != null && mesServer.State != ServerState.Running) || LoadFinish == false || MainTabIndex != 1)
            {
                queueServer.ClearItems();
                return;
            }
            queueServer.EnqueueItem(log);
        }

        private void QueueServer_ProcessItem(LogInfo log)
        {
            bool isHeartLog = log.LocalDeviceName.Contains("心跳服务器");
            if (isHeartLog)
            {
                if (IsViewHeartLog)
                    WriteRichTextLog(richHeartLog, log);
            }
            else
            {
                WriteRichTextLog(richLog, log);
            }
        }

        private void WriteRichTextLog(SkinChatRichTextBox richTextControl, LogInfo log)
        {
            this.Invoke(new ThreadStart(delegate
            {
                if (richTextControl.Lines.Count() > 500)
                {
                    richTextControl.ResetText();
                }
                switch (log.Level)
                {
                    case "Error":
                        richTextControl.AppendRichText(log.ToString(), new Font("微软雅黑", 9f), Color.Crimson);

                        break;
                    default:
                        richTextControl.AppendRichText(log.ToString(), new Font("微软雅黑", 9f), Color.FromArgb(2, 79, 142));
                        break;
                }
                richTextControl.AppendText(Environment.NewLine);
                //滚动到光标位置
                richTextControl.ScrollToCaret();
            }));
        }

        /// <summary>
        /// 连接计数
        /// </summary>
        private void UpdateClientCount(int dataCount, int heartCount)
        {
            CallCtrlWithThreadSafety.SetText(lblDataClientCount, dataCount.ToString(), this);
            CallCtrlWithThreadSafety.SetText(lblHeartClientCount, heartCount.ToString(), this);
        }

        #endregion

        private void tsmpMenu_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem tsmp = sender as ToolStripMenuItem;
                if (mesServer == null)
                    return;
                IPEndPoint EndPoint = mesServer.Listeners[0].EndPoint;

                Frm_Msg frm = new Frm_Msg();
                frm.Text = $"{ mesServer.Listeners[0].EndPoint} <<-- {tsmp.Text}";
                frm.txtMsg.Text = tsmp.Tag.ToString();
                frm.EndPoint = EndPoint;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

            }
            catch (Exception ex)
            {
                MessageUtilSkin.ShowTips(ex.Message);
            }
        }

        #region 切换业务数据/心跳数据页

        private void ChangePage(TreeNode node)
        {
            string rootNodeText = string.Empty;
            switch (node.Level)
            {
                case 0:
                    rootNodeText = node.Text;
                    break;
                case 1:
                    rootNodeText = node.Parent.Text;
                    break;
            }

            if (rootNodeText.Contains("心跳服务器"))
            {
                tabControlLogType.SelectedIndex = 1;
            }
            else
            {
                tabControlLogType.SelectedIndex = 0;
            }
        }

        #endregion

        //清空记录
        private void tsmClear_Click(object sender, EventArgs e)
        {
            switch (tabControlLogType.SelectedIndex)
            {
                case 0:
                    richLog.ResetText();
                    break;
                case 1:
                    richHeartLog.ResetText();
                    break;
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            switch (tabControlLogType.SelectedIndex)
            {
                case 0:
                    filePath = $@"{DirectoryUtil.GetCurrentDirectory()}\Logs\Data_{ DateTime.Now.ToString("yyyy-MM-dd")}.log";
                    break;

                case 1:
                    filePath = $@"{DirectoryUtil.GetCurrentDirectory()}\Logs\Heart_{ DateTime.Now.ToString("yyyy-MM-dd")}.log";
                    break;
            }


            if (FileUtil.FileIsExist(filePath))
            {
                Process.Start("notepad++.exe", filePath);
            }
            else
            {
                MessageUtilSkin.ShowTips($"不存在文件：{filePath}");
            }
        }

        #region API测试

        private void LoadMethod()
        {
            apiTestView1.DllFullName = "DM_API.dll";
            apiTestView1.Namespace = "DM_API";
            apiTestView1.Classname = "DM_SFCInterface";
            apiTestView1.LoadAPIMethod();
        }

        #endregion

        #region 加载测试指令

        #region 读取本地文件

        private void LoadTxtFile(string txtPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPath))
                    txtPath = $@"{DirectoryUtil.GetCurrentDirectory()}\Docs\protocol.txt";

                if (!FileUtil.FileIsExist(txtPath))
                    return;

                string sFullText = FileUtil.FileToString(txtPath);
                string[] arrComms = sFullText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string str in arrComms)
                {
                    ToolStripMenuItem tsmItem = new ToolStripMenuItem();

                    ArrayList al = StringUtil.ExtractInnerContent(str, "[", "]");
                    tsmItem.Text = al[0].ToString();
                    tsmItem.Tag = al[1].ToString();
                    tsmItem.Click += tsmpMenu_Click;
                    contextMenuStrip1.Items.Add(tsmItem);
                }
            }
            catch (Exception e)
            {
                return;
            }

        }


        #endregion
        #endregion

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //如果我们操作【×】按钮，那么不关闭程序而是缩小化到托盘，并提示用户.
            if (this.WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;//不关闭程序

                this.Hide();
                //最小化到托盘的时候显示图标提示信息，提示用户并未关闭程序
                this.WindowState = FormWindowState.Minimized;
                notifyIcon1.ShowBalloonTip(3000, "程序最小化提示",
                     "图标已经缩小到托盘，打开窗口请双击图标即可。也可以使用Alt+S键来显示/隐藏窗体。",
                     ToolTipIcon.Info);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyMenu_Show_Click(sender, e);
        }

        private void notifyMenu_Show_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
                this.Show();
                this.BringToFront();
                this.Activate();
                this.Focus();
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }

        private void tsmItemExit_Click(object sender, EventArgs e)
        {
            notifyMenu_Exit_Click(null, null);
        }

        private void notifyMenu_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                if (mesServer != null && mesServer.State == ServerState.Running)
                {
                    if (DialogResult.Yes == MessageUtilSkin.ShowYesNoAndTips("服务正在运行，请先关闭服务."))
                    {
                        TabControl1.SelectedIndex = 0;

                        if (this.WindowState == FormWindowState.Minimized)
                        {
                            this.WindowState = FormWindowState.Maximized;
                            this.Show();
                            this.BringToFront();
                            this.Activate();
                            this.Focus();
                        }
                    }
                }
                else
                {
                    if (DialogResult.Yes == MessageUtilSkin.ShowYesNoAndTips("是否确定退出程序？"))
                    {
                        this.ShowInTaskbar = false;
                        Application.Exit();
                    }
                }
            }
            catch
            {

                // Nothing to do.
            }
        }

        private void Frm_Main_MaximizedBoundsChanged(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Frm_Main_Move(object sender, EventArgs e)
        {

        }

        #region 客户端工具



        private void lnkClient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            META_DevLinkInfo info = BLLFactory<META_DevLink>.Instance.FindSingle($"DevCode='OP010'");

            //测试
            META_ParameterInfo info2 = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='WorkOrder'");
            info2.Value = "TEST002";
            BLLFactory<META_Parameter>.Instance.Update(info2, info2.ID);


            info2 = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='OK_RE'");

            try
            {
                if (mesServer == null)
                {
                    MessageUtilSkin.ShowTips("请启动数据服务器.");
                    return;
                }
                TabControl1.SelectedIndex = 1;
                IPEndPoint EndPoint = mesServer.Listeners[0].EndPoint;

                Frm_Msg frm = new Frm_Msg();
                frm.Text = $"{ EndPoint.Address}";
                frm.txtMsg.Text = string.Empty;
                frm.EndPoint = EndPoint;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
            }
            catch (Exception ex)
            {
                MessageUtilSkin.ShowTips(ex.Message);
            }
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            lnkClient_LinkClicked(null, null);
        }

        #endregion

        #region TabSelected
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainTabIndex = TabControl1.SelectedIndex;

            // 只要离开日志监控页，即清空日志队列
            if (TabControl1.SelectedIndex != 1)
                queueServer.ClearItems();

            if (TabControl1.SelectedIndex == 1 && tabControlLogType.SelectedIndex == 1)
                IsViewHeartLog = true;
            else
                IsViewHeartLog = false;
        }

        private void tabControlLogType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl1.SelectedIndex == 1 && tabControlLogType.SelectedIndex == 1)
                IsViewHeartLog = true;
            else
                IsViewHeartLog = false;
        }
        #endregion

        #region 更多

        private void Frm_Main_SysBottomClick(object sender, SysButtonEventArgs e)
        {
            contextMenuStrip4.Show(e.SysButton.Location.X, e.SysButton.Location.Y + 30);
        }
        private void tsmItemRestart_Click(object sender, EventArgs e)
        {
            if (mesServer != null && mesServer.State == ServerState.Running)
            {
                if (DialogResult.Yes == MessageUtilSkin.ShowYesNoAndTips("服务正在运行，请先关闭服务."))
                {
                    TabControl1.SelectedIndex = 0;
                    return; 
                } 
            }
            else
            {
                if (DialogResult.Yes == MessageUtilSkin.ShowYesNoAndTips("是否注销系统，重新登陆。"))
                {
                    this.ShowInTaskbar = false;
                    Application.Exit();
                    Application.Restart();
                    Environment.Exit(0);
                }
            }

        }

        private void tsmItemDoc_Click(object sender, EventArgs e)
        {
            string filePath = $@"{DirectoryUtil.GetCurrentDirectory()}\Docs\MES&PLC通讯系统使用说明书.pdf";

            if (FileUtil.FileIsExist(filePath))
            {
                try
                {
                    Process.Start("FoxitReader.exe", filePath);
                }
                catch (Exception)
                {
                    Process.Start(filePath);
                }
            }
            else
            {
                MessageUtilSkin.ShowTips($"不存在文件：{filePath}");
            }
        }

        #endregion

        /// <summary>
        /// 刷新设备连接历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            wgvList.DataSource = null;
            wgvList.DataSource = lstClientConnHistory;
        }

        /// <summary>
        /// 客户端连接明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDataClientCount_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl.Text == "0")
                return;

            Frm_ConnectClient frm;
            switch (lbl.Tag.ToString())
            {
                case "5001":
                    frm = new Frm_ConnectClient("5001");
                    frm.ShowDialog();
                    break;
                case "5000":
                    frm = new Frm_ConnectClient("5000");
                    frm.ShowDialog();
                    break;
                default:
                    return;
            }
        }


    }

    public class ClientInfo
    {
        public ClientInfo(string sessionID, string remoteDeviceName, string time, string mode, string reason)
        {
            SessionID = sessionID;
            RemoteDeviceName = remoteDeviceName;
            Time = time;
            Mode = mode;
            Reason = reason;
        }

        public string SessionID { get; set; }
        public string RemoteDeviceName { get; set; }
        public string Time { get; set; }
        public string Mode { get; set; }
        public string Reason { get; set; }
    }
}

