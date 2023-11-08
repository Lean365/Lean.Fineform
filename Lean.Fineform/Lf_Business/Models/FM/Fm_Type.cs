using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Fm_Type : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required, StringLength(1)]
        public string Type_ID { get; set; }
        [Required, StringLength(40)]
        public string Type_Name { get; set; }

        [StringLength(255)]
        public string Udf001 { get; set; }
        [StringLength(255)]
        public string Udf002 { get; set; }
        [StringLength(255)]
        public string Udf003 { get; set; }
        [StringLength(255)]
        public string Udf004 { get; set; }
        [StringLength(255)]
        public string Udf005 { get; set; }
        [StringLength(255)]
        public string UDF06 { get; set; }
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