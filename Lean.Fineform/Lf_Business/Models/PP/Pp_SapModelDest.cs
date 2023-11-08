using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Pp_SapModelDest : IKeyGUID
    {
        //SAP机种仕向
        [Required, Key]

        public Guid GUID { get; set; }
        [Required, StringLength(20)]
        public string D_SAP_DEST_Z001 { get; set; }//物料
        [StringLength(40)]
        public string D_SAP_DEST_Z002 { get; set; }//仕向

        [StringLength(40)]
        public string D_SAP_DEST_Z003 { get; set; }//机种


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