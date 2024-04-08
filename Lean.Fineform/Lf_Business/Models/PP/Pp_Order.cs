using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_Order : IKeyGUID
    {
        //生产订单
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(4)]
        public string Porderplt { get; set; }//工厂

        [Required, StringLength(4)]
        public string Pordertype { get; set; }//订单类型

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

        [StringLength(255)]
        public string UDF01 { get; set; }

        [StringLength(255)]
        public string UDF02 { get; set; }

        [StringLength(255)]
        public string UDF03 { get; set; }

        [StringLength(500)]
        public string UDF04 { get; set; }

        [StringLength(500)]
        public string UDF05 { get; set; }

        [StringLength(500)]
        public string UDF06 { get; set; }

        public int UDF51 { get; set; }

        public int UDF52 { get; set; }

        public int UDF53 { get; set; }
        public Decimal UDF54 { get; set; }

        public Decimal UDF55 { get; set; }

        public Decimal UDF56 { get; set; }

        [Required]
        public byte isDeleted { get; set; }	//13	//	删除标记

        [StringLength(400)]
        public string Remark { get; set; }//备注

        [StringLength(50)]
        public string Creator { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string Modifier { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}