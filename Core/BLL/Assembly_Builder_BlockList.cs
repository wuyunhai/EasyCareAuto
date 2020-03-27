using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using MES.SocketService.Entity;
using MES.SocketService.IDAL;
using YUN.Pager.Entity;
using YUN.Framework.ControlUtil;

namespace MES.SocketService.BLL
{
    /// <summary>
    /// Assembly_Builder_BlockList
    /// </summary>
	public class Assembly_Builder_BlockList : BaseBLL<Assembly_Builder_BlockListInfo>
    {
        public Assembly_Builder_BlockList() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

    }
}
