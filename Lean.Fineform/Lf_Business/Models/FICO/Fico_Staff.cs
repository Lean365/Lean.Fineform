using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lean.Fineform
{
    //人员
    public class Fico_Staff : IKeyGUID
    {

        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(20)]
        public string Bsdept { get; set; }//部门

        [Required, StringLength(6)]
        public string Bsfy { get; set; }//预算期间

        [Required, StringLength(100)]
        public string Bsfm { get; set; }//预算年月

        [Required]
        public int Bstitle { get; set; }//预算科目代码

        [Required, StringLength(100)]
        public string Bsclass { get; set; }//预算科目名称

        [Required, StringLength(100)]
        public string Bscategory { get; set; }//人员类别

        [Required]
        public int Bskeeppersonnel { get; set; }//保有人数

        [Required]
        public int Bsnowpersonnel { get; set; }//现有人数

        [Required]
        public int Bspersonnel { get; set; }//预算人数

        [Required]
        public bool Bsflag { get; set; }//启用标志

        [StringLength(8)]
        public string Bscheckdate { get; set; }//人员审核
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