using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using MES.SocketService.Entity;
using MES.SocketService.IDAL; 
using YUN.Framework.ControlUtil;

namespace MES.SocketService.BLL
{
    /// <summary>
    /// META_Parameter
    /// </summary>
	public class META_Parameter : BaseBLL<META_ParameterInfo>
    {
        public META_Parameter() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }
    }
}
