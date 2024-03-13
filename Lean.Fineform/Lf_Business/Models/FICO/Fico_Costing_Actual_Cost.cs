using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.FICO
{
    //预算实绩对比
    public class Fico_Costing_Actual_Cost : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(6)]
        public string Bc_FY { get; set; }//期间

        [Required, StringLength(6)]
        public string Bc_YM { get; set; }//年月

        [Required]
        public int Bc_CorpCode { get; set; }//公司代码

        [Required, StringLength(40)]
        public string Bc_CorpName { get; set; }//公司名称

        [Required,]
        public int Bc_CostCode { get; set; }//成本中心Code

        [Required, StringLength(20)]
        public string Bc_CostName { get; set; }//成本中心名称

        [Required]
        public int Bc_TitleCode { get; set; }//明细科目代码

        [Required, StringLength(40)]
        public string Bc_TitleName { get; set; }//明细科目名称

        [Required, StringLength(255)]
        public string Bc_TitleNote { get; set; }//说明

        public Decimal BC_BudgetAmt { get; set; }//预算金额

        public Decimal Bc_ActualAmt { get; set; }//实际发生

        public Decimal Bc_DiffAmt { get; set; }//预算-实际

        [Required, StringLength(8)]
        public string Bc_BalanceDate { get; set; }////结算日期，登録日付

        [Required, StringLength(50)]
        public string Bc_Accountant { get; set; }////核算员

        [Required, StringLength(50)]
        public string Bc_ExpCategory { get; set; }//费用分类

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