using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.FICO
{
    //历史库存
    public class Fico_Monthly_Inventory : IKeyGUID
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

        [Required, StringLength(4)]
        public string Bc_Assessment { get; set; }//评估类

        [Required, StringLength(1)]
        public string Bc_PriceControl { get; set; }//价格控制

        [Required]
        public Decimal Bc_MovingAverage { get; set; }//移动平均

        [Required]
        public int Bc_PerUnit { get; set; }//价格单位

        [Required, StringLength(3)]
        public string Bc_Currency { get; set; }//币种

        [Required]
        public Decimal Bc_Totalinventory { get; set; }//总库存

        [Required]
        public Decimal Bc_Totalamount { get; set; }//总金额

        [Required, StringLength(8)]
        public string Bc_Balancedate { get; set; }////结算日期，登録日付

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