using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.FICO
{
    //BOM成本
    public class Fico_Costing_Bom_Cost : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(4)]
        public string Bc_Plnt { get; set; }//工厂

        [Required, StringLength(6)]
        public string Bc_FY { get; set; }//期间

        [Required, StringLength(6)]
        public string Bc_YM { get; set; }//年月

        [Required, StringLength(20)]
        public string Bc_Item { get; set; }//物料

        [Required, StringLength(40)]
        public string Bc_ItemText { get; set; }//物料描述

        [Required]
        public Decimal Bc_MovingCost { get; set; }//移动平均成本

        [Required, StringLength(3)]
        public string Bc_Currency { get; set; }//币种，通货

        [Required, StringLength(8)]
        public string Bc_Balancedate { get; set; }//结算日期，登録日付

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