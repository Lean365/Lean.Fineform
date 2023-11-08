using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Qm_Acceptancerate : IKeyGUID
    {

        [Key]
        public Guid GUID { get; set; }//管理ID
        [Required, StringLength(10)]
        public string qmLinename { get; set; }//班别
        [StringLength(20)]
        public string qmModel { get; set; }//机种
        [Required, StringLength(8)]
        public string qmCheckdate { get; set; }//查检日期

        [Required, StringLength(20)]
        public string qmProlot { get; set; }//生产批号

        [Required]
        public decimal qmRejectqty { get; set; }//验退数

        [Required]
        public decimal qmAcceptqty { get; set; }//验收数
        [Required]
        public decimal qmProqty { get; set; }//生产数
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