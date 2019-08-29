using CCWin;
using SuperSocket.ClientEngine;
using SuperSocket.ProtoBase;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YUN.Framework.BaseUI;
using YUN.Framework.Commons;
using YUN.Framework.Commons.Collections;
using YUN.Framework.Commons.Threading;
using YUN.Framework.Commons.Winform;

namespace MES.SocketService
{
    public partial class Frm_Main : BaseForm
    {
        private MesServer mesServer;
        private static QueueServer<LogInfo> queueServer = new QueueServer<LogInfo>();
        private List<ListViewItem> lstvLogCache = new List<ListViewItem>();

        public Frm_Main()
        {
            InitializeComponent();

            Splasher.Status = ">> 正在加载程序样式设置...";
            Thread.Sleep(100);
            FrmSet();
            Application.DoEvents();

            Splasher.Status = ">> 正在刷新程序参数...";
            Thread.Sleep(100);
            RefreshConfig();
            Application.DoEvents();

            Splasher.Status = ">> 正在加载测试API内容...";
            Thread.Sleep(100);
            LoadMethod();
            Application.DoEvents();

            Splasher.Status = ">> 开始检验网络是否畅通...";
            Thread.Sleep(100);
            SetupCheck();

            Splasher.Close();
            //pOp.BackgroundWork = Run;
            //pOp.MessageInfo = "正在进行启动前检查操作……";
            //pOp.BackgroundWorkerCompleted += POp_BackgroundWorkerCompleted;
            //pOp.Start();
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

            queueServer.IsBackground = true;
            queueServer.ProcessItem += QueueServer_ProcessItem;
        }

        #region 启动检查

        /// <summary>
        /// 启动检查，检查1、数据交换服务器IP设置，2、WMS服务器IP设置，3、PDA设置，4、3台PLC设置是否都正确。
        /// </summary>
        private void SetupCheck()
        {
            bool checkResult = true; string strStaus = string.Empty;
            CDictionary<string, string> checkIPList = new CDictionary<string, string>();
            AppConfig appconfig = new AppConfig();
            checkIPList = appconfig.GetAppSettingsByPreKeyIsIP();
            if (checkIPList != null && checkIPList.Count > 0)
            {
                foreach (var item in checkIPList)
                {
                    Splasher.Status = strStaus = string.Format(">> 检查{0}网络是否正常.", item.Key);
                    Application.DoEvents();
                    txtMsg.AppendText(strStaus + Environment.NewLine);
                    if (NetworkUtil.TestNetConnectity(item.Value))
                    {
                        Splasher.Status = strStaus = string.Format(">> {1}({2})网络正常.", DateTime.Now, item.Key, item.Value);
                        Application.DoEvents();
                        txtMsg.AppendText(strStaus + Environment.NewLine);
                    }
                    else
                    {
                        checkResult = false;
                        Splasher.Status = strStaus = string.Format(">> {1}({2})网络异常,请检查.", DateTime.Now, item.Key, item.Value);
                        Application.DoEvents();
                        txtMsg.AppendRichText(strStaus + Environment.NewLine, new Font("微软雅黑", 9), Color.Crimson);
                    }
                    //Thread.Sleep(50);
                }
            }

            if (checkResult)
            {
                Thread.Sleep(50);
                btnTCP_Click(null, null);
                Splasher.Status = string.Format(">> 启动数据交换服务器MES.SocketService");
                Application.DoEvents();
            }

        }

        #endregion

        ProcessOperator pOp = new ProcessOperator();
        // 加载
        private void Frm_Main_Load(object sender, EventArgs e)
        {


        }

        private void POp_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            if (e.BackGroundException == null)
            {

            }
            else
            {
                MessageUtilSkin.ShowTips("Exception:" + e.BackGroundException.Message);
            }
        }

