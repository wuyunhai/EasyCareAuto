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
            this.gbMsg = new CCWin.SkinControl.SkinGroupBox();
            this.txtMsg = new YUN.Framework.Commons.YunTextBox();
            this.btnStopListen = new CCWin.SkinControl.SkinButton();
            this.btnListen = new CCWin.SkinControl.SkinButton();
            this.gbMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMsg
            // 
            this.gbMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMsg.BackColor = System.Drawing.Color.Transparent;
            this.gbMsg.BorderColor = System.Drawing.Color.LightGray;
            this.gbMsg.Controls.Add(this.txtMsg);
            this.gbMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.gbMsg.Location = new System.Drawing.Point(24, 56);
            this.gbMsg.Name = "gbMsg";
            this.gbMsg.RectBackColor = System.Drawing.Color.White;
            this.gbMsg.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.gbMsg.Size = new System.Drawing.Size(778, 272);
            this.gbMsg.TabIndex = 10;
            this.gbMsg.TabStop = false;
            this.gbMsg.Text = "通讯协议";
            this.gbMsg.TitleBorderColor = System.Drawing.Color.Transparent;
            this.gbMsg.TitleRectBackColor = System.Drawing.Color.Transparent;
            this.gbMsg.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // txtMsg
            // 
            this.txtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsg.BackColor = System.Drawing.Color.Transparent;
            this.txtMsg.BorderColor = System.Drawing.Color.LightGray;
            this.txtMsg.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txtMsg.HeightLightBolorColor = System.Drawing.Color.LightGray;
            this.txtMsg.Image = null;
            this.txtMsg.ImageSize = new System.Drawing.Size(0, 0);
            this.txtMsg.InputType = YUN.Framework.Commons.YunTextBox.NumTextBoxType.String;
            this.txtMsg.Lines = new string[] {
        "127.0.0.1"};
            this.txtMsg.Location = new System.Drawing.Point(11, 26);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.NormalBackrColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.txtMsg.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtMsg.PasswordChar = '\0';
            this.txtMsg.ReadOnlyEnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.txtMsg.Required = false;
            this.txtMsg.Size = new System.Drawing.Size(757, 233);
            this.txtMsg.TabIndex = 3;
            this.txtMsg.Text = "127.0.0.1";
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
            this.btnStopListen.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnStopListen.IsEnabledDraw = false;
            this.btnStopListen.Location = new System.Drawing.Point(652, 345);
            this.btnStopListen.MouseBack = null;
            this.btnStopListen.Name = "btnStopListen";
            this.btnStopListen.NormlBack = null;
            this.btnStopListen.Radius = 4;
            this.btnStopListen.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.btnStopListen.Size = new System.Drawing.Size(150, 35);
            this.btnStopListen.TabIndex = 12;
            this.btnStopListen.Text = " 返回";
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
            this.btnListen.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnListen.DownBack = null;
            this.btnListen.DownBaseColor = System.Drawing.Color.Gainsboro;
            this.btnListen.Image = ((System.Drawing.Image)(resources.GetObject("btnListen.Image")));
            this.btnListen.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnListen.IsEnabledDraw = false;
            this.btnListen.Location = new System.Drawing.Point(496, 345);
            this.btnListen.MouseBack = null;
            this.btnListen.Name = "btnListen";
            this.btnListen.NormlBack = null;
            this.btnListen.Radius = 4;
            this.btnListen.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.btnListen.Size = new System.Drawing.Size(150, 35);
            this.btnListen.TabIndex = 11;
            this.btnListen.Text = " 发送";
            this.btnListen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnListen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnListen.UseVisualStyleBackColor = false;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // Frm_Msg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(835, 400);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.btnStopListen);
            this.Controls.Add(this.gbMsg);
            this.Name = "Frm_Msg";
            this.Text = "协议内容";
            this.gbMsg.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private CCWin.SkinControl.SkinButton btnStopListen;
        private CCWin.SkinControl.SkinButton btnListen;
        public CCWin.SkinControl.SkinGroupBox gbMsg;
        public YUN.Framework.Commons.YunTextBox txtMsg;
    }
}