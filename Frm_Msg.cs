using CCWin.SkinControl;
using SuperSocket.ClientEngine;
using SuperSocket.ProtoBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using YUN.Framework.BaseUI;
using YUN.Framework.Commons;

namespace MES.SocketService
{
    public partial class Frm_Msg : BaseForm
    {
        private List<Protocol> lstProtocol = new List<Protocol>();
        private EasyClient<StringPackageInfo> tcpClient = new EasyClient<StringPackageInfo>();
        public IPEndPoint EndPoint;
        private bool isLineDebug = false;

        public Frm_Msg()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 在缓冲区重绘
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            isLineDebug = false;
            var data = Encoding.UTF8.GetBytes(txtMsg.Text.Trim() + Environment.NewLine);
            tcpClient.Send(new ArraySegment<byte>(data, 0, data.Length));
            WriteInfo($"发送 {txtMsg.Text.Trim()}");
        }

        private void btnStopListen_Click(object sender, EventArgs e)
        {
            TcpClose();
        }

        private void TcpClose()
        {
            if (tcpClient != null)
            {
                tcpClient.Close();
                tcpClient = null;
            }
        }

        private void Frm_Msg_Load(object sender, EventArgs e)
        {
            cmbDevCode.SelectedIndex = 0;
            cmbProRe.SelectedIndex = 0;
            cmbSTP.SelectedIndex = 0;

            txtServer.Text = EndPoint.ToString();
            ConnectServer();
            LoadTxtFile("");
            LoadProtocolFile("");
        }

        private async void ConnectServer()
        {
            tcpClient.Initialize(new MesClientTerminatorReceiveFiltercs());
            tcpClient.Error += TcpClient_Error;
            tcpClient.NewPackageReceived += TcpClient_NewPackageReceived;
            var connected = await tcpClient.ConnectAsync(EndPoint);
            if (!tcpClient.IsConnected)
            {
                lblStatus.Text = $"{DateTime.Now} >> 连接失败.";
                return;
            }

            lblStatus.Text = $"{DateTime.Now} >> 连接成功.";
            txtClient.Text = tcpClient.Socket.LocalEndPoint.ToString();
        }

        private void TcpClient_NewPackageReceived(object sender, PackageEventArgs<StringPackageInfo> e)
        {
            try
            {
                WriteInfo($"接收 {e.Package.Body}");

                if (!isLineDebug)
                    return;

                TransData transData = TransData.GetInstance(e.Package.Parameters);
                SetTreeNodeStatus(transData);
                if (transData.Status == "OK")
                {
                    Protocol item = lstProtocol.Find(li => li.IsSend == false && li.ParentID != "null");
                    if (item != null)
                    {
                        Thread.Sleep(60);
                        var data = Encoding.UTF8.GetBytes(item.Content.Trim() + Environment.NewLine);
                        tcpClient.Send(new ArraySegment<byte>(data, 0, data.Length));
                        item.IsSend = true;
                    }
                    else
                    {
                        LblSuccessNotice("");
                    }
                }

            }
            catch (Exception ex)
            {
                WriteInfo($"接收 {e.Package.Body},错误：{ex.ToString()}");
            }

        }

