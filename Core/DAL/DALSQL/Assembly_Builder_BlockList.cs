using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using YUN.Pager.Entity;
using YUN.Framework.Commons;
using YUN.Framework.ControlUtil;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MES.SocketService.Entity;
using MES.SocketService.IDAL;

namespace MES.SocketService.DALSQL
{
    /// <summary>
    /// Assembly_Builder_BlockList
    /// </summary>
	public class Assembly_Builder_BlockList : BaseDALSQL<Assembly_Builder_BlockListInfo>, IAssembly_Builder_BlockList
	{
		#region 对象实例及构造函数

		public static Assembly_Builder_BlockList Instance
		{
			get
			{
				return new Assembly_Builder_BlockList();
			}
		}
		public Assembly_Builder_BlockList() : base("TB_Assembly_Builder_BlockList","ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override Assembly_Builder_BlockListInfo DataReaderToEntity(IDataReader dataReader)
		{
			Assembly_Builder_BlockListInfo info = new Assembly_Builder_BlockListInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetGuid("ID");
			info.WO = reader.GetString("WO");
			info.BlockID = reader.GetString("BlockID");
			info.SN = reader.GetString("SN");
			info.IsUsed = reader.GetBoolean("IsUsed");
			info.Seq = reader.GetInt32("Seq");
			info.ActionDatetime = reader.GetDateTime("ActionDatetime");
			info.PRTDatetime = reader.GetDateTime("PRTDatetime");
			info.ImpSafeCode = reader.GetString("ImpSafeCode");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(Assembly_Builder_BlockListInfo obj)
		{
		    Assembly_Builder_BlockListInfo info = obj as Assembly_Builder_BlockListInfo;
			Hashtable hash = new Hashtable(); 
			
			hash.Add("ID", info.ID);
 			hash.Add("WO", info.WO);
 			hash.Add("BlockID", info.BlockID);
 			hash.Add("SN", info.SN);
 			hash.Add("IsUsed", info.IsUsed);
 			hash.Add("Seq", info.Seq);
 			hash.Add("ActionDatetime", info.ActionDatetime);
 			hash.Add("PRTDatetime", info.PRTDatetime);
 			hash.Add("ImpSafeCode", info.ImpSafeCode);
 				
			return hash;
		}

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetColumnNameAlias()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            #region 添加别名解析
            //dict.Add("ID", "编号");
            dict.Add("ID", "");
             dict.Add("WO", "");
             dict.Add("BlockID", "");
             dict.Add("SN", "");
             dict.Add("IsUsed", "");
             dict.Add("Seq", "");
             dict.Add("ActionDatetime", "");
             dict.Add("PRTDatetime", "");
             dict.Add("ImpSafeCode", "");
             #endregion

            return dict;
        }

    }
}