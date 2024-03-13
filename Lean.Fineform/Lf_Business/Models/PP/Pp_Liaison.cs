using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    //设计变更
    public class Pp_Liaison : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [StringLength(8)]
        public string Ec_issuedate { get; set; }//发行日期

        [StringLength(50)]
        public string Ec_model { get; set; }//技术机种

        [StringLength(2000)]
        public string Ec_modellist { get; set; }//技术机种明细

        [StringLength(2000)]
        public string Ec_region { get; set; }//技术机种仕向明细

        [StringLength(50)]
        public string Ec_leader { get; set; }//技术DTA担当

        [StringLength(8)]
        public string Ec_enterdate { get; set; }//实施日期

        [StringLength(10)]
        public string Ec_letterno { get; set; }//技联NO

        [StringLength(500)]
        public string Ec_letterdoc { get; set; }//技联NO

        [StringLength(10)]
        public string Ec_eppletterno { get; set; }//P番联络书

        [StringLength(500)]
        public string Ec_eppletterdoc { get; set; }//P番联络书

        [StringLength(10)]
        public string Ec_teppletterno { get; set; }//P番联络书TCJ

        [StringLength(500)]
        public string Ec_teppletterdoc { get; set; }//P番联络书TCJ

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