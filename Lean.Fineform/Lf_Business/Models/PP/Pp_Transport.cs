using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    //生产不良
    public class Pp_Transport : IKeyGUID//运输方式
    {
        [Required,Key]
        public Guid GUID { get; set; }
        [StringLength(20)]
        public string Transportype { get; set; }//运输类别
        [StringLength(20)]
        public string Transportcntext { get; set; }//名称
        [StringLength(20)]
        public string Transportentext { get; set; }//名称
        [StringLength(20)]
        public string Transportjptext { get; set; }//名称

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