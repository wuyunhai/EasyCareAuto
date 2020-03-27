using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YUN.Framework.BaseUI;

namespace MES.SocketService
{
    public partial class Frm_ConnectClient : BaseForm
    {
        private string Port;
        public Frm_ConnectClient(string port)
        {
            InitializeComponent();

            Port = port;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void Frm_ConnectClient_Load(object sender, EventArgs e)
        {
            #region wgvList

            Dictionary<string, string> columnNameAlias = new Dictionary<string, string>();
            columnNameAlias.Add("IsShowLog", "是否显示日志");
            columnNameAlias.Add("LocalDeviceName", "服务端地址");
            columnNameAlias.Add("RemoteDeviceName", "客户端地址");
            columnNameAlias.Add("StartTime", "连接时间");
            columnNameAlias.Add("LastActiveTime", "最近通讯时间");

            wgvList.ShowLineNumber = false;
            wgvList.ShowCheckBox = true;
            wgvList.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置   
            wgvList.dataGridView1.DataSourceChanged += new EventHandler(wgvList_DataSourceChanged); 
            wgvList.OnCheckBoxSelectionChanged += WgvList_OnCheckBoxSelectionChanged; 
            wgvList.DisplayColumns = "LocalDeviceName,RemoteDeviceName,StartTime,LastActiveTime";
            wgvList.ColumnNameAlias = columnNameAlias;

            #endregion

            BindData();
        }

        private void WgvList_OnCheckBoxSelectionChanged(object sender, EventArgs e)
        {
            List<int> list = wgvList.GetCheckedRows();

            // 业务处理

        }

        private void BindData()
        {
            List<MesSession> lst = GlobalData.ClientSessionList.FindAll(li => li.LocalEndPoint.Port.ToString() == Port);
            lst = (from li in lst orderby li.RemoteDeviceName ascending select li).ToList();
            wgvList.DataSource = lst;
        }

        private void wgvList_DataSourceChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.Columns.Count > 0 && dgv.RowCount > 0)
            {
                foreach (DataGridViewColumn column in dgv.Columns)
                {

                    if (column.Index == 0)
                    {
                        column.DataPropertyName = "IsShowLog";
                        column.HeaderText = "是否显示日志";
                        column.Width = 120;
                    }
                    else
                    {
                        column.Width = 200;
                    }
                }
            }
        }
    }
}
