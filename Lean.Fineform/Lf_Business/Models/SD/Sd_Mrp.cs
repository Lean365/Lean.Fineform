using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.SD
{
    public class Sd_Mrp : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(4)]
        public string Bc_Plnt { get; set; }//	工厂

        [Required, StringLength(6)]
        public string Bc_FY { get; set; }//期间

        [Required, StringLength(6)]
        public string Bc_YM { get; set; }//年月

        [Required, StringLength(20)]
        public string Bc_Material { get; set; }//	品目

        [Required, StringLength(40)]
        public string Bc_MaterialText { get; set; }//	品目テキスト

        [Required, StringLength(4)]
        public string Bc_ProfitCenter { get; set; }//	利益センタ

        [Required, StringLength(1)]
        public string Bc_PurchaseType { get; set; }//	調達タイプ

        [Required, StringLength(4)]
        public string Bc_MaterialType { get; set; }//	MTyp

        [Required, StringLength(4)]
        public string Bc_MaterialGroup { get; set; }//	品目 Group

        [Required, StringLength(4)]
        public string Bc_PurchaseGroup { get; set; }//	PGr

        [Required,]
        public int Bc_Moq { get; set; }//	MinLotSize

        [Required, StringLength(4)]
        public string Bc_AssessmentType { get; set; }//	ValCl

        [Required, StringLength(10)]
        public string Bc_SupplierCode { get; set; }//	仕入先

        [Required, StringLength(40)]
        public string Bc_SoupplierItem { get; set; }//	仕入先品目Code

        [Required,]
        public int Bc_LeadTime { get; set; }//	L/T

        [Required, StringLength(2)]
        public string Bc_TaxCode { get; set; }//	税Code

        [Required,]
        public Decimal Bc_Price { get; set; }//	購買価格

        [Required, StringLength(3)]
        public string Bc_Currency { get; set; }//	通貨

        [Required,]
        public int Bc_PerUnit { get; set; }//	価格単位

        [Required,]
        public Decimal Bc_AvailableQty { get; set; }//	在庫_Available_Qty

        [Required,]
        public Decimal Bc_AvailableAmt { get; set; }//	在庫_Available_Amt

        [Required,]
        public Decimal Bc_PurchaseOrder { get; set; }//	発注_Qty

        [Required,]
        public Decimal Bc_PurchaseAmt { get; set; }//	発注_Amt

        [Required,]
        public Decimal Bc_ReturnQty { get; set; }//	発注返品_Qty

        [Required,]
        public Decimal Bc_ReturnAmt { get; set; }//	発注返品_Amt

        [Required,]
        public Decimal Bc_RequestQty { get; set; }//	購買依頼_Qty

        [Required,]
        public Decimal Bc_RequestAmt { get; set; }//	購買依頼_Amt

        [Required,]
        public Decimal Bc_PlannedQty { get; set; }//	計画手配_Qty

        [Required,]
        public Decimal Bc_PlannedAmt { get; set; }//	計画手配_Amt

        [Required,]
        public Decimal Bc_OutboundQty { get; set; }//	出庫_Qty

        [Required,]
        public Decimal Bc_OutboundAmt { get; set; }//	出庫_Amt

        [Required,]
        public Decimal Bc_InventoryQty { get; set; }//	在庫_Qty

        [Required,]
        public Decimal Bc_InventoryAmt { get; set; }//	在庫_Amt

        [Required,]
        public Decimal Bc_OtherQty { get; set; }//	在庫_Other_Qty

        [Required,]
        public Decimal Bc_OtherAmt { get; set; }//	在庫_Other_Amt

        [Required,]
        public Decimal Bc_TotalQty { get; set; }//	総在庫_Qty

        [Required,]
        public Decimal Bc_TotalAmt { get; set; }//	総在庫_AMT

        [Required,]
        public Decimal Bc_PlannedCost { get; set; }//	計画原価

        [Required,]
        public Decimal Bc_MovingAverage { get; set; }//	移動平均単価

        [Required,]
        public Decimal Bc_CorrectionvalueQty { get; set; }//	補正値_総在庫Qty

        [Required,]
        public Decimal Bc_CorrectionvalueAmt { get; set; }//	補正値_総在庫Amt

        [Required,]
        public Decimal Bc_CustomerQty { get; set; }//	受注_Qty

        [Required,]
        public Decimal Bc_CustomerAmt { get; set; }//	受注_Amt

        [Required,]
        public Decimal Bc_ForecastQty { get; set; }//	FC_Qty

        [Required,]
        public Decimal Bc_ForecastAmt { get; set; }//	FC_Amt

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