using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.SD
{
    public class Sd_Forcast : IKeyGUID
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
        public string Bc_ForecastItem { get; set; }//物料

        [Required, StringLength(10)]
        public string Bc_ForecastMrp { get; set; }//MRP要素

        [Required,]
        public Decimal Bc_ForecastQty { get; set; }//数量

        [Required]
        public Decimal Bc_ForecastAmount { get; set; }//金额

        [Required, StringLength(8)]
        public string Bc_DesiredDate { get; set; }//所需日期

        [Required, StringLength(8)]
        public string Bc_ForecastDate { get; set; }//FC日期

        [Required, StringLength(10)]
        public string Bc_OrderType { get; set; }//订单类型

        [Required, StringLength(8)]
        public string Bc_ExchangeRateDate { get; set; }//日期

        [Required,]
        public decimal Bc_ExchangeRate { get; set; }//汇率

        [Required]
        public Decimal Bc_MovingAverage { get; set; }//移动价格

        [Required]
        public int Bc_PerUnit { get; set; }//价格单位

        [Required, StringLength(3)]
        public string Bc_Currency { get; set; }//币种

        [Required, StringLength(40)]
        public string Bc_ForecastItemText { get; set; }//物料描述

        [Required, StringLength(40)]
        public string Bc_ForecastModelName { get; set; }//机种描述

        [Required, StringLength(40)]
        public string Bc_ForecastRegional { get; set; }//仕向地

        [Required, StringLength(10)]
        public string Bc_Discontinued { get; set; }//停产标记

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