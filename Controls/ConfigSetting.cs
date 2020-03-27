using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YUN.Framework.Commons;
using System.Configuration;
using MES.SocketService.Entity;
using YUN.Framework.ControlUtil;
using MES.SocketService.BLL;

namespace MES.SocketService
{
    public partial class ConfigSetting : UserControl
    {
        public ConfigSetting()
        {
            InitializeComponent();
        }

        private void ConfigSetting_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            Control txtTemp = this.Controls.Find("txt_" + KeyName, true)[0];
            try
            { 
                YunTextBox yTextbox = txtTemp as YunTextBox;  
                META_ParameterInfo info = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='{KeyName}'");
                yTextbox.Text = info.Value;
                yTextbox.ReadOnly = true;

            }
            catch (Exception ex)
            {
                txtTemp.Text = $"{ DateTime.Now.ToString("yyyy.MM.dd HH: mm:ss.fff")} >> 刷新异常：{ex.Message}.";
            }
        }

        private string _labelText;
        [Category("Config")]
        [Browsable(true)]
        [Description("标签名称")]
        public string LabelText
        {
            get { return _labelText; }
            set
            {
                _labelText = value;
                label.Text = _labelText;
                this.Invalidate();
            }
        }

        private string _keyName;
        [Category("Config")]
        [Browsable(true)]
        [Description("对应配置文件中的Key")]
        public string KeyName
        {
            get { return _keyName; }
            set
            {
                _keyName = value;
                txt_.Name += _keyName;
                btnRefresh_.Name += _keyName;
                btnSave_.Name += _keyName;
                this.Invalidate();
            }
        }

        private void btnRefresh__Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string comm = StringUtil.RemovePrefix(btn.Name, "btnRefresh_");
            Control txtTemp = this.Controls.Find("txt_" + comm, true)[0];

            META_ParameterInfo info = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='{comm}'");
            txtTemp.Text = info.Value;
        }

        private void btnSave__Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Control txtTemp = this.Controls.Find("txt_" + _keyName, true)[0];
            YunTextBox yTextbox = txtTemp as YunTextBox;
            switch (btn.Text.Trim())
            {
                case "设置":
                    btn.Image = Properties.Resources.btnSave;
                    btn.Text = " 保存";
                    yTextbox.ReadOnly = false;
                    break;
                case "保存":
                    btn.Image = Properties.Resources.btnSet;
                    btn.Text = " 设置";
                    yTextbox.ReadOnly = true;

                    //测试
                    META_ParameterInfo info = BLLFactory<META_Parameter>.Instance.FindSingle($"Key='{_keyName}'");
                    info.Value = yTextbox.Text.Trim();
                    BLLFactory<META_Parameter>.Instance.Update(info, info.ID);

                    break;
            }
        }
    }
}
