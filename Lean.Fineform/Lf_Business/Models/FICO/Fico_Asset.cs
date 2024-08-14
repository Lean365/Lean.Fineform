using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.FICO
{
    //固定资产
    public class Fico_Asset : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(20)]
        public string Bsdept { get; set; }//部门

        [Required, StringLength(6)]
        public string Bafy { get; set; }//预算期间

        [Required, StringLength(100)]
        public string Bafm { get; set; }//预算年月

        [Required]
        public int Batitle { get; set; }//预算科目代码

        [Required, StringLength(100)]
        public string Baclass { get; set; }//预算科目名称

        [Required, StringLength(100)]
        public string Baname { get; set; }//固定资产名称

        [Required, StringLength(250)]
        public string Badesc { get; set; }//固定资产购买说明

        public int Bayears { get; set; }//使用年限

        public Decimal Bamoney { get; set; }//预算金额

        public Decimal Bactual { get; set; }//实际发生

        public Decimal Badiff { get; set; }//预算-实际

        public Decimal Badepreciation { get; set; }//折旧

        [Required]
        public bool Baflag { get; set; }//启用标志

        [StringLength(8)]
        public string Bacheckdate { get; set; }//预算审核

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