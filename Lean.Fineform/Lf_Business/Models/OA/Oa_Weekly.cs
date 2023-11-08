using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Oa_Weekly : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required,StringLength(8)]
        public string W_Date { get; set; }//日期

        [Required, StringLength(2)]
        public string W_Weekly { get; set; }//第几周

        [Required, StringLength(17)]
        public string W_During { get; set; }//时间段
        [Required, StringLength(17)]
        public string W_DeptName { get; set; }//部门

        [Required, StringLength(17)]
        public string W_Rapporteur { get; set; }//报告人

        [Required, StringLength(500)]
        public string W_People { get; set; }//人员状况

        [Required,]
        public string W_Jobcontent { get; set; }//工作内容

        [StringLength(500)]
        public string W_Jobcontentdoc { get; set; }//内容附件
        [StringLength(500),]
        public string W_ProductionStatusdoc { get; set; }//生产附件
        [StringLength(500),]
        public string W_Wipdoc { get; set; }//仕挂附件
        [StringLength(500),]
        public string W_ProductionResultsdoc { get; set; }//生产实绩附件
        [StringLength(500),]
        public string W_Salesdoc { get; set; }//出货实绩附件
        [StringLength(500),]
        public string W_CostDowndoc { get; set; }//降价附件
        [StringLength(500),]
        public string W_Shortagedoc { get; set; }//欠品附件
        [StringLength(500),]
        public string W_VenderEvaluationdoc { get; set; }//供应商评估附件
        [StringLength(500),]
        public string W_IqcJobcontentdoc { get; set; }//受入检查附件
        [StringLength(500),]
        public string W_QaJobcontentdoc { get; set; }//出货检查附件
        [StringLength(500),]
        public string W_DefectJobcontentdoc { get; set; }//不具合对应附件
        [StringLength(500),]
        public string W_Newmodelsdoc { get; set; }//新机种附件
        [StringLength(500),]
        public string W_EcNotedoc { get; set; }//设变附件

        [StringLength(500),]
        public string W_Inventorydoc { get; set; }//库存附件

        [StringLength(500),]
        public string W_WGPSdoc { get; set; }//WGPS附件
        [StringLength(500),]
        public string W_LossTimedoc { get; set; }//损失工数附件
        [StringLength(500),]
        public string W_ModelChangdoc { get; set; }//产线切换附件
        [StringLength(5000)]
        public string UDF01 { get; set; }
        [StringLength(5000)]
        public string UDF02 { get; set; }
        [StringLength(5000)]
        public string UDF03 { get; set; }
        [StringLength(5000)]
        public string UDF04 { get; set; }
        [StringLength(5000)]
        public string UDF05 { get; set; }
        [StringLength(5000)]
        public string UDF06 { get; set; }
        public Decimal UDF51 { get; set; }

        public Decimal UDF52 { get; set; }

        public Decimal UDF53 { get; set; }
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