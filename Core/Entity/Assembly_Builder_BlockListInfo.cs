using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using YUN.Framework.ControlUtil;

namespace MES.SocketService.Entity
{
    /// <summary>
    /// Assembly_Builder_BlockListInfo
    /// </summary>
    [DataContract]
    public class Assembly_Builder_BlockListInfo : BaseEntity
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
	    public Assembly_Builder_BlockListInfo()
		{
            this.ID= System.Guid.NewGuid();
                this.IsUsed= false;
             this.Seq= 0;
             this.ActionDatetime= System.DateTime.Now;
    
		}

        #region Property Members
        
		[DataMember]
        public virtual Guid ID { get; set; }

		[DataMember]
        public virtual string WO { get; set; }

		[DataMember]
        public virtual string BlockID { get; set; }

		[DataMember]
        public virtual string SN { get; set; }

		[DataMember]
        public virtual bool IsUsed { get; set; }

		[DataMember]
        public virtual int Seq { get; set; }

		[DataMember]
        public virtual DateTime ActionDatetime { get; set; }

		[DataMember]
        public virtual DateTime PRTDatetime { get; set; }

		[DataMember]
        public virtual string ImpSafeCode { get; set; }


        #endregion

    }
}