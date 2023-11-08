using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Pp_SapEcnSub : IKeyGUID
    {

        //设计变更SUB SAP
        [Required, Key]

        public Guid GUID { get; set; }
        [StringLength(6)]
        public string D_SAP_ZPABD_S001 { get; set; }//	设变号码	
        [StringLength(20)]
        public string D_SAP_ZPABD_S002 { get; set; }//	完成品	
        [StringLength(20)]
        public string D_SAP_ZPABD_S003 { get; set; }//	上阶品号	
        [StringLength(20)]
        public string D_SAP_ZPABD_S004 { get; set; }//	旧品号	
        [StringLength(40)]
        public string D_SAP_ZPABD_S005 { get; set; }//	品名	

        public decimal D_SAP_ZPABD_S006 { get; set; }//	数量	
        [StringLength(40)]
        public string D_SAP_ZPABD_S007 { get; set; }//	位置	
        [StringLength(20)]
        public string D_SAP_ZPABD_S008 { get; set; }//	新品号	
        [StringLength(40)]
        public string D_SAP_ZPABD_S009 { get; set; }//	品名	

        public decimal D_SAP_ZPABD_S010 { get; set; }//	数量	
        [StringLength(40)]
        public string D_SAP_ZPABD_S011 { get; set; }//	位置	
        [StringLength(4)]
        public string D_SAP_ZPABD_S012 { get; set; }//	番号	
        [StringLength(4)]
        public string D_SAP_ZPABD_S013 { get; set; }//	互换	
        [StringLength(4)]
        public string D_SAP_ZPABD_S014 { get; set; }//	区分	
        [StringLength(4)]
        public string D_SAP_ZPABD_S015 { get; set; }//	指示	
        [StringLength(4)]
        public string D_SAP_ZPABD_S016 { get; set; }//	旧品处理	
        [StringLength(8)]
        public string D_SAP_ZPABD_S017 { get; set; }//	BOM生效日期	
        [StringLength(255)]
        public string Udf001 { get; set; }
        [StringLength(255)]
        public string Udf002 { get; set; }
        [StringLength(255)]
        public string Udf003 { get; set; }

        public Decimal Udf004 { get; set; }

        public Decimal Udf005 { get; set; }

        public Decimal Udf006 { get; set; }
        [Required]
        public byte isDelete { get; set; }	//13	//	删除标记

        [StringLength(400)]
        public string Remark { get; set; }//备注

        [StringLength(50)]
        public string Creator { get; set; }
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string Modifier { get; set; }
        public DateTime? ModifyTime { get; set; }


    }
}