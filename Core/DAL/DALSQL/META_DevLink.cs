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
    /// META_DevLink
    /// </summary>
	public class META_DevLink : BaseDALSQLite<META_DevLinkInfo>, IMETA_DevLink
	{
		#region 对象实例及构造函数

		public static META_DevLink Instance
		{
			get
			{
				return new META_DevLink();
			}
		}
		public META_DevLink() : base("T_META_DevLink","ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override META_DevLinkInfo DataReaderToEntity(IDataReader dataReader)
		{
			META_DevLinkInfo info = new META_DevLinkInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			 
            info.ID = reader.GetInt32("ID");
            info.DevCode = reader.GetString("DevCode"); 
			info.EquipmentID1 = reader.GetString("EquipmentID1");
			info.EquipmentID2 = reader.GetString("EquipmentID2");
			info.EquipmentID3 = reader.GetString("EquipmentID3");
			info.ProcessType = reader.GetString("ProcessType");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(META_DevLinkInfo obj)
		{
		    META_DevLinkInfo info = obj as META_DevLinkInfo;
			Hashtable hash = new Hashtable(); 
			
			hash.Add("ID", info.ID);
 			hash.Add("DevCode", info.DevCode); 
 			hash.Add("EquipmentID1", info.EquipmentID1);
 			hash.Add("EquipmentID2", info.EquipmentID2);
 			hash.Add("EquipmentID3", info.EquipmentID3);
 			hash.Add("ProcessType", info.ProcessType);
 				
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
             dict.Add("DevCode", ""); 
             dict.Add("EquipmentID1", "");
             dict.Add("EquipmentID2", "");
             dict.Add("EquipmentID3", "");
             dict.Add("ProcessType", "");
             #endregion

            return dict;
        }

    }
}