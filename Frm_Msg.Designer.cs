namespace MES.SocketService
{
    partial class Frm_Msg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Msg));
            this.btnStopListen = new CCWin.SkinControl.SkinButton();
            this.btnListen = new CCWin.SkinControl.SkinButton();
            this.skinGroupBox1 = new CCWin.SkinControl.SkinGroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtClient = new YUN.Framework.Commons.YunTextBox();
            this.txtServer = new YUN.Framework.Commons.YunTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.titlePanel1 = new YUN.Framework.Commons.TitlePanel();
            this.richLog = new CCWin.SkinControl.SkinChatRichTextBox();
            this.titlePanel2 = new YUN.Framework.Commons.TitlePanel();
            this.cmbProRe = new CCWin.SkinControl.SkinComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbDevCode = new CCWin.SkinControl.SkinComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSTP = new CCWin.SkinControl.SkinComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSN = new YUN.Framework.Commons.YunTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.flowPanelComms = new CCWin.SkinControl.SkinFlowLayoutPanel();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.skinButton2 = new CCWin.SkinControl.SkinButton();
            this.skinButton3 = new CCWin.SkinControl.SkinButton();
            this.skinButton4 = new CCWin.SkinControl.SkinButton();
            this.skinButton5 = new CCWin.SkinControl.SkinButton();
            this.skinButton6 = new CCWin.SkinControl.SkinButton();
            this.skinButton7 = new CCWin.SkinControl.SkinButton();
            this.skinButton8 = new CCWin.SkinControl.SkinButton();
            this.skinButton9 = new CCWin.SkinControl.SkinButton();
            this.txtMsg = new YUN.Framework.Commons.YunTextBox();
            this.tabControlLogType = new CCWin.SkinControl.SkinTabControl();
            this.skinTabPage6 = new CCWin.SkinControl.SkinTabPage();
            this.skinTabPage7 = new CCWin.SkinControl.SkinTabPage();
            this.lblProtocolExec = new CCWin.SkinControl.SkinLabel();
            this.skinSplitContainer1 = new CCWin.SkinControl.SkinSplitContainer();
            this.skinPanel1 = new CCWin.SkinControl.SkinPanel();
            this.tvProtocol = new CCWin.SkinControl.SkinTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lnkUnLock = new System.Windows.Forms.LinkLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblSN = new System.Windows.Forms.Label();
            this.lblOrder = new System.Windows.Forms.Label();
            this.lblSuccess = new System.Windows.Forms.Label();
            this.btnDebug = new CCWin.SkinControl.SkinButton();
            this.skinGroupBox1.SuspendLayout();
            this.titlePanel1.SuspendLayout();
            this.titlePanel2.SuspendLayout();
            this.flowPanelComms.SuspendLayout();
            this.tabControlLogType.SuspendLayout();
            this.skinTabPage6.SuspendLayout();
            this.skinTabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer1)).BeginInit();
            this.skinSplitContainer1.Panel1.SuspendLayout();
            this.skinSplitContainer1.Panel2.SuspendLayout();
            this.skinSplitContainer1.SuspendLayout();
            this.skinPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStopListen
            // 
            this.btnStopListen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopListen.BackColor = System.Drawing.Color.Transparent;
            this.btnStopListen.BaseColor = System.Drawing.Color.Gainsboro;
            this.btnStopListen.BorderColor = System.Drawing.Color.LightGray;
            this.btnStopListen.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnStopListen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStopListen.DownBack = null;
            this.btnStopListen.DownBaseColor = System.Drawing.Color.Gainsboro;
            this.btnStopListen.Image = ((System.Drawing.Image)(resources.GetObject("btnStopListen.Image")));
            this.btnStopListen.ImageSize = new System.Drawing.Size(16, 16);
            this.btnStopListen.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnStopListen.IsEnabledDraw = false;
            this.btnStopListen.Location = new System.Drawing.Point(1340, 654);
            this.btnStopListen.MouseBack = null;
            this.btnStopListen.Name = "btnStopListen";
            this.btnStopListen.NormlBack = null;
            this.btnStopListen.Radius = 4;
            this.btnStopListen.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.btnStopListen.Size = new System.Drawing.Size(100, 28);
            this.btnStopListen.TabIndex = 12;
            this.btnStopListen.Text = " 退出";
            this.btnStopListen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStopListen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStopListen.UseVisualStyleBackColor = false;
            this.btnStopListen.Click += new System.EventHandler(this.btnStopListen_Click);
            // 
            // btnListen
            // 
            this.btnListen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnListen.BackColor = System.Drawing.Color.Transparent;
            this.btnListen.BaseColor = System.Drawing.Color.Gainsboro;
            this.btnListen.BorderColor = System.Drawing.Color.LightGray;
            this.btnListen.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnListen.DownBack = null;
            this.btnListen.DownBaseColor = System.Drawing.Color.Gainsboro;
            this.btnListen.Image = ((System.Drawing.Image)(resources.GetObject("btnListen.Image")));
            this.btnListen.ImageSize = new System.Drawing.Size(16, 16);
            this.btnListen.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnListen.IsEnabledDraw = false;
            this.btnListen.Location = new System.Drawing.Point(1234, 654);
            this.btnListen.MouseBack = null;
            this.btnListen.Name = "btnListen";
            this.btnListen.NormlBack = null;
            this.btnListen.Radius = 4;
            this.btnListen.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.btnListen.Size = new System.Drawing.Size(100, 28);
            this.btnListen.TabIndex = 11;
            this.btnListen.Text = " 发送";
            this.btnListen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnListen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnListen.UseVisualStyleBackColor = false;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // skinGroupBox1
            // 
            this.skinGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.skinGroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.skinGroupBox1.Controls.Add(this.lblStatus);
            this.skinGroupBox1.Controls.Add(this.txtClient);
            this.skinGroupBox1.Controls.Add(this.txtServer);
            this.skinGroupBox1.Controls.Add(this.label2);
            this.skinGroupBox1.Controls.Add(this.label1);
            this.skinGroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.skinGroupBox1.Location = new System.Drawing.Point(14, 45);
            this.skinGroupBox1.Name = "skinGroupBox1";
            this.skinGroupBox1.RectBackColor = System.Drawing.Color.White;
            this.skinGroupBox1.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinGroupBox1.Size = new System.Drawing.Size(1450, 55);
            this.skinGroupBox1.TabIndex = 11;
            this.skinGroupBox1.TabStop = false;
            this.skinGroupBox1.Text = "连接信息";
            this.skinGroupBox1.TitleBorderColor = System.Drawing.Color.Transparent;
            this.skinGroupBox1.TitleRectBackColor = System.Drawing.Color.White;
            this.skinGroupBox1.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.lblStatus.Location = new System.Drawing.Point(52, 22);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(188, 22);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "2019年9月2日16:11:16";
            // 
            // txtClient
            // 
            this.txtClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClient.BackColor = System.Drawing.Color.Transparent;
            this.txtClient.BorderColor = System.Drawing.Color.LightGray;
            this.txtClient.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtClient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txtClient.HeightLightBolorColor = System.Drawing.Color.LightGray;
            this.txtClient.Image = null;
            this.txtClient.ImageSize = new System.Drawing.Size(0, 0);
            this.txtClient.InputType = YUN.Framework.Commons.YunTextBox.NumTextBoxType.String;
            this.txtClient.Lines = new string[] {
        "127.0.0.1"};
            this.txtClient.Location = new System.Drawing.Point(1209, 21);
            this.txtClient.Multiline = true;
            this.txtClient.Name = "txtClient";
            this.txtClient.NormalBackrColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.txtClient.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtClient.PasswordChar = '\0';
            this.txtClient.ReadOnly = true;
            this.txtClient.ReadOnlyEnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.txtClient.Required = false;
            this.txtClient.Size = new System.Drawing.Size(234, 23);
            this.txtClient.TabIndex = 7;
            this.txtClient.TabStop = false;
            this.txtClient.Text = "127.0.0.1";
            // 
            // txtServer
            // 
            this.txtServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServer.BackColor = System.Drawing.Color.Transparent;
            this.txtServer.BorderColor = System.Drawing.Color.LightGray;
            this.txtServer.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txtServer.HeightLightBolorColor = System.Drawing.Color.LightGray;
            this.txtServer.Image = null;
            this.txtServer.ImageSize = new System.Drawing.Size(0, 0);
            this.txtServer.InputType = YUN.Framework.Commons.YunTextBox.NumTextBoxType.String;
            this.txtServer.Lines = new string[] {
        "127.0.0.1"};
            this.txtServer.Location = new System.Drawing.Point(884, 21);
            this.txtServer.Multiline = true;
            this.txtServer.Name = "txtServer";
            this.txtServer.NormalBackrColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.txtServer.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtServer.PasswordChar = '\0';
            this.txtServer.ReadOnly = true;
            this.txtServer.ReadOnlyEnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.txtServer.Required = false;
            this.txtServer.Size = new System.Drawing.Size(234, 23);
            this.txtServer.TabIndex = 6;
            this.txtServer.TabStop = false;
            this.txtServer.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1124, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "客户端信息：";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.label1.Location = new System.Drawing.Point(799, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "服务端信息：";
            // 
            // titlePanel1
            // 
            this.titlePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titlePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.titlePanel1.BorderColor = System.Drawing.Color.LightGray;
            this.titlePanel1.Controls.Add(this.richLog);
            this.titlePanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.titlePanel1.Location = new System.Drawing.Point(3, 3);
            this.titlePanel1.Name = "titlePanel1";
            this.titlePanel1.Padding = new System.Windows.Forms.Padding(0, 28, 0, 0);
            this.titlePanel1.Size = new System.Drawing.Size(1440, 254);
            this.titlePanel1.TabIndex = 207;
            this.titlePanel1.Title = "数据接收及提升窗口";
            this.titlePanel1.TitleAlign = YUN.Framework.Commons.TitleAlignment.Left;
            this.titlePanel1.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.titlePanel1.TitleBackImage = null;
            this.titlePanel1.TitleBorderColor = System.Drawing.Color.LightGray;
            this.titlePanel1.TitleFont = new System.Drawing.Font("Tahoma", 9F);
            this.titlePanel1.TitleHeight = 28;
            this.titlePanel1.TitleImage = ((System.Drawing.Image)(resources.GetObject("titlePanel1.TitleImage")));
            this.titlePanel1.TitleXOffset = 8;
            // 
            // richLog
            // 
            this.richLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.richLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richLog.Font = new System.Drawing.Font("Tahoma", 10F);
            this.richLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.richLog.Location = new System.Drawing.Point(3, 31);
            this.richLog.Name = "richLog";
            this.richLog.ReadOnly = true;
            this.richLog.SelectControl = null;
            this.richLog.SelectControlIndex = 0;
            this.richLog.SelectControlPoint = new System.Drawing.Point(0, 0);
            this.richLog.Size = new System.Drawing.Size(1434, 220);
            this.richLog.TabIndex = 0;
            this.richLog.Text = "";
            // 
            // titlePanel2
            // 
            this.titlePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titlePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.titlePanel2.BorderColor = System.Drawing.Color.LightGray;
            this.titlePanel2.Controls.Add(this.cmbProRe);
            this.titlePanel2.Controls.Add(this.label7);
            this.titlePanel2.Controls.Add(this.cmbDevCode);
            this.titlePanel2.Controls.Add(this.label6);
            this.titlePanel2.Controls.Add(this.cmbSTP);
            this.titlePanel2.Controls.Add(this.label5);
            this.titlePanel2.Controls.Add(this.label4);
            this.titlePanel2.Controls.Add(this.txtSN);
            this.titlePanel2.Controls.Add(this.label3);
            this.titlePanel2.Controls.Add(this.flowPanelComms);
            this.titlePanel2.Controls.Add(this.txtMsg);
            this.titlePanel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.titlePanel2.Location = new System.Drawing.Point(3, 262);
            this.titlePanel2.Name = "titlePanel2";
            this.titlePanel2.Padding = new System.Windows.Forms.Padding(0, 28, 0, 0);
            this.titlePanel2.Size = new System.Drawing.Size(1440, 386);
            this.titlePanel2.TabIndex = 208;
            this.titlePanel2.Title = "数据发送窗口[文本模式]   ";
            this.titlePanel2.TitleAlign = YUN.Framework.Commons.TitleAlignment.Left;
            this.titlePanel2.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.titlePanel2.TitleBackImage = null;
            this.titlePanel2.TitleBorderColor = System.Drawing.Color.LightGray;
            this.titlePanel2.TitleFont = new System.Drawing.Font("Tahoma", 9F);
            this.titlePanel2.TitleHeight = 28;
            this.titlePanel2.TitleImage = ((System.Drawing.Image)(resources.GetObject("titlePanel2.TitleImage")));
            this.titlePanel2.TitleXOffset = 8;
            // 
            // cmbProRe
            // 
            this.cmbProRe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProRe.BaseColor = System.Drawing.Color.Gainsboro;
            this.cmbProRe.BorderColor = System.Drawing.Color.LightGray;
            this.cmbProRe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProRe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProRe.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbProRe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cmbProRe.ItemHeight = 19;
            this.cmbProRe.Items.AddRange(new object[] {
            "OK_RE",
            "NG_RE"});
            this.cmbProRe.Location = new System.Drawing.Point(1352, 2);
            this.cmbProRe.Name = "cmbProRe";
            this.cmbProRe.Size = new System.Drawing.Size(80, 25);
            this.cmbProRe.TabIndex = 103;
            this.cmbProRe.WaterText = "";
            this.cmbProRe.SelectedIndexChanged += new System.EventHandler(this.cmbDevCode_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(1294, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 14);
            this.label7.TabIndex = 102;
            this.label7.Text = "协议请求";
            // 
            // cmbDevCode
            // 
            this.cmbDevCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDevCode.BaseColor = System.Drawing.Color.Gainsboro;
            this.cmbDevCode.BorderColor = System.Drawing.Color.LightGray;
            this.cmbDevCode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDevCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDevCode.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbDevCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cmbDevCode.ItemHeight = 19;
            this.cmbDevCode.Items.AddRange(new object[] {
            "OP010",
            "OP020",
            "OP030",
            "OP040",
            "OP050",
            "OP060",
            "OP070",
            "OP080",
            "OP090",
            "OP100",
            "OP110",
            "OP120",
            "OP130",
            "OP140",
            "OP150",
            "OP160",
            "OP170",
            "OP180",
            "OP190",
            "OP200",
            "OP210",
            "OP220",
            "OP230",
            "OP240",
            "OP250",
            "OP260",
            "OP270",
            "OP280",
            "OP290",
            "OP300",
            "OP310",
            "OP320",
            "OP330",
            "OP340",
            "OP350"});
            this.cmbDevCode.Location = new System.Drawing.Point(842, 2);
            this.cmbDevCode.Name = "cmbDevCode";
            this.cmbDevCode.Size = new System.Drawing.Size(100, 25);
            this.cmbDevCode.TabIndex = 101;
            this.cmbDevCode.WaterText = "";
            this.cmbDevCode.SelectedIndexChanged += new System.EventHandler(this.cmbDevCode_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(784, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 14);
            this.label6.TabIndex = 100;
            this.label6.Text = "机台编码";
            // 
            // cmbSTP
            // 
            this.cmbSTP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSTP.BaseColor = System.Drawing.Color.Gainsboro;
            this.cmbSTP.BorderColor = System.Drawing.Color.LightGray;
            this.cmbSTP.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSTP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSTP.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbSTP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cmbSTP.ItemHeight = 19;
            this.cmbSTP.Items.AddRange(new object[] {
            "STP01",
            "STP02",
            "STP03",
            "STP04",
            "STP05",
            "STP06"});
            this.cmbSTP.Location = new System.Drawing.Point(1003, 2);
            this.cmbSTP.Name = "cmbSTP";
            this.cmbSTP.Size = new System.Drawing.Size(80, 25);
            this.cmbSTP.TabIndex = 99;
            this.cmbSTP.WaterText = "";
            this.cmbSTP.SelectedIndexChanged += new System.EventHandler(this.cmbDevCode_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(945, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 11;
            this.label5.Text = "操作序号";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(1086, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "SN";
            // 
            // txtSN
            // 
            this.txtSN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSN.BackColor = System.Drawing.Color.Transparent;
            this.txtSN.BorderColor = System.Drawing.Color.LightGray;
            this.txtSN.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtSN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txtSN.HeightLightBolorColor = System.Drawing.Color.LightGray;
            this.txtSN.Image = null;
            this.txtSN.ImageSize = new System.Drawing.Size(0, 0);
            this.txtSN.InputType = YUN.Framework.Commons.YunTextBox.NumTextBoxType.String;
            this.txtSN.Lines = new string[0];
            this.txtSN.Location = new System.Drawing.Point(1111, 3);
            this.txtSN.Name = "txtSN";
            this.txtSN.NormalBackrColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.txtSN.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtSN.PasswordChar = '\0';
            this.txtSN.ReadOnlyEnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.txtSN.Required = false;
            this.txtSN.Size = new System.Drawing.Size(180, 23);
            this.txtSN.TabIndex = 9;
            this.txtSN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSN_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(3, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "指令集  ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowPanelComms
            // 
            this.flowPanelComms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowPanelComms.BackColor = System.Drawing.Color.Transparent;
            this.flowPanelComms.Controls.Add(this.skinButton1);
            this.flowPanelComms.Controls.Add(this.skinButton2);
            this.flowPanelComms.Controls.Add(this.skinButton3);
            this.flowPanelComms.Controls.Add(this.skinButton4);
            this.flowPanelComms.Controls.Add(this.skinButton5);
            this.flowPanelComms.Controls.Add(this.skinButton6);
            this.flowPanelComms.Controls.Add(this.skinButton7);
            this.flowPanelComms.Controls.Add(this.skinButton8);
            this.flowPanelComms.Controls.Add(this.skinButton9);
            this.flowPanelComms.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.flowPanelComms.DownBack = null;
            this.flowPanelComms.Location = new System.Drawing.Point(69, 351);
            this.flowPanelComms.MouseBack = null;
            this.flowPanelComms.Name = "flowPanelComms";
            this.flowPanelComms.NormlBack = null;
            this.flowPanelComms.Size = new System.Drawing.Size(1368, 32);
            this.flowPanelComms.TabIndex = 6;
            // 
            // skinButton1
            // 
            this.skinButton1.AutoSize = true;
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.skinButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.DownBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.skinButton1.FadeGlow = false;
            this.skinButton1.ForeColor = System.Drawing.Color.White;
            this.skinButton1.InnerBorderColor = System.Drawing.Color.Transparent;
            this.skinButton1.IsDrawGlass = false;
            this.skinButton1.Location = new System.Drawing.Point(3, 3);
            this.skinButton1.MouseBack = null;
            this.skinButton1.MouseBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Radius = 4;
            this.skinButton1.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinButton1.Size = new System.Drawing.Size(99, 24);
            this.skinButton1.TabIndex = 0;
            this.skinButton1.Text = "[工单余量请求]";
            this.skinButton1.UseVisualStyleBackColor = false;
            // 
            // skinButton2
            // 
            this.skinButton2.AutoSize = true;
            this.skinButton2.BackColor = System.Drawing.Color.Transparent;
            this.skinButton2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.skinButton2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.skinButton2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton2.DownBack = null;
            this.skinButton2.ForeColor = System.Drawing.Color.White;
            this.skinButton2.InnerBorderColor = System.Drawing.Color.Transparent;
            this.skinButton2.IsDrawGlass = false;
            this.skinButton2.Location = new System.Drawing.Point(108, 3);
            this.skinButton2.MouseBack = null;
            this.skinButton2.Name = "skinButton2";
            this.skinButton2.NormlBack = null;
            this.skinButton2.Palace = true;
            this.skinButton2.Radius = 4;
            this.skinButton2.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinButton2.Size = new System.Drawing.Size(99, 24);
            this.skinButton2.TabIndex = 1;
            this.skinButton2.Text = "[条码打印请求]";
            this.skinButton2.UseVisualStyleBackColor = false;
            // 
            // skinButton3
            // 
            this.skinButton3.AutoSize = true;
            this.skinButton3.BackColor = System.Drawing.Color.Transparent;
            this.skinButton3.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.skinButton3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.skinButton3.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton3.DownBack = null;
            this.skinButton3.ForeColor = System.Drawing.Color.White;
            this.skinButton3.InnerBorderColor = System.Drawing.Color.Transparent;
            this.skinButton3.IsDrawGlass = false;
            this.skinButton3.Location = new System.Drawing.Point(213, 3);
            this.skinButton3.MouseBack = null;
            this.skinButton3.Name = "skinButton3";
            this.skinButton3.NormlBack = null;
            this.skinButton3.Radius = 4;
            this.skinButton3.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinButton3.Size = new System.Drawing.Size(99, 24);
            this.skinButton3.TabIndex = 2;
            this.skinButton3.Text = "[打印条码校验]";
            this.skinButton3.UseVisualStyleBackColor = false;
            // 
            // skinButton4
            // 
            this.skinButton4.AutoSize = true;
            this.skinButton4.BackColor = System.Drawing.Color.Transparent;
            this.skinButton4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.skinButton4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.skinButton4.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton4.DownBack = null;
            this.skinButton4.ForeColor = System.Drawing.Color.White;
            this.skinButton4.InnerBorderColor = System.Drawing.Color.Transparent;
            this.skinButton4.IsDrawGlass = false;
            this.skinButton4.Location = new System.Drawing.Point(318, 3);
            this.skinButton4.MouseBack = null;
            this.skinButton4.Name = "skinButton4";
            this.skinButton4.NormlBack = null;
            this.skinButton4.Radius = 4;
            this.skinButton4.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinButton4.Size = new System.Drawing.Size(88, 24);
            this.skinButton4.TabIndex = 3;
            this.skinButton4.Text = "[前工序校验]";
            this.skinButton4.UseVisualStyleBackColor = false;
            // 
            // skinButton5
            // 
            this.skinButton5.AutoSize = true;
            this.skinButton5.BackColor = System.Drawing.Color.Transparent;
            this.skinButton5.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.skinButton5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.skinButton5.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton5.DownBack = null;
            this.skinButton5.ForeColor = System.Drawing.Color.White;
            this.skinButton5.InnerBorderColor = System.Drawing.Color.Transparent;
            this.skinButton5.IsDrawGlass = false;
            this.skinButton5.Location = new System.Drawing.Point(412, 3);
            this.skinButton5.MouseBack = null;
            this.skinButton5.Name = "skinButton5";
            this.skinButton5.NormlBack = null;
            this.skinButton5.Radius = 4;
            this.skinButton5.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinButton5.Size = new System.Drawing.Size(99, 24);
            this.skinButton5.TabIndex = 4;
            this.skinButton5.Text = "[通用过站请求]";
            this.skinButton5.UseVisualStyleBackColor = false;
            // 
            // skinButton6
            // 
            this.skinButton6.AutoSize = true;
            this.skinButton6.BackColor = System.Drawing.Color.Transparent;
            this.skinButton6.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.skinButton6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.skinButton6.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton6.DownBack = null;
            this.skinButton6.ForeColor = System.Drawing.Color.White;
            this.skinButton6.InnerBorderColor = System.Drawing.Color.Transparent;
            this.skinButton6.IsDrawGlass = false;
            this.skinButton6.Location = new System.Drawing.Point(517, 3);
            this.skinButton6.MouseBack = null;
            this.skinButton6.Name = "skinButton6";
            this.skinButton6.NormlBack = null;
            this.skinButton6.Radius = 4;
            this.skinButton6.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinButton6.Size = new System.Drawing.Size(99, 24);
            this.skinButton6.TabIndex = 5;
            this.skinButton6.Text = "[装配工序校验]";
            this.skinButton6.UseVisualStyleBackColor = false;
            // 
            // skinButton7
            // 
            this.skinButton7.AutoSize = true;
            this.skinButton7.BackColor = System.Drawing.Color.Transparent;
            this.skinButton7.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.skinButton7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.skinButton7.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton7.DownBack = null;
            this.skinButton7.ForeColor = System.Drawing.Color.White;
            this.skinButton7.InnerBorderColor = System.Drawing.Color.Transparent;
            this.skinButton7.IsDrawGlass = false;
            this.skinButton7.Location = new System.Drawing.Point(622, 3);
            this.skinButton7.MouseBack = null;
            this.skinButton7.Name = "skinButton7";
            this.skinButton7.NormlBack = null;
            this.skinButton7.Radius = 4;
            this.skinButton7.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinButton7.Size = new System.Drawing.Size(159, 24);
            this.skinButton7.TabIndex = 6;
            this.skinButton7.Text = "[检验工序过站（值类型）]";
            this.skinButton7.UseVisualStyleBackColor = false;
            // 
            // skinButton8
            // 
            this.skinButton8.AutoSize = true;
            this.skinButton8.BackColor = System.Drawing.Color.Transparent;
            this.skinButton8.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.skinButton8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.skinButton8.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton8.DownBack = null;
            this.skinButton8.ForeColor = System.Drawing.Color.White;
            this.skinButton8.InnerBorderColor = System.Drawing.Color.Transparent;
            this.skinButton8.IsDrawGlass = false;
            this.skinButton8.Location = new System.Drawing.Point(787, 3);
            this.skinButton8.MouseBack = null;
            this.skinButton8.Name = "skinButton8";
            this.skinButton8.NormlBack = null;
            this.skinButton8.Radius = 4;
            this.skinButton8.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinButton8.Size = new System.Drawing.Size(99, 24);
            this.skinButton8.TabIndex = 7;
            this.skinButton8.Text = "[通用数据采集]";
            this.skinButton8.UseVisualStyleBackColor = false;
            // 
            // skinButton9
            // 
            this.skinButton9.AutoSize = true;
            this.skinButton9.BackColor = System.Drawing.Color.Transparent;
            this.skinButton9.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.skinButton9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.skinButton9.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton9.DownBack = null;
            this.skinButton9.ForeColor = System.Drawing.Color.White;
            this.skinButton9.InnerBorderColor = System.Drawing.Color.Transparent;
            this.skinButton9.IsDrawGlass = false;
            this.skinButton9.Location = new System.Drawing.Point(892, 3);
            this.skinButton9.MouseBack = null;
            this.skinButton9.Name = "skinButton9";
            this.skinButton9.NormlBack = null;
            this.skinButton9.Radius = 4;
            this.skinButton9.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinButton9.Size = new System.Drawing.Size(159, 24);
            this.skinButton9.TabIndex = 8;
            this.skinButton9.Text = "[料箱与栈位关联关系确认]";
            this.skinButton9.UseVisualStyleBackColor = false;
            // 
            // txtMsg
            // 
            this.txtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsg.BackColor = System.Drawing.Color.Transparent;
            this.txtMsg.BorderColor = System.Drawing.Color.LightGray;
            this.txtMsg.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txtMsg.HeightLightBolorColor = System.Drawing.Color.LightGray;
            this.txtMsg.Image = null;
            this.txtMsg.ImageSize = new System.Drawing.Size(0, 0);
            this.txtMsg.InputType = YUN.Framework.Commons.YunTextBox.NumTextBoxType.String;
            this.txtMsg.Lines = new string[] {
        "127.0.0.1"};
            this.txtMsg.Location = new System.Drawing.Point(3, 31);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.NormalBackrColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.txtMsg.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtMsg.PasswordChar = '\0';
            this.txtMsg.ReadOnlyEnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.txtMsg.Required = false;
            this.txtMsg.Size = new System.Drawing.Size(1434, 317);
            this.txtMsg.TabIndex = 4;
            this.txtMsg.Text = "127.0.0.1";
            // 
            // tabControlLogType
            // 
            this.tabControlLogType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlLogType.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.tabControlLogType.ArrowBorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tabControlLogType.ArrowColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tabControlLogType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.tabControlLogType.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.tabControlLogType.Controls.Add(this.skinTabPage6);
            this.tabControlLogType.Controls.Add(this.skinTabPage7);
            this.tabControlLogType.HeadBack = null;
            this.tabControlLogType.HeadPalace = true;
            this.tabControlLogType.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.tabControlLogType.ItemSize = new System.Drawing.Size(150, 30);
            this.tabControlLogType.Location = new System.Drawing.Point(14, 106);
            this.tabControlLogType.Name = "tabControlLogType";
            this.tabControlLogType.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("tabControlLogType.PageArrowDown")));
            this.tabControlLogType.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("tabControlLogType.PageArrowHover")));
            this.tabControlLogType.PageBorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tabControlLogType.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("tabControlLogType.PageCloseHover")));
            this.tabControlLogType.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("tabControlLogType.PageCloseNormal")));
            this.tabControlLogType.PageDown = ((System.Drawing.Image)(resources.GetObject("tabControlLogType.PageDown")));
            this.tabControlLogType.PageDownTxtColor = System.Drawing.Color.White;
            this.tabControlLogType.PageHover = null;
            this.tabControlLogType.PageHoverTxtColor = System.Drawing.Color.White;
            this.tabControlLogType.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.tabControlLogType.PageNorml = null;
            this.tabControlLogType.PageNormlTxtColor = System.Drawing.Color.White;
            this.tabControlLogType.SelectedIndex = 1;
            this.tabControlLogType.Size = new System.Drawing.Size(1450, 720);
            this.tabControlLogType.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlLogType.TabIndex = 209;
            // 
            // skinTabPage6
            // 
            this.skinTabPage6.BackColor = System.Drawing.Color.White;
            this.skinTabPage6.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.skinTabPage6.Controls.Add(this.titlePanel2);
            this.skinTabPage6.Controls.Add(this.btnListen);
            this.skinTabPage6.Controls.Add(this.titlePanel1);
            this.skinTabPage6.Controls.Add(this.btnStopListen);
            this.skinTabPage6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPage6.Location = new System.Drawing.Point(0, 30);
            this.skinTabPage6.Name = "skinTabPage6";
            this.skinTabPage6.Size = new System.Drawing.Size(1450, 690);
            this.skinTabPage6.TabIndex = 0;
            this.skinTabPage6.TabItemImage = null;
            this.skinTabPage6.Text = "单步测试";
            // 
            // skinTabPage7
            // 
            this.skinTabPage7.BackColor = System.Drawing.Color.White;
            this.skinTabPage7.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.skinTabPage7.Controls.Add(this.lblProtocolExec);
            this.skinTabPage7.Controls.Add(this.skinSplitContainer1);
            this.skinTabPage7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPage7.Location = new System.Drawing.Point(0, 30);
            this.skinTabPage7.Name = "skinTabPage7";
            this.skinTabPage7.Size = new System.Drawing.Size(1450, 690);
            this.skinTabPage7.TabIndex = 1;
            this.skinTabPage7.TabItemImage = null;
            this.skinTabPage7.Text = "整线测试";
            // 
            // lblProtocolExec
            // 
            this.lblProtocolExec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProtocolExec.BackColor = System.Drawing.Color.Transparent;
            this.lblProtocolExec.BorderColor = System.Drawing.Color.Transparent;
            this.lblProtocolExec.BorderSize = 0;
            this.lblProtocolExec.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblProtocolExec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.lblProtocolExec.ForeColorSuit = true;
            this.lblProtocolExec.Location = new System.Drawing.Point(4, 656);
            this.lblProtocolExec.Name = "lblProtocolExec";
            this.lblProtocolExec.Size = new System.Drawing.Size(1440, 33);
            this.lblProtocolExec.TabIndex = 185;
            this.lblProtocolExec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // skinSplitContainer1
            // 
            this.skinSplitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinSplitContainer1.ArroColor = System.Drawing.Color.White;
            this.skinSplitContainer1.ArroHoverColor = System.Drawing.Color.White;
            this.skinSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.skinSplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.skinSplitContainer1.LineBack = System.Drawing.Color.White;
            this.skinSplitContainer1.LineBack2 = System.Drawing.Color.White;
            this.skinSplitContainer1.Location = new System.Drawing.Point(4, 3);
            this.skinSplitContainer1.Name = "skinSplitContainer1";
            // 
            // skinSplitContainer1.Panel1
            // 
            this.skinSplitContainer1.Panel1.Controls.Add(this.skinPanel1);
            // 
            // skinSplitContainer1.Panel2
            // 
            this.skinSplitContainer1.Panel2.Controls.Add(this.lnkUnLock);
            this.skinSplitContainer1.Panel2.Controls.Add(this.label9);
            this.skinSplitContainer1.Panel2.Controls.Add(this.label8);
            this.skinSplitContainer1.Panel2.Controls.Add(this.lblSN);
            this.skinSplitContainer1.Panel2.Controls.Add(this.lblOrder);
            this.skinSplitContainer1.Panel2.Controls.Add(this.lblSuccess);
            this.skinSplitContainer1.Panel2.Controls.Add(this.btnDebug);
            this.skinSplitContainer1.Size = new System.Drawing.Size(1440, 650);
            this.skinSplitContainer1.SplitterDistance = 1094;
            this.skinSplitContainer1.TabIndex = 184;
            // 
            // skinPanel1
            // 
            this.skinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel1.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.skinPanel1.Controls.Add(this.tvProtocol);
            this.skinPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(0, 0);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(1094, 650);
            this.skinPanel1.TabIndex = 0;
            // 
            // tvProtocol
            // 
            this.tvProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvProtocol.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.tvProtocol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvProtocol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tvProtocol.FullRowSelect = true;
            this.tvProtocol.HideSelection = false;
            this.tvProtocol.ImageIndex = 0;
            this.tvProtocol.ImageList = this.imageList1;
            this.tvProtocol.Indent = 24;
            this.tvProtocol.ItemHeight = 25;
            this.tvProtocol.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tvProtocol.Location = new System.Drawing.Point(9, 3);
            this.tvProtocol.Name = "tvProtocol";
            this.tvProtocol.SelectedImageIndex = 0;
            this.tvProtocol.Size = new System.Drawing.Size(1074, 639);
            this.tvProtocol.TabIndex = 9;
            this.tvProtocol.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "箭头D.png");
            this.imageList1.Images.SetKeyName(1, "箭头Y.png");
            this.imageList1.Images.SetKeyName(2, "箭头R.png");
            this.imageList1.Images.SetKeyName(3, "箭头G.png");
            // 
            // lnkUnLock
            // 
            this.lnkUnLock.AutoSize = true;
            this.lnkUnLock.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lnkUnLock.Location = new System.Drawing.Point(293, 51);
            this.lnkUnLock.Name = "lnkUnLock";
            this.lnkUnLock.Size = new System.Drawing.Size(35, 14);
            this.lnkUnLock.TabIndex = 190;
            this.lnkUnLock.TabStop = true;
            this.lnkUnLock.Text = "解 锁";
            this.lnkUnLock.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUnLock_LinkClicked);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.label9.Location = new System.Drawing.Point(5, 181);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 25);
            this.label9.TabIndex = 188;
            this.label9.Text = "产品条码:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.label8.Location = new System.Drawing.Point(5, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 25);
            this.label8.TabIndex = 187;
            this.label8.Text = "当前工单:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSN
            // 
            this.lblSN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSN.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblSN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.lblSN.Location = new System.Drawing.Point(70, 181);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(265, 25);
            this.lblSN.TabIndex = 186;
            this.lblSN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrder
            // 
            this.lblOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrder.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.lblOrder.Location = new System.Drawing.Point(70, 146);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(267, 25);
            this.lblOrder.TabIndex = 185;
            this.lblOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSuccess
            // 
            this.lblSuccess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSuccess.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.lblSuccess.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblSuccess.Location = new System.Drawing.Point(8, 89);
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(327, 25);
            this.lblSuccess.TabIndex = 184;
            this.lblSuccess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDebug
            // 
            this.btnDebug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDebug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.btnDebug.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.btnDebug.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnDebug.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnDebug.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDebug.DownBack = null;
            this.btnDebug.DownBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.btnDebug.FadeGlow = false;
            this.btnDebug.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.btnDebug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.btnDebug.ImageSize = new System.Drawing.Size(24, 24);
            this.btnDebug.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnDebug.IsDrawGlass = false;
            this.btnDebug.Location = new System.Drawing.Point(52, 20);
            this.btnDebug.MouseBack = null;
            this.btnDebug.MouseBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(79)))), ((int)(((byte)(142)))));
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.NormlBack = null;
            this.btnDebug.Radius = 4;
            this.btnDebug.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.btnDebug.Size = new System.Drawing.Size(235, 45);
            this.btnDebug.TabIndex = 183;
            this.btnDebug.Text = "测试开始";
            this.btnDebug.UseVisualStyleBackColor = false;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // Frm_Msg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1476, 833);
            this.Controls.Add(this.skinGroupBox1);
            this.Controls.Add(this.tabControlLogType);
            this.MinimumSize = new System.Drawing.Size(1293, 686);
            this.Name = "Frm_Msg";
            this.Text = "主动调试窗口";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_Msg_FormClosed);
            this.Load += new System.EventHandler(this.Frm_Msg_Load);
            this.skinGroupBox1.ResumeLayout(false);
            this.skinGroupBox1.PerformLayout();
            this.titlePanel1.ResumeLayout(false);
            this.titlePanel2.ResumeLayout(false);
            this.titlePanel2.PerformLayout();
            this.flowPanelComms.ResumeLayout(false);
            this.flowPanelComms.PerformLayout();
            this.tabControlLogType.ResumeLayout(false);
            this.skinTabPage6.ResumeLayout(false);
            this.skinTabPage7.ResumeLayout(false);
            this.skinSplitContainer1.Panel1.ResumeLayout(false);
            this.skinSplitContainer1.Panel2.ResumeLayout(false);
            this.skinSplitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer1)).EndInit();
            this.skinSplitContainer1.ResumeLayout(false);
            this.skinPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private CCWin.SkinControl.SkinButton btnStopListen;
        private CCWin.SkinControl.SkinButton btnListen;
        public CCWin.SkinControl.SkinGroupBox skinGroupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public YUN.Framework.Commons.YunTextBox txtServer;
        public YUN.Framework.Commons.YunTextBox txtClient;
        private System.Windows.Forms.Label lblStatus;
        private YUN.Framework.Commons.TitlePanel titlePanel1;
        private CCWin.SkinControl.SkinChatRichTextBox richLog;
        private YUN.Framework.Commons.TitlePanel titlePanel2;
        public YUN.Framework.Commons.YunTextBox txtMsg;
        private CCWin.SkinControl.SkinFlowLayoutPanel flowPanelComms;
        private CCWin.SkinControl.SkinButton skinButton1;
        private CCWin.SkinControl.SkinButton skinButton2;
        private CCWin.SkinControl.SkinButton skinButton3;
        private CCWin.SkinControl.SkinButton skinButton4;
        private CCWin.SkinControl.SkinButton skinButton5;
        private CCWin.SkinControl.SkinButton skinButton6;
        private CCWin.SkinControl.SkinButton skinButton7;
        private CCWin.SkinControl.SkinButton skinButton8;
        private CCWin.SkinControl.SkinButton skinButton9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public YUN.Framework.Commons.YunTextBox txtSN;
        private System.Windows.Forms.Label label5;
        private CCWin.SkinControl.SkinComboBox cmbSTP;
        private CCWin.SkinControl.SkinComboBox cmbDevCode;
        private System.Windows.Forms.Label label6;
        private CCWin.SkinControl.SkinComboBox cmbProRe;
        private System.Windows.Forms.Label label7;
        private CCWin.SkinControl.SkinTabControl tabControlLogType;
        private CCWin.SkinControl.SkinTabPage skinTabPage6;
        private CCWin.SkinControl.SkinTabPage skinTabPage7;
        private CCWin.SkinControl.SkinTreeView tvProtocol;
        private CCWin.SkinControl.SkinButton btnDebug;
        private System.Windows.Forms.ImageList imageList1;
        private CCWin.SkinControl.SkinSplitContainer skinSplitContainer1;
        private CCWin.SkinControl.SkinPanel skinPanel1;
        private CCWin.SkinControl.SkinLabel lblProtocolExec;
        private System.Windows.Forms.Label lblSuccess;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel lnkUnLock;
    }
}