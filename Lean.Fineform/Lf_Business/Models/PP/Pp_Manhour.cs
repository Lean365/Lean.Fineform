using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_Manhour : IKeyGUID
    {
        //生产工时
        [Key]
        public Guid GUID { get; set; }

        [StringLength(8)]
        public string Prodate { get; set; } //更新日期

        [Required, StringLength(4)]
        public string Proplnt { get; set; }//工厂

        [StringLength(20)]
        public string Proitem { get; set; }//品号

        [Required, StringLength(50)]
        public string Prowcname { get; set; }//工作中心

        [Required, StringLength(50)]
        public string Promodel { get; set; }//机种名

        [StringLength(100)]
        public string Protext { get; set; }//品号TEXT

        [StringLength(100)]
        public string Prowctext { get; set; }//工作中心文本

        public Decimal Proshort { get; set; }//short工时

        [StringLength(1)]
        public string Propset { get; set; }//short单位

        public Decimal Prorate { get; set; }//short换算工时Rate
        public Decimal Prost { get; set; }//工时

        [StringLength(3)]
        public string Proset { get; set; }//单位

        [StringLength(20)]
        public string Prodesc { get; set; }//仕向け地

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