namespace MES.SocketService
{
    partial class ConfigSetting
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigSetting));
            this.btnRefresh_ = new CCWin.SkinControl.SkinButton();
            this.btnSave_ = new CCWin.SkinControl.SkinButton();
            this.txt_ = new YUN.Framework.Commons.YunTextBox();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRefresh_
            // 
            this.btnRefresh_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh_.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.btnRefresh_.BaseColor = System.Drawing.Color.WhiteSmoke;
            this.btnRefresh_.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnRefresh_.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnRefresh_.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh_.DownBack = null;
            this.btnRefresh_.DownBaseColor = System.Drawing.Color.Gainsboro;
            this.btnRefresh_.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnRefresh_.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnRefresh_.ForeColorSuit = true;
            this.btnRefresh_.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh_.Image")));
            this.btnRefresh_.ImageSize = new System.Drawing.Size(16, 16);
            this.btnRefresh_.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnRefresh_.IsDrawGlass = false;
            this.btnRefresh_.IsEnabledDraw = false;
            this.btnRefresh_.Location = new System.Drawing.Point(968, 2);
            this.btnRefresh_.MouseBack = null;
            this.btnRefresh_.MouseBaseColor = System.Drawing.Color.Gainsboro;
            this.btnRefresh_.Name = "btnRefresh_";
            this.btnRefresh_.NormlBack = null;
            this.btnRefresh_.Radius = 4;
            this.btnRefresh_.Size = new System.Drawing.Size(80, 25);
            this.btnRefresh_.TabIndex = 195;
            this.btnRefresh_.Text = " 刷新";
            this.btnRefresh_.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh_.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh_.UseVisualStyleBackColor = false;
            this.btnRefresh_.Click += new System.EventHandler(this.btnRefresh__Click);
            // 
            // btnSave_
            // 
            this.btnSave_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave_.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.btnSave_.BaseColor = System.Drawing.Color.WhiteSmoke;
            this.btnSave_.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnSave_.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSave_.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave_.DownBack = null;
            this.btnSave_.DownBaseColor = System.Drawing.Color.Gainsboro;
            this.btnSave_.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnSave_.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnSave_.ForeColorSuit = true;
            this.btnSave_.Image = global::MES.SocketService.Properties.Resources.btnSet;
            this.btnSave_.ImageSize = new System.Drawing.Size(16, 16);
            this.btnSave_.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnSave_.IsDrawGlass = false;
            this.btnSave_.IsEnabledDraw = false;
            this.btnSave_.Location = new System.Drawing.Point(1053, 2);
            this.btnSave_.MouseBack = null;
            this.btnSave_.MouseBaseColor = System.Drawing.Color.Gainsboro;
            this.btnSave_.Name = "btnSave_";
            this.btnSave_.NormlBack = null;
            this.btnSave_.Radius = 4;
            this.btnSave_.Size = new System.Drawing.Size(80, 25);
            this.btnSave_.TabIndex = 194;
            this.btnSave_.Text = " 设置";
            this.btnSave_.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave_.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave_.UseVisualStyleBackColor = false;
            this.btnSave_.Click += new System.EventHandler(this.btnSave__Click);
            // 
            // txt_
            // 
            this.txt_.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_.BackColor = System.Drawing.Color.Transparent;
            this.txt_.BorderColor = System.Drawing.Color.LightGray;
            this.txt_.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txt_.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txt_.HeightLightBolorColor = System.Drawing.Color.LightGray;
            this.txt_.Image = null;
            this.txt_.ImageSize = new System.Drawing.Size(0, 0);
            this.txt_.InputType = YUN.Framework.Commons.YunTextBox.NumTextBoxType.String;
            this.txt_.Lines = new string[0];
            this.txt_.Location = new System.Drawing.Point(135, 1);
            this.txt_.Name = "txt_";
            this.txt_.NormalBackrColor = System.Drawing.Color.White;
            this.txt_.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_.PasswordChar = '\0';
            this.txt_.ReadOnlyEnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(250)))), ((int)(((byte)(243)))));
            this.txt_.Required = false;
            this.txt_.Size = new System.Drawing.Size(828, 27);
            this.txt_.TabIndex = 193;
            // 
            // label
            // 
            this.label.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label.Location = new System.Drawing.Point(6, 5);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(125, 20);
            this.label.TabIndex = 192;
            this.label.Text = "KeyName";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfigSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnRefresh_);
            this.Controls.Add(this.btnSave_);
            this.Controls.Add(this.txt_);
            this.Controls.Add(this.label);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Name = "ConfigSetting";
            this.Size = new System.Drawing.Size(1153, 29);
            this.Load += new System.EventHandler(this.ConfigSetting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinButton btnRefresh_;
        private CCWin.SkinControl.SkinButton btnSave_;
        private YUN.Framework.Commons.YunTextBox txt_;
        private System.Windows.Forms.Label label;
    }
}
