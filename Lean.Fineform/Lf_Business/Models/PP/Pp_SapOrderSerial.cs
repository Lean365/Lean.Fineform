using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_SapOrderSerial : IKeyGUID
    {
        //SAP序列号
        [Required, Key]
        public Guid GUID { get; set; }

        [StringLength(4)]
        public string D_SAP_SER05_C001 { get; set; } //	Plnt

        [Required, StringLength(10)]
        public string D_SAP_SER05_C002 { get; set; } //	工单

        [StringLength(20)]
        public string D_SAP_SER05_C003 { get; set; } //	物料

        [StringLength(40)]
        public string D_SAP_SER05_C004 { get; set; } //	序列号

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
        public byte IsDeleted { get; set; }	//13	//	删除标记

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