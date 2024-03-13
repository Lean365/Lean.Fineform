using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.SD
{
    public class Sd_So_Residue : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(10)]
        public string Bc_CustomerCode { get; set; }//客户代码

        [Required, StringLength(40)]
        public string Bc_CustomerName { get; set; }//客户名称

        [Required, StringLength(20)]
        public string Bc_SalesItem { get; set; }//物料

        [Required, StringLength(40)]
        public string Bc_SalesItemText { get; set; }//物料描述

        [Required, StringLength(4)]
        public string Bc_Location { get; set; }//库位

        [Required, StringLength(8)]
        public string Bc_DeliveryDate { get; set; }//交货日期

        [Required]
        public Decimal Bc_UnpaidQty { get; set; }//订单残

        [Required]
        public Decimal Bc_AlreadyQty { get; set; }//已交数量

        [Required, StringLength(10)]
        public string Bc_SalesOrder { get; set; }//采购订单

        [Required, StringLength(5)]
        public string Bc_SalesOrderDetails { get; set; }//明细

        [Required]
        public Decimal Bc_PlannedQty { get; set; }//订单计划数量

        [Required, StringLength(4)]
        public string Bc_SalesGroup { get; set; }//采购组

        [Required, StringLength(8)]
        public string Bc_SalesDate { get; set; }//采购日期

        [Required, StringLength(3)]
        public string Bc_SalesUnit { get; set; }//采购单位

        [Required]
        public Decimal Bc_SalesPrice { get; set; }//采购单价

        [Required, StringLength(3)]
        public string Bc_SalesCurrency { get; set; }//采购币种

        [Required, StringLength(2)]
        public string Bc_SalesTaxType { get; set; }//税别

        [Required, StringLength(4)]
        public string Bc_ProfitCenter { get; set; }//利润中心

        [Required, StringLength(4)]
        public string Bc_Plnt { get; set; }//工厂

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