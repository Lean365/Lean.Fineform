using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.FICO
{
    //费用
    public class Fico_Expense : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(20)]
        public string Bedept { get; set; }//部门

        [Required, StringLength(6)]
        public string Befy { get; set; }//预算期间

        [Required, StringLength(100)]
        public string Befm { get; set; }//预算年月

        [Required]
        public int Betitle { get; set; }//预算科目代码

        [Required, StringLength(100)]
        public string Beclass { get; set; }//预算科目名称

        [Required]
        public int Betitlesub { get; set; }//明细科目代码

        [Required, StringLength(100)]
        public string Beclasssub { get; set; }//明细科目名称

        [Required, StringLength(255)]
        public string Beclassmemo { get; set; }//说明

        public Decimal Bebtmoney { get; set; }//预算金额

        public Decimal Beatmoney { get; set; }//实际发生

        public Decimal Bediffmoney { get; set; }//预算-实际

        [Required]
        public bool Beflag { get; set; }//启用标志

        [StringLength(8)]
        public string Becheckdate { get; set; }//预算审核

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