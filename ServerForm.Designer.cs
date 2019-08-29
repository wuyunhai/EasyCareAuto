namespace MES.SocketService
{
    partial class ServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.titlePanel1 = new YUN.Framework.Commons.TitlePanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServerIP = new YUN.Framework.Commons.YunTextBox();
            this.txtServerPort = new YUN.Framework.Commons.YunTextBox();
            this.btnListen = new CCWin.SkinControl.SkinButton();
            this.btnStopListen = new CCWin.SkinControl.SkinButton();
            this.cbAutoSend = new CCWin.SkinControl.SkinCheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInterval = new YUN.Framework.Commons.YunTextBox();
            this.skinGroupBox3 = new CCWin.SkinControl.SkinGroupBox();
            this.rtbData = new YUN.Framework.Commons.YunTextBox();
            this.skinTabControl1 = new CCWin.SkinControl.SkinTabControl();
            this.skinTabPage3 = new CCWin.SkinControl.SkinTabPage();
            this.skinTabPage4 = new CCWin.SkinControl.SkinTabPage();
            this.skinPanel1 = new CCWin.SkinControl.SkinPanel();
            this.skinPanel2 = new CCWin.SkinControl.SkinPanel();
            this.btnClearLog = new CCWin.SkinControl.SkinButton();
            this.cbLog = new CCWin.SkinControl.SkinCheckBox();
            this.btnOpenLog = new CCWin.SkinControl.SkinButton();
            this.PacketView = new CCWin.SkinControl.SkinListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.titlePanel2 = new YUN.Framework.Commons.TitlePanel();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.rbUdp = new CCWin.SkinControl.SkinRadioButton();
            this.rbTcp = new CCWin.SkinControl.SkinRadioButton();
            this.skinGroupBox2 = new CCWin.SkinControl.SkinGroupBox();
            this.titlePanel1.SuspendLayout();
            this.skinGroupBox3.SuspendLayout();
            this.skinTabControl1.SuspendLayout();
            this.skinTabPage3.SuspendLayout();
            this.skinTabPage4.SuspendLayout();
            this.skinPanel1.SuspendLayout();
            this.titlePanel2.SuspendLayout();
            this.skinGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlePanel1
            // 
            this.titlePanel1.BorderColor = System.Drawing.Color.LightGray;
            this.titlePanel1.Controls.Add(this.titlePanel2);
            this.titlePanel1.Controls.Add(this.skinGroupBox3);
            this.titlePanel1.Controls.Add(this.skinGroupBox2);
            this.titlePanel1.Controls.Add(this.btnStopListen);
            this.titlePanel1.Controls.Add(this.btnListen);
            this.titlePanel1.Controls.Add(this.txtServerPort);
            this.titlePanel1.Controls.Add(this.txtServerIP);
            this.titlePanel1.Controls.Add(this.label2);
            this.titlePanel1.Controls.Add(this.label1);
            this.titlePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel1.Location = new System.Drawing.Point(0, 0);
            this.titlePanel1.Name = "titlePanel1";
            this.titlePanel1.Padding = new System.Windows.Forms.Padding(0, 28, 0, 0);
            this.titlePanel1.Size = new System.Drawing.Size(1564, 247);
            this.titlePanel1.TabIndex = 0;
            this.titlePanel1.Title = "服务器信息";
            this.titlePanel1.TitleAlign = YUN.Framework.Commons.TitleAlignment.Left;
            this.titlePanel1.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.titlePanel1.TitleBackImage = null;
            this.titlePanel1.TitleBorderColor = System.Drawing.Color.LightGray;
            this.titlePanel1.TitleFont = new System.Drawing.Font("Tahoma", 9F);
            this.titlePanel1.TitleHeight = 28;
            this.titlePanel1.TitleImage = ((System.Drawing.Image)(resources.GetObject("titlePanel1.TitleImage")));
            this.titlePanel1.TitleXOffset = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器IP：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "监听端口：";
            // 
            // txtServerIP
            // 
            this.txtServerIP.BackColor = System.Drawing.Color.Transparent;
            this.txtServerIP.BorderColor = System.Drawing.Color.LightGray;
            this.txtServerIP.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtServerIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txtServerIP.HeightLightBolorColor = System.Drawing.Color.LightGray;
            this.txtServerIP.Image = null;
            this.txtServerIP.ImageSize = new System.Drawing.Size(0, 0);
            this.txtServerIP.InputType = YUN.Framework.Commons.YunTextBox.NumTextBoxType.String;
            this.txtServerIP.Lines = new string[] {
        "127.0.0.1"};
            this.txtServerIP.Location = new System.Drawing.Point(77, 39);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.NormalBackrColor = System.Drawing.Color.White;
            this.txtServerIP.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtServerIP.PasswordChar = '\0';
            this.txtServerIP.ReadOnly = true;
            this.txtServerIP.ReadOnlyEnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.txtServerIP.Required = false;
            this.txtServerIP.Size = new System.Drawing.Size(200, 25);
            this.txtServerIP.TabIndex = 2;
            this.txtServerIP.Text = "127.0.0.1";
            // 
            // txtServerPort
            // 
            this.txtServerPort.BackColor = System.Drawing.Color.Transparent;
            this.txtServerPort.BorderColor = System.Drawing.Color.LightGray;
            this.txtServerPort.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtServerPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txtServerPort.HeightLightBolorColor = System.Drawing.Color.LightGray;
            this.txtServerPort.Image = null;
            this.txtServerPort.ImageSize = new System.Drawing.Size(0, 0);
            this.txtServerPort.InputType = YUN.Framework.Commons.YunTextBox.NumTextBoxType.String;
            this.txtServerPort.Lines = new string[] {
        "4999"};
            this.txtServerPort.Location = new System.Drawing.Point(356, 39);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.NormalBackrColor = System.Drawing.Color.White;
            this.txtServerPort.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtServerPort.PasswordChar = '\0';
            this.txtServerPort.ReadOnly = true;
            this.txtServerPort.ReadOnlyEnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.txtServerPort.Required = false;
            this.txtServerPort.Size = new System.Drawing.Size(200, 25);
            this.txtServerPort.TabIndex = 3;
            this.txtServerPort.Text = "4999";
            // 
            // btnListen
            // 
            this.btnListen.BackColor = System.Drawing.Color.Transparent;
            this.btnListen.BaseColor = System.Drawing.Color.Gainsboro;
            this.btnListen.BorderColor = System.Drawing.Color.LightGray;
            this.btnListen.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnListen.DownBack = null;
            this.btnListen.DownBaseColor = System.Drawing.Color.Gainsboro;
            this.btnListen.Image = ((System.Drawing.Image)(resources.GetObject("btnListen.Image")));
            this.btnListen.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnListen.IsEnabledDraw = false;
            this.btnListen.Location = new System.Drawing.Point(577, 39);
            this.btnListen.MouseBack = null;
            this.btnListen.Name = "btnListen";
            this.btnListen.NormlBack = null;
            this.btnListen.Radius = 4;
            this.btnListen.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.btnListen.Size = new System.Drawing.Size(130, 40);
            this.btnListen.TabIndex = 4;
            this.btnListen.Text = " 开始监听";
            this.btnListen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnListen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnListen.UseVisualStyleBackColor = false;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // btnStopListen
            // 
            this.btnStopListen.BackColor = System.Drawing.Color.Transparent;
            this.btnStopListen.BaseColor = System.Drawing.Color.Gainsboro;
            this.btnStopListen.BorderColor = System.Drawing.Color.LightGray;
            this.btnStopListen.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnStopListen.DownBack = null;
            this.btnStopListen.DownBaseColor = System.Drawing.Color.Gainsboro;
            this.btnStopListen.Enabled = false;
            this.btnStopListen.Image = ((System.Drawing.Image)(resources.GetObject("btnStopListen.Image")));
            this.btnStopListen.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnStopListen.IsEnabledDraw = false;
            this.btnStopListen.Location = new System.Drawing.Point(577, 85);
            this.btnStopListen.MouseBack = null;
            this.btnStopListen.Name = "btnStopListen";
            this.btnStopListen.NormlBack = null;
            this.btnStopListen.Radius = 4;
            this.btnStopListen.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.btnStopListen.Size = new System.Drawing.Size(130, 40);
            this.btnStopListen.TabIndex = 5;
            this.btnStopListen.Text = " 停止监听";
            this.btnStopListen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStopListen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStopListen.UseVisualStyleBackColor = false;
            this.btnStopListen.Click += new System.EventHandler(this.btnStopListen_Click);
            // 
            // cbAutoSend
            // 
            this.cbAutoSend.AutoSize = true;
            this.cbAutoSend.BackColor = System.Drawing.Color.Transparent;
            this.cbAutoSend.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cbAutoSend.Checked = true;
            this.cbAutoSend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoSend.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.cbAutoSend.DefaultCheckButtonWidth = 16;
            this.cbAutoSend.DownBack = ((System.Drawing.Image)(resources.GetObject("cbAutoSend.DownBack")));
            this.cbAutoSend.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbAutoSend.LightEffect = false;
            this.cbAutoSend.LightEffectBack = System.Drawing.Color.Black;
            this.cbAutoSend.LightEffectWidth = 0;
            this.cbAutoSend.Location = new System.Drawing.Point(13, 30);
            this.cbAutoSend.MouseBack = ((System.Drawing.Image)(resources.GetObject("cbAutoSend.MouseBack")));
            this.cbAutoSend.Name = "cbAutoSend";
            this.cbAutoSend.NormlBack = ((System.Drawing.Image)(resources.GetObject("cbAutoSend.NormlBack")));
            this.cbAutoSend.SelectedDownBack = ((System.Drawing.Image)(resources.GetObject("cbAutoSend.SelectedDownBack")));
            this.cbAutoSend.SelectedMouseBack = ((System.Drawing.Image)(resources.GetObject("cbAutoSend.SelectedMouseBack")));
            this.cbAutoSend.SelectedNormlBack = ((System.Drawing.Image)(resources.GetObject("cbAutoSend.SelectedNormlBack")));
            this.cbAutoSend.Size = new System.Drawing.Size(75, 21);
            this.cbAutoSend.TabIndex = 10;
            this.cbAutoSend.Text = "自动应答";
            this.cbAutoSend.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Crimson;
            this.label6.Location = new System.Drawing.Point(263, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(288, 14);
            this.label6.TabIndex = 17;
            this.label6.Text = "应答数据包(默认是原文回复，如要定制回复请填写：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(180, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 14);
            this.label4.TabIndex = 16;
            this.label4.Text = "秒钟发送一次";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(100, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 14);
            this.label3.TabIndex = 14;
            this.label3.Text = "每隔";
            // 
            // txtInterval
            // 
            this.txtInterval.BackColor = System.Drawing.Color.Transparent;
            this.txtInterval.BorderColor = System.Drawing.Color.LightGray;
            this.txtInterval.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtInterval.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txtInterval.HeightLightBolorColor = System.Drawing.Color.LightGray;
            this.txtInterval.Image = null;
            this.txtInterval.ImageSize = new System.Drawing.Size(0, 0);
            this.txtInterval.InputType = YUN.Framework.Commons.YunTextBox.NumTextBoxType.String;
            this.txtInterval.Lines = new string[] {
        "5"};
            this.txtInterval.Location = new System.Drawing.Point(129, 28);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.NormalBackrColor = System.Drawing.Color.White;
            this.txtInterval.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtInterval.PasswordChar = '\0';
            this.txtInterval.ReadOnlyEnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.txtInterval.Required = false;
            this.txtInterval.Size = new System.Drawing.Size(50, 25);
            this.txtInterval.TabIndex = 18;
            this.txtInterval.Text = "5";
            this.txtInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // skinGroupBox3
            // 
            this.skinGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.skinGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.skinGroupBox3.BorderColor = System.Drawing.Color.LightGray;
            this.skinGroupBox3.Controls.Add(this.rtbData);
            this.skinGroupBox3.Controls.Add(this.cbAutoSend);
            this.skinGroupBox3.Controls.Add(this.txtInterval);
            this.skinGroupBox3.Controls.Add(this.label3);
            this.skinGroupBox3.Controls.Add(this.label6);
            this.skinGroupBox3.Controls.Add(this.label4);
            this.skinGroupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.skinGroupBox3.Location = new System.Drawing.Point(77, 128);
            this.skinGroupBox3.Name = "skinGroupBox3";
            this.skinGroupBox3.RectBackColor = System.Drawing.Color.White;
            this.skinGroupBox3.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinGroupBox3.Size = new System.Drawing.Size(724, 106);
            this.skinGroupBox3.TabIndex = 19;
            this.skinGroupBox3.TabStop = false;
            this.skinGroupBox3.Text = "应答设置";
            this.skinGroupBox3.TitleBorderColor = System.Drawing.Color.Transparent;
            this.skinGroupBox3.TitleRectBackColor = System.Drawing.Color.Transparent;
            this.skinGroupBox3.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // rtbData
            // 
            this.rtbData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbData.BackColor = System.Drawing.Color.Transparent;
            this.rtbData.BorderColor = System.Drawing.Color.LightGray;
            this.rtbData.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rtbData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.rtbData.HeightLightBolorColor = System.Drawing.Color.LightGray;
            this.rtbData.Image = null;
            this.rtbData.ImageSize = new System.Drawing.Size(0, 0);
            this.rtbData.InputType = YUN.Framework.Commons.YunTextBox.NumTextBoxType.String;
            this.rtbData.Lines = new string[] {
        "5"};
            this.rtbData.Location = new System.Drawing.Point(13, 60);
            this.rtbData.Name = "rtbData";
            this.rtbData.NormalBackrColor = System.Drawing.Color.White;
            this.rtbData.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rtbData.PasswordChar = '\0';
            this.rtbData.ReadOnlyEnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.rtbData.Required = false;
            this.rtbData.Size = new System.Drawing.Size(705, 31);
            this.rtbData.TabIndex = 19;
            this.rtbData.Text = "5";
            // 
            // skinTabControl1
            // 
            this.skinTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinTabControl1.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.skinTabControl1.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.skinTabControl1.Controls.Add(this.skinTabPage3);
            this.skinTabControl1.Controls.Add(this.skinTabPage4);
            this.skinTabControl1.HeadBack = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.HeadBack")));
            this.skinTabControl1.HeadPalace = true;
            this.skinTabControl1.ImgSize = new System.Drawing.Size(16, 16);
            this.skinTabControl1.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.skinTabControl1.ItemSize = new System.Drawing.Size(100, 30);
            this.skinTabControl1.Location = new System.Drawing.Point(0, 253);
            this.skinTabControl1.Name = "skinTabControl1";
            this.skinTabControl1.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowDown")));
            this.skinTabControl1.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowHover")));
            this.skinTabControl1.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseHover")));
            this.skinTabControl1.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseNormal")));
            this.skinTabControl1.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageDown")));
            this.skinTabControl1.PageDownTxtColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.skinTabControl1.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageHover")));
            this.skinTabControl1.PageHoverTxtColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.skinTabControl1.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.skinTabControl1.PageNorml = null;
            this.skinTabControl1.PageNormlTxtColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.skinTabControl1.Radius = 4;
            this.skinTabControl1.SelectedIndex = 0;
            this.skinTabControl1.Size = new System.Drawing.Size(1564, 537);
            this.skinTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.skinTabControl1.TabIndex = 1;
            // 
            // skinTabPage3
            // 
            this.skinTabPage3.BackColor = System.Drawing.Color.White;
            this.skinTabPage3.Controls.Add(this.skinPanel1);
            this.skinTabPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPage3.Location = new System.Drawing.Point(0, 30);
            this.skinTabPage3.Name = "skinTabPage3";
            this.skinTabPage3.Size = new System.Drawing.Size(1564, 507);
            this.skinTabPage3.TabIndex = 2;
            this.skinTabPage3.TabItemImage = null;
            this.skinTabPage3.Text = "接收数据";
            // 
            // skinTabPage4
            // 
            this.skinTabPage4.BackColor = System.Drawing.Color.White;
            this.skinTabPage4.Controls.Add(this.skinPanel2);
            this.skinTabPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPage4.Location = new System.Drawing.Point(0, 30);
            this.skinTabPage4.Name = "skinTabPage4";
            this.skinTabPage4.Size = new System.Drawing.Size(1564, 507);
            this.skinTabPage4.TabIndex = 3;
            this.skinTabPage4.TabItemImage = null;
            this.skinTabPage4.Text = "当前客户端连接";
            // 
            // skinPanel1
            // 
            this.skinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel1.BorderColor = System.Drawing.Color.LightGray;
            this.skinPanel1.Controls.Add(this.PacketView);
            this.skinPanel1.Controls.Add(this.btnOpenLog);
            this.skinPanel1.Controls.Add(this.cbLog);
            this.skinPanel1.Controls.Add(this.btnClearLog);
            this.skinPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(0, 0);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(1564, 507);
            this.skinPanel1.TabIndex = 0;
            // 
            // skinPanel2
            // 
            this.skinPanel2.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel2.BorderColor = System.Drawing.Color.LightGray;
            this.skinPanel2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinPanel2.DownBack = null;
            this.skinPanel2.Location = new System.Drawing.Point(0, 0);
            this.skinPanel2.MouseBack = null;
            this.skinPanel2.Name = "skinPanel2";
            this.skinPanel2.NormlBack = null;
            this.skinPanel2.Size = new System.Drawing.Size(1564, 507);
            this.skinPanel2.TabIndex = 1;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.BackColor = System.Drawing.Color.Transparent;
            this.btnClearLog.BaseColor = System.Drawing.Color.Gainsboro;
            this.btnClearLog.BorderColor = System.Drawing.Color.LightGray;
            this.btnClearLog.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnClearLog.DownBack = null;
            this.btnClearLog.DownBaseColor = System.Drawing.Color.Gainsboro;
            this.btnClearLog.Image = ((System.Drawing.Image)(resources.GetObject("btnClearLog.Image")));
            this.btnClearLog.ImageSize = new System.Drawing.Size(16, 16);
            this.btnClearLog.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnClearLog.Location = new System.Drawing.Point(1180, 5);
            this.btnClearLog.MouseBack = null;
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.NormlBack = null;
            this.btnClearLog.Radius = 4;
            this.btnClearLog.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.btnClearLog.Size = new System.Drawing.Size(85, 25);
            this.btnClearLog.TabIndex = 6;
            this.btnClearLog.Text = " 清空日志";
            this.btnClearLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClearLog.UseVisualStyleBackColor = false;
            // 
            // cbLog
            // 
            this.cbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLog.AutoSize = true;
            this.cbLog.BackColor = System.Drawing.Color.Transparent;
            this.cbLog.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cbLog.Checked = true;
            this.cbLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLog.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.cbLog.DefaultCheckButtonWidth = 16;
            this.cbLog.DownBack = ((System.Drawing.Image)(resources.GetObject("cbLog.DownBack")));
            this.cbLog.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLog.LightEffect = false;
            this.cbLog.LightEffectBack = System.Drawing.Color.Black;
            this.cbLog.LightEffectWidth = 0;
            this.cbLog.Location = new System.Drawing.Point(1272, 9);
            this.cbLog.MouseBack = ((System.Drawing.Image)(resources.GetObject("cbLog.MouseBack")));
            this.cbLog.Name = "cbLog";
            this.cbLog.NormlBack = ((System.Drawing.Image)(resources.GetObject("cbLog.NormlBack")));
            this.cbLog.SelectedDownBack = ((System.Drawing.Image)(resources.GetObject("cbLog.SelectedDownBack")));
            this.cbLog.SelectedMouseBack = ((System.Drawing.Image)(resources.GetObject("cbLog.SelectedMouseBack")));
            this.cbLog.SelectedNormlBack = ((System.Drawing.Image)(resources.GetObject("cbLog.SelectedNormlBack")));
            this.cbLog.Size = new System.Drawing.Size(159, 21);
            this.cbLog.TabIndex = 11;
            this.cbLog.Text = "保存数据到日志文件当中";
            this.cbLog.UseVisualStyleBackColor = false;
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenLog.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenLog.BaseColor = System.Drawing.Color.Gainsboro;
            this.btnOpenLog.BorderColor = System.Drawing.Color.LightGray;
            this.btnOpenLog.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnOpenLog.DownBack = null;
            this.btnOpenLog.DownBaseColor = System.Drawing.Color.Gainsboro;
            this.btnOpenLog.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenLog.Image")));
            this.btnOpenLog.ImageSize = new System.Drawing.Size(16, 16);
            this.btnOpenLog.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnOpenLog.Location = new System.Drawing.Point(1437, 6);
            this.btnOpenLog.MouseBack = null;
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.NormlBack = null;
            this.btnOpenLog.Radius = 4;
            this.btnOpenLog.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.btnOpenLog.Size = new System.Drawing.Size(115, 25);
            this.btnOpenLog.TabIndex = 12;
            this.btnOpenLog.Text = " 打开日志目录";
            this.btnOpenLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOpenLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpenLog.UseVisualStyleBackColor = false;
            // 
            // PacketView
            // 
            this.PacketView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PacketView.BorderColor = System.Drawing.Color.LightGray;
            this.PacketView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.ColumnHeader,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.PacketView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.PacketView.Location = new System.Drawing.Point(3, 36);
            this.PacketView.Name = "PacketView";
            this.PacketView.OwnerDraw = true;
            this.PacketView.Size = new System.Drawing.Size(1558, 468);
            this.PacketView.TabIndex = 13;
            this.PacketView.UseCompatibleStateImageBehavior = false;
            this.PacketView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            // 
            // ColumnHeader
            // 
            this.ColumnHeader.Text = "连接ID";
            this.ColumnHeader.Width = 220;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IP地址";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "时间";
            this.columnHeader3.Width = 180;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "数据报文";
            this.columnHeader4.Width = 528;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "字节数";
            this.columnHeader5.Width = 90;
            // 
            // titlePanel2
            // 
            this.titlePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titlePanel2.BorderColor = System.Drawing.Color.LightGray;
            this.titlePanel2.Controls.Add(this.txtMsg);
            this.titlePanel2.Location = new System.Drawing.Point(807, 39);
            this.titlePanel2.Name = "titlePanel2";
            this.titlePanel2.Padding = new System.Windows.Forms.Padding(0, 28, 0, 0);
            this.titlePanel2.Size = new System.Drawing.Size(745, 195);
            this.titlePanel2.TabIndex = 20;
            this.titlePanel2.Title = "消息提示区";
            this.titlePanel2.TitleAlign = YUN.Framework.Commons.TitleAlignment.Left;
            this.titlePanel2.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.titlePanel2.TitleBackImage = null;
            this.titlePanel2.TitleBorderColor = System.Drawing.Color.LightGray;
            this.titlePanel2.TitleFont = new System.Drawing.Font("Tahoma", 9F);
            this.titlePanel2.TitleHeight = 28;
            this.titlePanel2.TitleImage = null;
            this.titlePanel2.TitleXOffset = 8;
            // 
            // txtMsg
            // 
            this.txtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsg.Location = new System.Drawing.Point(3, 31);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.Size = new System.Drawing.Size(739, 161);
            this.txtMsg.TabIndex = 0;
            // 
            // rbUdp
            // 
            this.rbUdp.AutoSize = true;
            this.rbUdp.BackColor = System.Drawing.Color.Transparent;
            this.rbUdp.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.rbUdp.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.rbUdp.DefaultRadioButtonWidth = 16;
            this.rbUdp.DownBack = ((System.Drawing.Image)(resources.GetObject("rbUdp.DownBack")));
            this.rbUdp.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbUdp.LightEffect = false;
            this.rbUdp.LightEffectWidth = 0;
            this.rbUdp.Location = new System.Drawing.Point(103, 21);
            this.rbUdp.MouseBack = ((System.Drawing.Image)(resources.GetObject("rbUdp.MouseBack")));
            this.rbUdp.Name = "rbUdp";
            this.rbUdp.NormlBack = ((System.Drawing.Image)(resources.GetObject("rbUdp.NormlBack")));
            this.rbUdp.SelectedDownBack = ((System.Drawing.Image)(resources.GetObject("rbUdp.SelectedDownBack")));
            this.rbUdp.SelectedMouseBack = ((System.Drawing.Image)(resources.GetObject("rbUdp.SelectedMouseBack")));
            this.rbUdp.SelectedNormlBack = ((System.Drawing.Image)(resources.GetObject("rbUdp.SelectedNormlBack")));
            this.rbUdp.Size = new System.Drawing.Size(51, 21);
            this.rbUdp.TabIndex = 7;
            this.rbUdp.Text = "UDP";
            this.rbUdp.UseVisualStyleBackColor = false;
            // 
            // rbTcp
            // 
            this.rbTcp.AutoSize = true;
            this.rbTcp.BackColor = System.Drawing.Color.Transparent;
            this.rbTcp.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.rbTcp.Checked = true;
            this.rbTcp.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.rbTcp.DefaultRadioButtonWidth = 16;
            this.rbTcp.DownBack = ((System.Drawing.Image)(resources.GetObject("rbTcp.DownBack")));
            this.rbTcp.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbTcp.LightEffect = false;
            this.rbTcp.LightEffectWidth = 0;
            this.rbTcp.Location = new System.Drawing.Point(49, 21);
            this.rbTcp.MouseBack = ((System.Drawing.Image)(resources.GetObject("rbTcp.MouseBack")));
            this.rbTcp.Name = "rbTcp";
            this.rbTcp.NormlBack = ((System.Drawing.Image)(resources.GetObject("rbTcp.NormlBack")));
            this.rbTcp.SelectedDownBack = ((System.Drawing.Image)(resources.GetObject("rbTcp.SelectedDownBack")));
            this.rbTcp.SelectedMouseBack = ((System.Drawing.Image)(resources.GetObject("rbTcp.SelectedMouseBack")));
            this.rbTcp.SelectedNormlBack = ((System.Drawing.Image)(resources.GetObject("rbTcp.SelectedNormlBack")));
            this.rbTcp.Size = new System.Drawing.Size(48, 21);
            this.rbTcp.TabIndex = 6;
            this.rbTcp.TabStop = true;
            this.rbTcp.Text = "TCP";
            this.rbTcp.UseVisualStyleBackColor = false;
            // 
            // skinGroupBox2
            // 
            this.skinGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.skinGroupBox2.BorderColor = System.Drawing.Color.LightGray;
            this.skinGroupBox2.Controls.Add(this.rbTcp);
            this.skinGroupBox2.Controls.Add(this.rbUdp);
            this.skinGroupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.skinGroupBox2.Location = new System.Drawing.Point(77, 70);
            this.skinGroupBox2.Name = "skinGroupBox2";
            this.skinGroupBox2.RectBackColor = System.Drawing.Color.White;
            this.skinGroupBox2.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinGroupBox2.Size = new System.Drawing.Size(479, 52);
            this.skinGroupBox2.TabIndex = 9;
            this.skinGroupBox2.TabStop = false;
            this.skinGroupBox2.Text = "通讯协议";
            this.skinGroupBox2.TitleBorderColor = System.Drawing.Color.Transparent;
            this.skinGroupBox2.TitleRectBackColor = System.Drawing.Color.Transparent;
            this.skinGroupBox2.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1564, 790);
            this.Controls.Add(this.skinTabControl1);
            this.Controls.Add(this.titlePanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ServerForm";
            this.Text = "ServerForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.titlePanel1.ResumeLayout(false);
            this.titlePanel1.PerformLayout();
            this.skinGroupBox3.ResumeLayout(false);
            this.skinGroupBox3.PerformLayout();
            this.skinTabControl1.ResumeLayout(false);
            this.skinTabPage3.ResumeLayout(false);
            this.skinTabPage4.ResumeLayout(false);
            this.skinPanel1.ResumeLayout(false);
            this.skinPanel1.PerformLayout();
            this.titlePanel2.ResumeLayout(false);
            this.titlePanel2.PerformLayout();
            this.skinGroupBox2.ResumeLayout(false);
            this.skinGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private YUN.Framework.Commons.TitlePanel titlePanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private YUN.Framework.Commons.YunTextBox txtServerPort;
        private YUN.Framework.Commons.YunTextBox txtServerIP;
        private CCWin.SkinControl.SkinButton btnListen;
        private CCWin.SkinControl.SkinButton btnStopListen;
        private CCWin.SkinControl.SkinCheckBox cbAutoSend;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private CCWin.SkinControl.SkinGroupBox skinGroupBox3;
        private YUN.Framework.Commons.YunTextBox txtInterval;
        private YUN.Framework.Commons.YunTextBox rtbData;
        private CCWin.SkinControl.SkinTabControl skinTabControl1;
        private CCWin.SkinControl.SkinTabPage skinTabPage3;
        private CCWin.SkinControl.SkinPanel skinPanel1;
        private CCWin.SkinControl.SkinCheckBox cbLog;
        private CCWin.SkinControl.SkinButton btnClearLog;
        private CCWin.SkinControl.SkinTabPage skinTabPage4;
        private CCWin.SkinControl.SkinPanel skinPanel2;
        private CCWin.SkinControl.SkinButton btnOpenLog;
        private CCWin.SkinControl.SkinListView PacketView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader ColumnHeader;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private YUN.Framework.Commons.TitlePanel titlePanel2;
        private System.Windows.Forms.TextBox txtMsg;
        private CCWin.SkinControl.SkinGroupBox skinGroupBox2;
        private CCWin.SkinControl.SkinRadioButton rbTcp;
        private CCWin.SkinControl.SkinRadioButton rbUdp;
    }
}