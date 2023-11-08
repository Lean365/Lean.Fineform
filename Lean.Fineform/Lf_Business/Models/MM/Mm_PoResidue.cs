using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lean.Fineform
{
    public class Mm_PoResidue : IKeyGUID
    {

        [Key]
        public Guid GUID { get; set; }


        [Required, StringLength(10)]
        public string Bc_SupplierCode { get; set; }//供应商代码

        [Required, StringLength(40)]
        public string Bc_SupplierName { get; set; }//供应商名称

        [Required, StringLength(20)]
        public string Bc_PurchaseItem { get; set; }//物料

        [Required, StringLength(40)]
        public string Bc_PurchaseItemText { get; set; }//物料描述

        [Required,StringLength(4)]
        public string Bc_Location { get; set; }//库位
        [Required, StringLength(8)]
        public string Bc_DeliveryDate { get; set; }//交货日期
        [Required]
        public Decimal Bc_UnpaidQty { get; set; }//订单残
        [Required]
        public Decimal Bc_AlreadyQty { get; set; }//已交数量


        [Required, StringLength(10)]
        public string Bc_PurchaseOrder { get; set; }//采购订单
        [Required, StringLength(5)]
        public string Bc_PurchaseOrderDetails { get; set; }//明细
        [Required]
        public Decimal Bc_PlannedQty { get; set; }//订单计划数量
        [Required, StringLength(4)]
        public string Bc_PurchaseGroup { get; set; }//采购组
        [Required, StringLength(8)]
        public string Bc_PurchaseDate { get; set; }//采购日期
        [Required, StringLength(3)]
        public string Bc_PurchaseUnit { get; set; }//采购单位
        [Required]
        public Decimal Bc_UnitPrice { get; set; }//采购单价
        [Required, StringLength(3)]
        public string Bc_PurchaseCurrency { get; set; }//采购币种
        [Required, StringLength(2)]
        public string Bc_PurchaseTaxType { get; set; }//税别

        [Required, StringLength(4)]
        public string Bc_ProfitCenter { get; set; }//利润中心
        [Required, StringLength(4)]
        public string Bc_Plnt { get; set; }//工厂

        [Required, StringLength(8)]
        public string Bc_Balancedate { get; set; }////结算日期，登録日付
        [StringLength(5000)]
        public string UDF01 { get; set; }
        [StringLength(5000)]
        public string UDF02 { get; set; }
        [StringLength(5000)]
        public string UDF03 { get; set; }
        [StringLength(5000)]
        public string UDF04 { get; set; }
        [StringLength(5000)]
        public string UDF05 { get; set; }
        [StringLength(5000)]
        public string UDF06 { get; set; }
        public Decimal UDF51 { get; set; }

        public Decimal UDF52 { get; set; }

        public Decimal UDF53 { get; set; }
        public Decimal UDF54 { get; set; }

        public Decimal UDF55 { get; set; }

        public Decimal UDF56 { get; set; }

        [Required]
        public byte isDelete { get; set; }	//13	//	删除标记

        [StringLength(400)]
        public string Remark { get; set; }//备注

        [StringLength(50)]
        public string Creator { get; set; }
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string Modifier { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}