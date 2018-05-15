using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MES.SocketService
{
    partial class MainService : ServiceBase
    {
        /// <summary>
        /// 通过BootStrap启动Socket服务
        /// </summary>
        private IBootstrap m_Bootstrap;

        public MainService()
        {
            InitializeComponent();
            m_Bootstrap = BootstrapFactory.CreateBootstrap();
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            if (!m_Bootstrap.Initialize())
            {
                return;
            }
            m_Bootstrap.Start();
        }

        protected override void OnStop()
        {
            // TODO:  在此处添加代码以执行停止服务所需的关闭操作。
            m_Bootstrap.Stop();
            base.OnStop();
        }

        /// <summary>
        /// 系统即将关闭
        /// </summary>
        protected override void OnShutdown()
        {
            m_Bootstrap.Stop();
            base.OnShutdown();
        }
    }
}
