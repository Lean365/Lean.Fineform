using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lean.Fineform
{
    //销售发票
    public class Fico_Costing_SalesInvoice : IKeyGUID
    {

        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(4)]
        public string Bc_Plnt { get; set; }//工厂

        [Required, StringLength(6)]
        public string Bc_FY { get; set; }//期间

        [Required, StringLength(6)]
        public string Bc_YM { get; set; }//年月


        [Required, StringLength(10)]
        public string Bc_Invoice { get; set; }//销售发票
        [Required, StringLength(4)]
        public string Bc_InvoiceDetails { get; set; }//销售发票明细

        [Required, StringLength(4)]
        public string Bc_ProfitCenter { get; set; }//利润中心

        [Required, StringLength(40)]
        public string Bc_ProfitCenterText { get; set; }//利润中心文本
        [Required, StringLength(10)]
        public string Bc_Subjects{ get; set; }//会计科目
        [Required, StringLength(40)]

        public string Bc_SubjectsText { get; set; }//科目文本
        [Required, ]
        public Decimal Bc_SalesQty { get; set; }//数量

        [Required, StringLength(2)]
        public string Bc_SalesUnit { get; set; }//单位
        [Required, ]
        public Decimal Bc_LocalAmount { get; set; }//本币金额
        [Required, ]
        public Decimal Bc_BusinessAmount { get; set; }//交易货币金额
        [Required, StringLength(4)]
        public string Bc_Currency { get; set; }//币种
        [Required, StringLength(20)]
        public string Bc_SalesItem { get; set; }//物料


        [Required, StringLength(40)]
        public string Bc_SalesItemText { get; set; }//物料文本
        [Required, StringLength(10)]
        public string Bc_CustomerCode { get; set; }//客户代码
        [Required, StringLength(40)]
        public string Bc_CustomerName { get; set; }//客户名称
        [Required, StringLength(10)]
        public string Bc_Reference { get; set; }//参考凭证
        [Required, StringLength(4)]
        public string Bc_ReferenceDetails { get; set; }//凭证明细
        [Required, StringLength(8)]
        public string Bc_PostingDate { get; set; }//过账日期
        [Required, StringLength(10)]
        public string Bc_PostingUser { get; set; }//用户
        [Required, StringLength(8)]
        public string Bc_EntryDate { get; set; }//录入日期
        [Required, StringLength(20)]
        public string Bc_EntryTime { get; set; }//录入时间
        [Required, StringLength(2)]
        public string Bc_Origin { get; set; }//源产地
        [Required, StringLength(2)]
        public string Bc_Pgroup { get; set; }//产品组
        [Required, StringLength(4)]
        public string Bc_MaterialType { get; set; }//物料类型

        [Required, StringLength(10)]
        public string Bc_InvoiceType { get; set; }//发票类型
        [Required, StringLength(40)]
        public string Bc_InvoiceText { get; set; }//发票文本
        [Required, StringLength(8)]
        public string Bc_Balancedate { get; set; }////结算日期，登録日付
        //勘定コード 
        //勘定コードテキスト   //
        //数量 
        //数量  
        //金額 
        //取引通貨額   
        //取引通貨額 Type    指図 指図テキスト  MvT 品目コード   Plnt 品目テキスト  得意先 得意先テキスト 伝票タイプ 仕入先 仕入先テキスト 登録時刻    伝票日付 登録日 ユーザ名 年度  転記日付 参照伝票No  Itm D   WBS 要素  伝票タイプ 明細テキスト  購買伝票 明細  販売伝票 明細  TTy D/C 製品部門    Plnt 品目Group MTyp 品目階層1   Hie Hierar  Orig St  D 通貨





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