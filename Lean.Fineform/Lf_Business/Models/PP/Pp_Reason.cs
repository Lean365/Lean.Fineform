using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Pp_Reason : IKeyGUID
    {
        //生产不良原因
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(50)]
        public string Reasontype { get; set; }//异常类别
        [Required, StringLength(255)]
        public string Reasoncntext { get; set; }//停线原因   
        [Required, StringLength(255)]
        public string Reasonentext { get; set; }//停线原因   
        [Required, StringLength(255)]
        public string Reasonjptext { get; set; }//停线原因   

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