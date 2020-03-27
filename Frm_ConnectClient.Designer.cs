namespace MES.SocketService
{
    partial class Frm_ConnectClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ConnectClient));
            this.titlePanel2 = new YUN.Framework.Commons.TitlePanel();
            this.wgvList = new YUN.Pager.WinControl.WinGridView();
            this.btnRefresh = new CCWin.SkinControl.SkinButton();
            this.titlePanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlePanel2
            // 
            this.titlePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titlePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.titlePanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.titlePanel2.Controls.Add(this.wgvList);
            this.titlePanel2.Controls.Add(this.btnRefresh);
            this.titlePanel2.ForeColor = System.Drawing.Color.Gray;
            this.titlePanel2.Location = new System.Drawing.Point(7, 37);
            this.titlePanel2.Name = "titlePanel2";
            this.titlePanel2.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.titlePanel2.Size = new System.Drawing.Size(1284, 655);
            this.titlePanel2.TabIndex = 208;
            this.titlePanel2.Title = "";
            this.titlePanel2.TitleAlign = YUN.Framework.Commons.TitleAlignment.Left;
            this.titlePanel2.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.titlePanel2.TitleBackImage = null;
            this.titlePanel2.TitleBorderColor = System.Drawing.Color.DarkGray;
            this.titlePanel2.TitleFont = new System.Drawing.Font("Tahoma", 9F);
            this.titlePanel2.TitleHeight = 32;
            this.titlePanel2.TitleImage = null;
            this.titlePanel2.TitleXOffset = 8;
            // 
            // wgvList
            // 
            this.wgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wgvList.AppendedMenu = null;
            this.wgvList.AutoGenerateColumns = false;
            this.wgvList.ColumnNameAlias = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("wgvList.ColumnNameAlias")));
            this.wgvList.DataSource = null;
            this.wgvList.DisplayColumns = "";
            this.wgvList.FixedColumns = null;
            this.wgvList.Location = new System.Drawing.Point(3, 31);
            this.wgvList.Name = "wgvList";
            this.wgvList.PrintTitle = "";
            this.wgvList.ShowAddMenu = true;
            this.wgvList.ShowCheckBox = true;
            this.wgvList.ShowDeleteMenu = true;
            this.wgvList.ShowEditMenu = true;
            this.wgvList.ShowExportButton = true;
            this.wgvList.Size = new System.Drawing.Size(1269, 612);
            this.wgvList.TabIndex = 156;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.btnRefresh.BaseColor = System.Drawing.Color.WhiteSmoke;
            this.btnRefresh.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnRefresh.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.DownBack = null;
            this.btnRefresh.DownBaseColor = System.Drawing.Color.Gainsboro;
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnRefresh.ForeColorSuit = true;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageSize = new System.Drawing.Size(16, 16);
            this.btnRefresh.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnRefresh.IsDrawGlass = false;
            this.btnRefresh.IsEnabledDraw = false;
            this.btnRefresh.Location = new System.Drawing.Point(1122, 4);
            this.btnRefresh.MouseBack = null;
            this.btnRefresh.MouseBaseColor = System.Drawing.Color.Gainsboro;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.NormlBack = null;
            this.btnRefresh.Radius = 4;
            this.btnRefresh.Size = new System.Drawing.Size(159, 25);
            this.btnRefresh.TabIndex = 183;
            this.btnRefresh.Text = " 刷新数据";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Frm_ConnectClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 697);
            this.Controls.Add(this.titlePanel2);
            this.Name = "Frm_ConnectClient";
            this.Text = "明细";
            this.Load += new System.EventHandler(this.Frm_ConnectClient_Load);
            this.titlePanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private YUN.Framework.Commons.TitlePanel titlePanel2;
        private YUN.Pager.WinControl.WinGridView wgvList;
        private CCWin.SkinControl.SkinButton btnRefresh;
    }
}