using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Qm_CheckType : IKeyGUID//生产AQL表
    {
        //检验类别方式种类
        [Key]
        public Guid GUID { get; set; }//管理ID

        [Required, StringLength(50)]
        public string Checktype { get; set; }//类别

        [Required, StringLength(50)]
        public string Checkcntext { get; set; }//检验名称EN

        [Required, StringLength(50)]
        public string Checkentext { get; set; }//检验名称JP

        [Required, StringLength(50)]
        public string Checkjptext { get; set; }//检验名称JP
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