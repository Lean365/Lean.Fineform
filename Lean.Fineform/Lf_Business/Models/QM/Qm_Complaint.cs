using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Fine.Lf_Business.Models.QM
{
    public class Qm_Complaint : IKeyGUID
    {

        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(20)]
        public string Cc_DocNo { get; set; }//客诉编号内部

        [Required, StringLength(20)]
        public string Cc_IssuesNo { get; set; }//客诉编号外部

        [Required, StringLength(20)]
        public string Cc_Customer { get; set; }//客户代码

        [Required, StringLength(40)]
        public string Cc_Model { get; set; }//机种名

        [Required, StringLength(20)]
        public string Cc_Item { get; set; }//物料

        [Required, StringLength(20)]
        public string Cc_Region { get; set; }//仕向

        [StringLength(20)]
        public string Cc_Order { get; set; }//订单

        [Required, StringLength(8)]
        public string Cc_ReceivingDate { get; set; }//接收日期

        [Required,]
        public int Cc_DefectsQty { get; set; }//数量

        [StringLength(2000)]
        public string Cc_Issues { get; set; }//投诉事项

        [Required, StringLength(200)]
        public string Cc_Serialno { get; set; }//序列号

        [StringLength(200)]
        public string Cc_Reference { get; set; }//参考文件

        [Required, StringLength(2000)]
        public string Cc_DefectNotes { get; set; }//症状
        [Required, StringLength(2000)]
        public string Cc_Rootcauseanalysis { get; set; }//分析


        [StringLength(50)]
        public string qaModifier { get; set; }//QA登录
        public DateTime? qaModifyTime { get; set; }//QA登录日期

        [StringLength(20)]
        public string Cc_Line { get; set; }//班别
        [StringLength(8)]
        public string Cc_ProcessDate { get; set; }//处理日期

        [StringLength(4000)]
        public string Cc_Ddescription { get; set; }//问题描述P1D

        [StringLength(4000)]
        public string Cc_Reasons { get; set; }//原因分析P1D
        [StringLength(40)]
        public string Cc_Operator { get; set; }//作业员
        [StringLength(4000)]
        public string Cc_Station { get; set; }//工位

        [StringLength(40)]
        public string Cc_Lot { get; set; }//批次

        [StringLength(4000)]
        public string Cc_CorrectActions { get; set; }//改善对策P1D

        [StringLength(50)]
        public string p1dModifier { get; set; }//制一课登录
        public DateTime? p1dModifyTime { get; set; }//制一课登录日期
        [Required, StringLength(40)]
        public string Cc_Discover { get; set; }//承认部门        

        [Required, StringLength(8)]
        public string Cc_ReceivedDate { get; set; }//承认日期

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