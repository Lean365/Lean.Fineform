using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fine.Lf_Business.Models.QM
{
    public class Qm_Improvement : IKeyGUID//生产质量控制分析对策
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
        public string qmCheckNotes { get; set; }//不良内容

        //原因分析及对策实施
        [Required, StringLength(20)]
        public string qmPersonnel { get; set; }//对策人员
        [StringLength(8)]
        public string qmDate { get; set; }//对应日期
        [StringLength(300)]
        public string qmDirectreason { get; set; }//直接发生原因
        [StringLength(300)]
        public string qmIndirectreason { get; set; }//间接流出原因
        [StringLength(300)]
        public string qmDisposal  { get; set; }//处置
        [StringLength(300)]
        public string qmDirectsolutions { get; set; }//直接对策
        [StringLength(300)]
        public string qmIndirectsolutions { get; set; }//间接对策

        //对策确认
        [Required, StringLength(20)]
        public string qmVerify { get; set; }//检证人员
        [StringLength(8)]
        public string qmCarryoutdate { get; set; }//实施日期
        public bool qmCarryoutverify { get; set; }//实施检证

        [StringLength(300)]
        public string qmSolutionsverify { get; set; }//对策实施检证

        [StringLength(300)]
        public string qmNotes { get; set; }//特记事项
        [StringLength(20)]
        public string qmIssueno { get; set; }//发行NO
        [StringLength(255)]
        public string UDF01 { get; set; }
        [StringLength(255)]
        public string UDF02 { get; set; }
        [StringLength(255)]
        public string UDF03 { get; set; }
        [StringLength(500)]
        public string UDF04 { get; set; }
        [StringLength(500)]
        public string UDF05 { get; set; }
        [StringLength(500)]
        public string UDF06 { get; set; }
        public int UDF51 { get; set; }

        public int UDF52 { get; set; }

        public int UDF53 { get; set; }
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