        private void Run()
        {
            FrmSet();

            RefreshConfig(); //参数配置

            LoadMethod();// API测试

            SetupCheck();//启动前检查

            //btnTCP_Click(null, null); // 服务启动
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
                if (!SetupAppServer())
                {
                    btnTCP.Enabled = true;
                    return;
                }
            }
            if (mesServer.State != ServerState.Running)
            {
                mesServer.Start();
                btnTCP.Text = " 关闭数据服务器";
                btnTCP.Image = Properties.Resources.end;
                PicBoxTCP.Image = null;
                PicBoxTCP.Image = Properties.Resources.connect;
                lblTCP.Text = "MES服务器地址:" + mesServer.Listeners[0].EndPoint.Address;
                txtMsg.AppendText(DateTime.Now + ">> MES服务器启动成功" + Environment.NewLine);
                txtScroll.ScrollText = DateTime.Now + ">> MES服务器启动成功";
                lvServer.Groups["lvgPort"].Header = string.Format("服务端监听端口 [{0}]", mesServer.Listeners.Count());
                lvServer.Groups["lvgPort"].Items.Clear();

                AppConfig config = new AppConfig();
                foreach (ListenerInfo item in mesServer.Listeners)
                {
                    //tab1 ListView 添加节点
                    string localDeviceName = config.AppConfigGet(item.EndPoint.Port.ToString());
                    localDeviceName = string.Format("{0} [{1}]", localDeviceName, item.EndPoint);

                    ListViewItem lvItem = new ListViewItem(localDeviceName, 0, lvServer.Groups["lvgPort"]);
                    lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, mesServer.StartedTime.ToString("yyyy-MM-dd HH:mm:ss"), Color.DarkGray, Color.White, new Font("微软雅黑", 8f)));
                    lvServer.Items.Add(lvItem);

