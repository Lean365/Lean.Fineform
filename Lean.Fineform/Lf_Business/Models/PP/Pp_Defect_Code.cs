using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    //不良代码
    public class Pp_Defect_Code : IKeyGUID
    {
        [Required, Key]
        public Guid GUID { get; set; }

        [StringLength(200)]
        public string ngclass { get; set; }

        [StringLength(200)]
        public string cn_classmatter { get; set; }

        [StringLength(200)]
        public string en_classmatter { get; set; }

        [StringLength(200)]
        public string jp_classmatter { get; set; }

        [StringLength(200)]
        public string ngcode { get; set; }

        [StringLength(200)]
        public string cn_ngmatter { get; set; }

        [StringLength(200)]
        public string en_ngmatter { get; set; }

        [StringLength(200)]
        public string jp_ngmatter { get; set; }

        [StringLength(200)]
        public string analysisclass { get; set; }

        [StringLength(200)]
        public string jp_class { get; set; }

        [StringLength(200)]
        public string jp_ng { get; set; }

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