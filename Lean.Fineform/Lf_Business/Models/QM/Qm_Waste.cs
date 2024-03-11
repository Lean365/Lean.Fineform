using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fine.Lf_Business.Models.QM
{
    public class Qm_Waste : IKeyGUID
    {
        //废弃事故部品
        [Key]
        public Guid GUID { get; set; }//管理ID
        [StringLength(8)]
        public string Qcwd001 { get; set; }//日期
        [StringLength(20)]
        public string Qcwd002 { get; set; }        //	机种

        public Decimal Qcwd003 { get; set; } //	间接人员赁率
        [StringLength(15)]
        public string Qcwd004 { get; set; }//	部品品号
        [StringLength(100)]
        public string Qcwd005 { get; set; }//	部品品名
        [StringLength(300)]
        public string Qcwd006 { get; set; }//	事故内容

        public Decimal Qcwd007 { get; set; }//	废弃费用
        public Decimal Qcwd008 { get; set; }//	废弃数量
        public Decimal Qcwd009 { get; set; }//	部品单价
        public Decimal Qcwd010 { get; set; }//	废弃处理费用
        public Decimal Qcwd011 { get; set; }//	运费
        public Decimal Qcwd012 { get; set; }//	其他费用
        public int Qcwd013 { get; set; }//	处理作业时间(分)
        public Decimal Qcwd014 { get; set; }//	关税
        public Decimal Qcwd015 { get; set; }//	处理发生其他费用
        [StringLength(200)]
        public string Qcwd016 { get; set; }     //	设变
        [StringLength(50)]
        public string Qcwdrec { get; set; }     //	品质问题対応记录者
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