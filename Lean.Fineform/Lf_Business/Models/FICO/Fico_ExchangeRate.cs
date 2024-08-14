using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.FICO
{
    //汇率
    public class Fico_ExchangeRate : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(8)]
        public string ER_EffectiveDate { get; set; }//SAP有效起始日

        [Required,]
        public decimal ER_Std { get; set; }//基准

        [Required, StringLength(3)]
        public string ER_fmCurrency { get; set; }//币种

        [Required,]
        public decimal ER_Rate { get; set; }//汇率

        [Required, StringLength(3)]
        public string ER_toCurrency { get; set; }//币种

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