using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using YUN.Framework.ControlUtil;

namespace MES.SocketService.Entity
{
    /// <summary>
    /// META_DevLinkInfo
    /// </summary>
    [DataContract]
    public class META_DevLinkInfo : BaseEntity
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public META_DevLinkInfo()
        {
            // SQLite主键自增长 需设置主键为空
            ID = null;
        }

        #region Property Members

        [DataMember]
        public virtual int? ID { get; set; }

        [DataMember]
        public virtual string DevCode { get; set; }


        [DataMember]
        public virtual string EquipmentID1 { get; set; }

        [DataMember]
        public virtual string EquipmentID2 { get; set; }

        [DataMember]
        public virtual string EquipmentID3 { get; set; }

        [DataMember]
        public virtual string ProcessType { get; set; }


        #endregion

    }
}