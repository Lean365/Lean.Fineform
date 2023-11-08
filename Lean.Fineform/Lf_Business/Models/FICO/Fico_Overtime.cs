using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lean.Fineform
{
    //加班
    public class Fico_Overtime : IKeyGUID
    {

        [Key]
        public Guid GUID { get; set; }
        [Required, StringLength(20)]
        public string Bsdept { get; set; }//部门
        [Required, StringLength(6)]
        public string Bofy { get; set; }//预算期间

        [Required, StringLength(100)]
        public string Bofm { get; set; }//预算年月
        [Required]
        public int Botitle { get; set; }//预算科目代码

        [Required, StringLength(100)]
        public string Boclass { get; set; }//预算科目名称

        [Required]
        public Decimal Borequiredst { get; set; }//必要工数

        [Required]
        public int Bodirectemployee { get; set; }//保有人数

        [Required]
        public int Boindirectemployee { get; set; }//间接人数

        [Required]
        public int Bodays { get; set; }//上班天数

        [Required, StringLength(500)]
        public string Bocontent { get; set; }//加班事由内容

        [Required]
        public Decimal Boretainst { get; set; }//保有工数保有人数*上班天数*8小时*60分钟*稼动率0.95

        [Required]
        public Decimal Boretainstdiff { get; set; }//工数差异(必要工数-保有工数)

        [Required]
        public Decimal Boovertime { get; set; }//保有人员投入加班时间(工数差异/60)

        [Required]
        public Decimal Bodirectovertime { get; set; }//平均每人每月投入加班时间（保有人员投入加班时间/保有人数)

        [Required]
        public Decimal Boindirectovertime { get; set; }//间接每人每月投入加班时间（平均每人每月投入加班时间*间接人数）

        [Required]
        public Decimal Boovertimetotal { get; set; }//投入加班时间汇总（保有人员投入加班时间+间接每人每月投入加班时间）

        [Required]
        public bool Boflag { get; set; }//启用标志

        [StringLength(8)]
        public string Bocheckdate { get; set; }//加班审核
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