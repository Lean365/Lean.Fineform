using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    //不良代码
    public class Pp_DefectCode : IKeyGUID
    {
        [Required, Key]
        public Guid GUID { get; set; }
        [StringLength(200)]
        public string ngclass { get; set; }

        [StringLength(200)]
        public string cn_classmatter { get; set; }

        [StringLength(200)]
        public string en_classmatter { get; set; }

        [StringLength(200)]
        public string jp_classmatter { get; set; }

        [StringLength(200)]
        public string ngcode { get; set; }

        [StringLength(200)]
        public string cn_ngmatter { get; set; }

        [StringLength(200)]
        public string en_ngmatter { get; set; }

        [StringLength(200)]
        public string jp_ngmatter { get; set; }

        [StringLength(200)]
        public string analysisclass { get; set; }

        [StringLength(200)]
        public string jp_class { get; set; }

        [StringLength(200)]
        public string jp_ng { get; set; }

        [StringLength(255)]
        public string Udf001 { get; set; }
        [StringLength(255)]
        public string Udf002 { get; set; }
        [StringLength(255)]
        public string Udf003 { get; set; }

        public Decimal Udf004 { get; set; }

        public Decimal Udf005 { get; set; }

        public Decimal Udf006 { get; set; }
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