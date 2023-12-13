using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Pp_SapOrder : IKeyGUID
    {
        //SAP订单
        [Required, Key]

        public Guid GUID { get; set; }
        [Required, StringLength(4)]
        public string D_SAP_COOIS_C001 { get; set; }//工厂
        [Required, StringLength(10)]
        public string D_SAP_COOIS_C002 { get; set; }//订单
        [Required, StringLength(20)]
        public string D_SAP_COOIS_C003 { get; set; }//品号

        [StringLength(40)]
        public string D_SAP_COOIS_C004 { get; set; }//LOT


        public Decimal D_SAP_COOIS_C005 { get; set; }//数量


        public Decimal D_SAP_COOIS_C006 { get; set; }//已生产

        [StringLength(8)]
        public string D_SAP_COOIS_C007 { get; set; }//开始日期

        [StringLength(10)]
        public string D_SAP_COOIS_C008 { get; set; }//作业手顺

        [StringLength(10)]
        public string D_SAP_COOIS_C009 { get; set; }//订单类型
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