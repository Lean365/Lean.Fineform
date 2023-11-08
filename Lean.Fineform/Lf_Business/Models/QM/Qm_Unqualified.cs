using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Qm_Unqualified : IKeyGUID//生产质量控制不合格联络
    {

        [Key]
        public Guid GUID { get; set; }//管理ID

        [Required, StringLength(20)]
        public string qmInspector { get; set; }//检查人员
        [Required, StringLength(10)]
        public string qmLine { get; set; }//班别
        [Required, StringLength(20)]
        public string qmOrder { get; set; }//生产工单
        [StringLength(20)]
        public string qmModels { get; set; }//机种
        [Required, StringLength(20)]
        public string qmMaterial { get; set; }//物料
        [Required, StringLength(20)]
        public string qmRegion { get; set; }//仕向
        [Required, StringLength(8)]
        public string qmCheckdate { get; set; }//查检日期

        [Required, StringLength(20)]
        public string qmProlot { get; set; }//生产批号

        [Required, StringLength(50)]
        public string qmLotserial { get; set; }//批号说明

        [Required]
        public decimal qmRejectqty { get; set; }//验退数

        [Required, StringLength(10)]
        public string qmJudgmentlevel { get; set; }//不良级别

        [Required, StringLength(300)]
        public string qmCheckNotes { get; set; }//判定说明
        [StringLength(20)]
        public string qmIssueno { get; set; }//发行NO
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