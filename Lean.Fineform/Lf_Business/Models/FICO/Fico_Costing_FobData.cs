using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Fine.Lf_Business.Models.FICO
{
    //FOB
    public class Fico_Costing_FobData : IKeyGUID
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
        public Decimal Bc_TcjFob { get; set; }//TCJ　FOB
        
        [Required, StringLength(3)]
        public string Bc_TcjCurrency { get; set; }//币种，通货


        [Required]
        public int Bc_TcjPerUnit { get; set; }//单位

        [Required, StringLength(4)]
        public string Bc_TcjProfitCenter { get; set; }//利润中心

        [Required]
        public Decimal Bc_DtaFob { get; set; }//TCJ　FOB

        [Required, StringLength(3)]
        public string Bc_DtaCurrency { get; set; }//币种，通货

        [Required]
        public int Bc_DtaPerUnit { get; set; }//单位

        [Required, StringLength(4)]
        public string Bc_DtaProfitCenter { get; set; }//利润中心
        [Required, StringLength(8)]
        public string Bc_EffectiveStartDate { get; set; }//有效日期
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