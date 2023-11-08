using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Pp_EcCategory : IKeyGUID
    {
        //管理区分
        [Key]
        public Guid GUID { get; set; }

        public int Difftype { get; set; }//管理区分ID
        [StringLength(40)]
        public string Diffcnname { get; set; }//管理区分名称
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