        private void SetTreeNodeStatus(TransData transData)
        {
            this.Invoke(new ThreadStart(delegate
            {
                tvProtocol.BeginUpdate();

                //设置工单
                if (transData.FuncCode == "WOR")
                {
                    lblOrder.Text = transData.ProcessData;
                }
                //设置SN
                if (transData.FuncCode == "PTR")
                {
                    lblSN.Text = transData.SN;

                    lstProtocol.ForEach(li =>
                    {
                        if (li.ParentID != "null")
                        {
                            string[] arrs = li.Content.Split(';');
                            arrs[3] = transData.SN;
                            li.Content = StringHelper.GetArrayStr(arrs.ToList(), ";");
                        }
                    });
                }

                Protocol item = lstProtocol.FindLast(li => li.IsSend == true && li.ParentID != "null");
                if (item != null)
                {
                    TreeNode node = tvProtocol.Nodes.Find($"{item.ParentID}_{item.ID}", true).FirstOrDefault();
                    node.Text = $"{item.Name} ---> [{item.Content}] <{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}>";
                    if (transData.Status == "OK")
                    {
                        node.ImageIndex = 3;
                        node.SelectedImageIndex = 3;
                        node.ForeColor = Color.DarkCyan;
                    }
                    else
                    {
                        node.ImageIndex = 2;
                        node.SelectedImageIndex = 2;
                        node.ForeColor = Color.Crimson;
                    }
                    node.EnsureVisible();// 滚动条

                    bool finish = true;
                    foreach (TreeNode tNode in node.Parent.Nodes)
                    {
                        if (tNode.ImageIndex != 3)//存在没完成的项目
                            finish = false;
                    }

                    TreeNode pNode = tvProtocol.Nodes.Find($"{item.ParentID}", true).FirstOrDefault();
                    if (finish)
                    {
                        pNode.ImageIndex = 3;
                        pNode.SelectedImageIndex = 3;
                    }
                    else
                    {
                        pNode.ImageIndex = 1;
                        pNode.SelectedImageIndex = 1;
                    }
                    tvProtocol.Refresh();
                }
                tvProtocol.EndUpdate();
            }));
        }

        private void LblSuccessNotice(string str)
        {
            lblSuccess.Invoke(new ThreadStart(delegate
            {
                btnDebug.Enabled = true;
                lblSuccess.Text = $"{DateTime.Now.ToString("fff")}--->恭喜，测试通过!!! ";
            }));

        }


