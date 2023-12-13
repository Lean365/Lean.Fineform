using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Pp_Order : IKeyGUID
    {
        //生产订单
        [Key]
        public Guid GUID { get; set; }
        [Required, StringLength(4)]
        public string Porderplt { get; set; }//工厂

        [Required, StringLength(7)]
        public string Porderno { get; set; }//单号

        [Required, StringLength(20)]
        public string Porderhbn { get; set; }//物料
        [Required, StringLength(20)]
        public string Porderlot { get; set; }//Lot
        [Required]
        public decimal Porderqty { get; set; }//台数
        [Required]
        public decimal Porderreal { get; set; }//生产台数
        [Required, StringLength(8)]
        public string Porderdate { get; set; }//日期
        [Required, StringLength(8)]
        public string Porderroute { get; set; }//工艺 

        [Required, StringLength(50)]
        public string Porderserial { get; set; }//序号

        [Required, StringLength(10)]
        public string Pordertype { get; set; }//订单类型

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