using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_SapManhour : IKeyGUID
    {
        //SAP工时
        [Required, Key]
        public Guid GUID { get; set; }

        [Required, StringLength(4)]
        public string D_SAP_ZPBLD_Z001 { get; set; }//工厂

        [Required, StringLength(20)]
        public string D_SAP_ZPBLD_Z002 { get; set; }//物料

        [Required, StringLength(10)]
        public string D_SAP_ZPBLD_Z003 { get; set; }//工作中心

        [StringLength(40)]
        public string D_SAP_ZPBLD_Z004 { get; set; }//工作中心文本

        public Decimal D_SAP_ZPBLD_Z005 { get; set; }//值

        [StringLength(1)]
        public string D_SAP_ZPBLD_Z006 { get; set; }//单位

        public Decimal D_SAP_ZPBLD_Z007 { get; set; }//值

        [StringLength(3)]
        public string D_SAP_ZPBLD_Z008 { get; set; }//单位

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