        private void WriteInfo(string info)
        {
            this.Invoke(new ThreadStart(delegate
            {
                lblStatus.Text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} >> {info}" + Environment.NewLine;
                lblProtocolExec.Text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} >> {info}" + Environment.NewLine;
                richLog.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} >> {info}" + Environment.NewLine);
                //将光标位置设置到当前内容的末尾
                //richLog.SelectionStart = richLog.Text.Length;
                //滚动到光标位置
                richLog.ScrollToCaret();
            }));
        }

        private void TcpClient_Error(object sender, ErrorEventArgs e)
        {
            this.Invoke(
                new ThreadStart(
                    delegate
                    {
                        lblStatus.Text = $"{DateTime.Now} >> {e.Exception.Message}.";
                    }));
        }

        private void Frm_Msg_FormClosed(object sender, FormClosedEventArgs e)
        {
            TcpClose();
        }

        #region 读取本地文件

        private void LoadTxtFile(string txtPath)
        {
            if (string.IsNullOrWhiteSpace(txtPath))
                txtPath = $@"{DirectoryUtil.GetCurrentDirectory()}\Docs\protocol.txt";

            if (!FileUtil.FileIsExist(txtPath))
                return;

            string sFullText = FileUtil.FileToString(txtPath);
            string[] arrComms = sFullText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            flowPanelComms.Controls.Clear();
            foreach (string str in arrComms)
            {
                SkinButton btn = new SkinButton();

                #region btnStyle

                btn.IsDrawGlass = false;
                btn.AutoSize = true;
                btn.InnerBorderColor = Color.Transparent;
                btn.RoundStyle = CCWin.SkinClass.RoundStyle.All;
                btn.Radius = 4;
                btn.BorderColor = Color.FromArgb(250, 250, 250);
                btn.BaseColor = Color.FromArgb(2, 79, 142); ;
                btn.DownBaseColor = Color.FromArgb(2, 79, 142);
                btn.MouseBaseColor = Color.FromArgb(2, 79, 142);
                btn.FadeGlow = false;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Tahoma", 9);
                btn.Cursor = Cursors.Hand;
                btn.Click += Btn_Click;

                #endregion

                ArrayList al = StringUtil.ExtractInnerContent(str, "[", "]");
                btn.Text = al[0].ToString();
                btn.Tag = al[1].ToString();

                flowPanelComms.Controls.Add(btn);
            }
        }
        private void LoadProtocolFile(string xmlPath)
        {
            tvProtocol.BeginUpdate();

            tvProtocol.Nodes.Clear();

            if (string.IsNullOrWhiteSpace(xmlPath))
                xmlPath = $@"{DirectoryUtil.GetCurrentDirectory()}\Docs\protocolDetail.xml";

            if (!FileUtil.FileIsExist(xmlPath))
                return;

            XmlHelper xml = new XmlHelper(xmlPath);
            DataSet ds = xml.GetData("Protocols");

            lstProtocol = DataTableHelper.DataTableToList<Protocol>(ds.Tables[0]).ToList();

            foreach (Protocol pItem in lstProtocol)
            {
                if (pItem.ParentID == "null")
                {
                    TreeNode node = new TreeNode(pItem.ID, 0, 0);
                    node.Name = pItem.ID;
                    node.Tag = pItem.Content;
                    tvProtocol.Nodes.Add(node);
                }
            }
            foreach (Protocol pItem in lstProtocol)
            {
                if (pItem.ParentID != "null")
                {
                    TreeNode node = new TreeNode($"{pItem.Name}---[{pItem.Content}]", 0, 0);
                    //TreeNode node = new TreeNode($"{pItem.Name}", 0, 0);
                    node.Name = $"{pItem.ParentID}_{pItem.ID}";
                    node.Tag = pItem.Content;
                    TreeNode pNode = tvProtocol.Nodes[pItem.ParentID];
                    pNode.Nodes.Add(node);
                }
            }

            tvProtocol.ExpandAll();
            tvProtocol.SelectedNode = tvProtocol.Nodes[0];
            tvProtocol.Nodes[0].EnsureVisible();
            tvProtocol.EndUpdate();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            txtMsg.Text = GenerateProtocol(btn.Tag.ToString());
            btn.Select();
        }

        #endregion

        #region 动态生成协议

        private string GenerateProtocol(string baseProtocol)
        {
            try
            {
                List<string> lstComm = StringHelper.GetStrArray(baseProtocol, ';', false);
                if (lstComm[0] != "HEA")
                {
                    if (cmbDevCode.SelectedIndex != -1)
                    {
                        lstComm[1] = cmbDevCode.Text.Trim();
                    }
                    if (cmbSTP.SelectedIndex != -1)
                    {
                        lstComm[2] = cmbSTP.Text.Trim();
                    }
                    if (lstComm[3] != "##" && txtSN.Text.Trim() != string.Empty)
                    {
                        lstComm[3] = txtSN.Text.Trim();
                    }
                    if (cmbProRe.SelectedIndex != -1)
                    {
                        lstComm[5] = cmbProRe.Text.Trim();
                    }
                }

                return StringHelper.GetArrayStr(lstComm, ";");
            }
            catch (Exception)
            {
                return baseProtocol;
            }
        }

        private void cmbDevCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMsg.Text.Trim())) return;
            txtMsg.Text = GenerateProtocol(txtMsg.Text.Trim());
        }

        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (string.IsNullOrEmpty(txtMsg.Text.Trim())) return;
                txtMsg.Text = GenerateProtocol(txtMsg.Text.Trim());
            }
        }

        #endregion

        private void btnDebug_Click(object sender, EventArgs e)
        {
            try
            {
                isLineDebug = true;
                btnDebug.Enabled = false;

                //初始化 
                LoadProtocolFile("");
                tvProtocol.SelectedNode = null;

                lblSuccess.Text = "";
                Thread.Sleep(50);

                Protocol item = lstProtocol.Find(li => li.IsSend == false && li.ParentID != "null");
                if (!string.IsNullOrEmpty(item.Content))
                {
                    var data = Encoding.UTF8.GetBytes(item.Content + Environment.NewLine);
                    tcpClient.Send(new ArraySegment<byte>(data, 0, data.Length));
                    WriteInfo($"发送 {item.Content}");
                    item.IsSend = true;
                }
            }
            catch (Exception ex)
            {
                MessageUtilSkin.ShowError(ex.Message);
            }
        }

        private void lnkUnLock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnDebug.Enabled = true;
        }
    }

    public class Protocol
    {
        public Protocol()
        {
            IsSend = false;
        }

        public string ID { get; set; }
        public string ParentID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Count { get; set; }
        public bool IsSend { get; set; }
    }
}