                    //tab2 Treeview 添加节点
                    TreeNode node = new TreeNode(localDeviceName, 0, 0);
                    node.Name = item.EndPoint.ToString();
                    node.Checked = true;
                    tvServer.Nodes.Add(node);
                }
            }
            else
            {
                mesServer.Stop();
                btnTCP.Text = " 启动数据服务器";
                btnTCP.Image = Properties.Resources.start;
                PicBoxTCP.Image = null;
                PicBoxTCP.Image = Properties.Resources.disconnect;
                txtMsg.AppendText(DateTime.Now + ">> MES服务器已关闭" + Environment.NewLine);
                txtScroll.ScrollText = DateTime.Now + ">> MES服务器已关闭";
                lblTCP.Text = "MES服务器地址:";

                //tab1 ListView清空
                lvServer.Groups["lvgPort"].Items.Clear();
                lvServer.Items.Clear();

                //tab2 Treeview清空
                tvServer.Nodes.Clear();
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
                }

                //tab1 连接关闭ListView节点递减 
                ListViewItem lvttem = lvServer.FindItemWithText(session.RemoteDeviceName.ToString());
                if (lvttem != null)
                {
                    lvServer.Items.Remove(lvttem);
                    lvServer.Groups["lvgClient"].Header = string.Format("在线客户端 [{0}]", lvServer.Groups["lvgClient"].Items.Count);
                }

                //tab2 连接关闭TreeView节点递减
                TreeNode[] nodes = tvServer.Nodes.Find(session.RemoteEndPoint.ToString(), true);
                foreach (TreeNode node in nodes)
                {
                    tvServer.Nodes.Remove(node);
                }

                TeartbeatShowStateInfo(listAllView.Items.Count, session.RemoteEndPoint + " 已断开连接，原因：" + value);
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
                lvi.SubItems.Add(session.RemoteDeviceName);
                lvi.SubItems.Add(session.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                lvi.SubItems.Add(session.Config.Mode.ToString());
                listAllView.Items.Add(lvi);
                listAllView.EndUpdate();

                //tab1 新连接ListView节点递增 
                lvServer.BeginUpdate();
                ListViewItem lvItem = new ListViewItem(session.RemoteDeviceName, 1, lvServer.Groups["lvgClient"]);
                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, session.StartTime.ToString("yyyy-MM-dd HH:mm:ss"), Color.DarkGray, Color.White, new Font("微软雅黑", 8f)));
                lvServer.Items.Add(lvItem);
                lvServer.Groups["lvgClient"].Header = string.Format("在线客户端 [{0}]", lvServer.Groups["lvgClient"].Items.Count);
                lvServer.EndUpdate();

                //tab2 新连接TreeView节点递增 
                //TreeNode node = new TreeNode("Client " + session.RemoteEndPoint, 1, 1);
                TreeNode node = new TreeNode(session.RemoteDeviceName, 1, 1);
                node.Name = session.RemoteEndPoint.ToString();
                node.Checked = true;
                int index = tvServer.Nodes.IndexOfKey(session.LocalEndPoint.ToString());
                if (index >= 0)
                {
                    tvServer.Nodes[index].Nodes.Add(node);
                }
                tvServer.ExpandAll();
                TeartbeatShowStateInfo(listAllView.Items.Count, session.RemoteEndPoint + " 成功连接至服务器中心.");

            }));
        }

        #endregion

        /// <summary>
        /// 信息添加
        /// </summary>
        /// <param name="msg"></param>
        void ServerShowStateInfo(LogInfo log)
        {
            queueServer.EnqueueItem(log);
        }

        private void QueueServer_ProcessItem(LogInfo log)
        {
            this.Invoke(new ThreadStart(delegate
            {
                if (!log.IsView) return;

                if (richLog.Lines.Count() > 10000)
                {
                    richLog.ResetText();
                }
                switch (log.Level)
                {
                    case "Error":
                        richLog.AppendRichText(log.ToString(), new Font("微软雅黑", 9f), Color.Crimson);

                        break;
                    default:
                        richLog.AppendRichText(log.ToString(), new Font("微软雅黑", 9f), Color.FromArgb(51, 51, 51));
                        break;
                }
                richLog.AppendText(Environment.NewLine);
                //将光标位置设置到当前内容的末尾
                //richLog.SelectionStart = richLog.Text.Length;
                //滚动到光标位置
                richLog.ScrollToCaret();
            }));
        }
        /// <summary>
        /// 连接计数
        /// </summary>
        void TeartbeatShowStateInfo(int num, string msg)
        {
            this.Invoke(new ThreadStart(delegate
            {
                txtScroll.ScrollText = msg;
                lblClientCount.Text = num.ToString();
            }));
        }

        #endregion

        #endregion

        /// <summary>
        /// 初始化服务器
        /// </summary>
        private bool SetupAppServer()
        {
            if (mesServer == null)
            {
                //方法一、采用当前应用程序中的【App.config】文件。
                var bootstrap = BootstrapFactory.CreateBootstrap();
                if (!bootstrap.Initialize())
                { 
                    txtMsg.AppendRichText(DateTime.Now + ">> 初始化服务器配置文件失败，请检查." + Environment.NewLine, new Font("微软雅黑", 9), Color.Crimson);
                    return false;
                }
                StartResult startResult = bootstrap.Start();
                if (startResult == StartResult.Success)
                {
                    txtMsg.AppendText(DateTime.Now + ">> 初始化服务器成功." + Environment.NewLine);
                    mesServer = bootstrap.AppServers.Cast<MesServer>().FirstOrDefault();
                    mesServer.Stop();
                    //mesServer.Start();
                    return true;
                }
                else
                { 
                    txtMsg.AppendRichText(DateTime.Now + ">> 初始化服务器失败,请检查服务器设置是否正确." + Environment.NewLine, new Font("微软雅黑", 9), Color.Crimson);
                    return false;
                }
            }
            return true;
        }

        private async void tsmpMenu_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem tsmp = sender as ToolStripMenuItem;

                if (tvServer.SelectedNode != null)
                {
                    TreeNode node = tvServer.SelectedNode;
                    if (node.Level == 1)
                    {
                        MesSession session = GlobalData.ClientSessionList.Find(s => s.RemoteDeviceName == node.Text);
                        if (session != null)
                        {
                            Frm_Msg frm = new Frm_Msg();
                            frm.Text = session.RemoteDeviceName;
                            frm.gbMsg.Text = tsmp.Text;
                            frm.txtMsg.Text = tsmp.Tag.ToString();
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                string msg = frm.txtMsg.Text.Trim() + Environment.NewLine;

                                EasyClient<StringPackageInfo> tcpClient = new EasyClient<StringPackageInfo>();
                                tcpClient.Initialize(new MesClientTerminatorReceiveFiltercs());
                                var connected = await tcpClient.ConnectAsync(session.LocalEndPoint);
                                if (tcpClient.IsConnected)
                                {
                                    var data = Encoding.UTF8.GetBytes(msg.ToString());
                                    tcpClient.Send(new ArraySegment<byte>(data, 0, data.Length));
                                    if (tcpClient != null)
                                    { 
                                        await tcpClient.Close();
                                        tcpClient = null;
                                    }
                                }
                                else
                                {
                                    MessageUtilSkin.ShowError("连接失败.");
                                }

                            }
                        }
                        else
                        {
                            MessageUtilSkin.ShowTips("不存在该session请确认.");
                        }
                    }
                    else
                    {
                        MessageUtilSkin.ShowTips("请选择客户端节点进行操作.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageUtilSkin.ShowTips(ex.Message);
            }
        }
        
        #region 实时监控

        private void tvServer_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                TreeViewHelper.TreeNodeCheck(e.Node);
                MesSession session = null;
                switch (e.Node.Level)
                {
                    case 0:
                        session = GlobalData.ClientSessionList.Find(s => s.LocalDeviceName == e.Node.Text);
                        break;

                    case 1:
                        session = GlobalData.ClientSessionList.Find(s => s.RemoteDeviceName == e.Node.Text);
                        break;
                }

                if (session == null) return;
                session.IsView = e.Node.Checked;
            }
        }

        private void tvServer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = tvServer.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    tvServer.SelectedNode = CurrentNode;//选中这个节点
                }

            }
        }

        #endregion

        //清空记录
        private void tsmClear_Click(object sender, EventArgs e)
        {
            richLog.ResetText();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string filePath = string.Format(@"{0}\Logs\{1}.log", DirectoryUtil.GetCurrentDirectory(), DateTime.Now.ToString("yyyy-MM-dd"));

            if (FileUtil.FileIsExist(filePath))
            {
                System.Diagnostics.Process.Start(filePath);
            }
            else
            {
                MessageUtilSkin.ShowTips(string.Format("不存在文件：{0}", filePath));
            }
        }

        #region API测试

        private void LoadMethod()
        {
            string filePath = string.Format(@"{0}\{1}.dll", DirectoryUtil.GetCurrentDirectory(), "DM_API");

            if (FileUtil.FileIsExist(filePath))
            {
                try
                {
                    Assembly aBox = Assembly.LoadFrom(FileUtil.GetFileName(filePath));
                    Type[] _t = aBox.GetTypes(); //获得全部Type
                    foreach (Type t in _t)
                    {
                        if (t.Namespace == "DM_API" && t.Name == "DM_SFCInterface")
                        {
                            MethodInfo[] methods = t.GetMethods();

                            LoadMethodTree(methods);
                        }
                    }
                    //MessageBox.Show("File \"AboutBox.dll\" Invalid!\n\nAssembly Name Error.");
                } //文件、命名空间、方法都相符，但执行该DLL 内容出错
                catch (System.NullReferenceException ex)
                {
                    safelbl.Text = ">>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：File \"AboutBox.dll\" Invalid! ";
                }
                catch (Exception ex)
                {
                    safelbl.Text = ">>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：File \"AboutBox.dll\" Error: \n\n" + ex.Message;
                }
            }
            else
            {
                safelbl.Text = ">>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：File \"AboutBox.dll\" Missing! ";
            }

        }
        private void btnRefMethods_Click(object sender, EventArgs e)
        {
            LoadMethod();
        }
        private void LoadMethodTree(MethodInfo[] methods)
        {
            tvMethod.Nodes.Clear();

            foreach (MethodInfo info in methods)
            {
                TreeNode node = new TreeNode(info.Name, 5, 5);
                node.Tag = info;
                tvMethod.Nodes.Add(node);
            }
        }

        private void tvMethod_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvMethod.SelectedNode != null)
            {
                MethodInfo info = tvMethod.SelectedNode.Tag as MethodInfo;
                ParameterInfo[] parameters = info.GetParameters();
                txtParme.Text = string.Empty;
                txtParme.ReadOnly = true;
                StringBuilder sbParam = new StringBuilder();
                for (int i = 0; i < parameters.Length; i++)
                {
                    txtParme.ReadOnly = false;
                    sbParam.AppendFormat("【{0}({1})】", parameters[i].Name, parameters[i].ParameterType);
                }

                List<ParameterInfo> listParam = parameters.ToList();

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("所属程序集：{0} \r\n\r\n", info.Module);
                sb.AppendFormat("方法名称：{0} \r\n\r\n", info.Name);
                sb.AppendFormat("执行参数：{0} \r\n\r\n", (sbParam.Length == 0) ? "空" : sbParam.ToString());
                sb.AppendFormat("返回类型：{0} \r\n", info.ReturnType);

                txtMethodInfo.Text = sb.ToString();
            }
        }
        private void btnAction_Click(object sender, EventArgs e)
        {
            if (tvMethod.SelectedNode != null)
            {
                tsPB.Visible = true;
                safelbl.Text = ">>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：正在执行API.";
                btnAction.Enabled = false;

                string sRet = string.Format("{0}：", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                try
                {
                    AppConfig appconfig = new AppConfig();
                    int apiTimeout = ConvertHelper.ToInt32(appconfig.AppConfigGet("ApiTimeout"), 1000);
                    MethodInfo mInfo = tvMethod.SelectedNode.Tag as MethodInfo;
                    //创建实例
                    object o = Activator.CreateInstance(mInfo.DeclaringType);
                    object obj;
                    string retMessage = string.Empty;
                    if (txtParme.Text == string.Empty)
                    {
                        obj = TaskHelper.RunTaskWithTimeout<object>((Func<object>)delegate { return mInfo.Invoke(o, new object[] { }); }, apiTimeout, ref retMessage);
                    }
                    else
                    {
                        obj = TaskHelper.RunTaskWithTimeout<object>((Func<object>)delegate { return mInfo.Invoke(o, StringHelper.GetStrArray(txtParme.Text.Trim(), ',', false).ToArray()); }, apiTimeout, ref retMessage);
                    }
                    sRet += (obj == null) ? retMessage : obj.ToString();
                }
                catch (TargetException tex)
                {
                    sRet += tex.Message + ((tex.InnerException != null) ? tex.InnerException.ToString() : "");
                }
                catch (ArgumentException arex)
                {
                    sRet += arex.Message + ((arex.InnerException != null) ? arex.InnerException.ToString() : "");
                }
                catch (TargetInvocationException tix)
                {
                    sRet += tix.Message + ((tix.InnerException != null) ? tix.InnerException.ToString() : "");
                }
                catch (TargetParameterCountException tpex)
                {
                    sRet += tpex.Message + ((tpex.InnerException != null) ? tpex.InnerException.ToString() : "");
                }
                catch (MethodAccessException mex)
                {
                    sRet += mex.Message + ((mex.InnerException != null) ? mex.InnerException.ToString() : "");
                }
                catch (InvalidOperationException ioex)
                {
                    sRet += ioex.Message + ((ioex.InnerException != null) ? ioex.InnerException.ToString() : "");
                }
                catch (NotSupportedException noex)
                {
                    sRet += noex.Message + ((noex.InnerException != null) ? noex.InnerException.ToString() : "");
                }
                catch (Exception ex)
                {
                    sRet += ex.Message + ((ex.InnerException != null) ? ex.InnerException.ToString() : "");
                }


                txtMethodRet.Text = sRet;
                btnAction.Enabled = true;

                tsPB.Visible = false;
                safelbl.Text = ">>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：API执行结束.";
            }
            else
            {
                MessageUtilSkin.ShowTips("请选择API.");
            }
        }

        #endregion

        #region 参数设置
        private void btnSaveStorageId_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string comm = StringUtil.RemovePrefix(btn.Name, "btnSave_");
            Control txtTemp = tPanelSet.Controls.Find("txt_" + comm, true)[0];

            switch (btn.Text.Trim())
            {
                case "设置":
                    btn.Image = Properties.Resources.btnSave;
                    btn.Text = " 保存";
                    CallCtrlWithThreadSafety.SetEnable(txtTemp, true, this);
                    break;
                case "保存":
                    btn.Image = Properties.Resources.btnSet;
                    btn.Text = " 设置";
                    CallCtrlWithThreadSafety.SetEnable(txtTemp, false, this);

                    string sValue = txtTemp.GetType().GetProperty("Text").GetValue(txtTemp, null).ToString();
                    AppConfig config = new AppConfig();
                    config.AppConfigSet(comm, sValue);
                    ConfigurationManager.RefreshSection("appSettings");
                    safelbl.Text = string.Format("{0} >> 保存成功.", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"));

                    break;
            }
        }

        private void btnRefresh_StorageId_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string comm = StringUtil.RemovePrefix(btn.Name, "btnRefresh_");
            Control txtTemp = tPanelSet.Controls.Find("txt_" + comm, true)[0];

            AppConfig config = new AppConfig();
            string value = config.AppConfigGet(comm);

            txtTemp.Text = value;
            safelbl.Text = string.Format("{0} >> 刷新成功.", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"));
        }

        private void RefreshConfig()
        {
            try
            {
                AppConfig config = new AppConfig();
                foreach (Control c in tPanelSet.Controls)
                {
                    if (c.Name.StartsWith("txt_"))
                    {
                        YunTextBox yTextbox = c as YunTextBox;

                        string comm = StringUtil.RemovePrefix(c.Name, "txt_");
                        string value = config.AppConfigGet(comm);
                        yTextbox.Text = value;
                        CallCtrlWithThreadSafety.SetEnable(yTextbox, false, this);
                    }
                }
                safelbl.Text = string.Format("{0} >> 全部刷新成功.", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"));
            }
            catch (Exception ex)
            {
                safelbl.Text = string.Format("{0} >> 刷新异常：{1}.", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"), ex.Message);
            }
        }

        private void btnRefreshAll_Click(object sender, EventArgs e)
        {
            RefreshConfig();
        }


        #endregion

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //如果我们操作【×】按钮，那么不关闭程序而是缩小化到托盘，并提示用户.
            if (this.WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;//不关闭程序

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

        private void notifyMenu_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowInTaskbar = false; 
                Application.Exit();
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
            if (this == null)
            {
                return;
            }

            //最小化到托盘的时候显示图标提示信息
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.ShowBalloonTip(3000, "程序最小化提示",
                    "图标已经缩小到托盘，打开窗口请双击图标即可。也可以使用Alt+S键来显示/隐藏窗体。",
                    ToolTipIcon.Info);
            }
        }

    }
}

