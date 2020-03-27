using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
 
using YUN.Framework.Commons;
using YUN.Framework.ControlUtil;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MES.SocketService.Entity;
using MES.SocketService.IDAL;

namespace MES.SocketService.DALSQL
{
    /// <summary>
    /// META_Parameter
    /// </summary>
	public class META_Parameter : BaseDALSQLite<META_ParameterInfo>, IMETA_Parameter
	{
		#region 对象实例及构造函数

		public static META_Parameter Instance
		{
			get
			{
				return new META_Parameter();
			}
		}
		public META_Parameter() : base("T_META_Parameter","ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override META_ParameterInfo DataReaderToEntity(IDataReader dataReader)
		{
			META_ParameterInfo info = new META_ParameterInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			 
            info.ID = reader.GetInt32("ID");
            info.Key = reader.GetString("Key"); 
			info.Value = reader.GetString("Value"); 
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(META_ParameterInfo obj)
		{
		    META_ParameterInfo info = obj as META_ParameterInfo;
			Hashtable hash = new Hashtable(); 
			
			hash.Add("ID", info.ID);
 			hash.Add("Key", info.Key); 
 			hash.Add("Value", info.Value); 
 				
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
             dict.Add("Key", ""); 
             dict.Add("Value", ""); 
             #endregion

            return dict;
        }

    }
}