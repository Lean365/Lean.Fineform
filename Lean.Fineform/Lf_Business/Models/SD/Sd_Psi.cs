using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Fine.Lf_Business.Models.SD
{
    public class Sd_Psi : IKeyGUID
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
        public string Bc_PsiItem { get; set; }//物料

        [Required, StringLength(10)]
        public string Bc_PsiMrp { get; set; }//MRP要素
        [Required, StringLength(3)]
        public string Bc_PsiVera { get; set; }//PSI版本A

        [Required,]
        public Decimal Bc_PsiVera_Qty { get; set; }//数量

        [Required, StringLength(3)]
        public string Bc_PsiVerb { get; set; }//PSI版本B
        [Required,]
        public Decimal Bc_PsiVerb_Qty { get; set; }//数量

        [Required,]
        public Decimal Bc_PsiDiff_Qty { get; set; }//增减数量

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