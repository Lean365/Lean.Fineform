using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Qm_Reworkdata : IKeyGUID
    {
        //品質問題対応/不具合の改修対応
        [Key]
        public Guid GUID { get; set; }

        [StringLength(8)]
        public string Qcrd001 { get; set; }     //日期

        [StringLength(50)]
        public string Qcrd002 { get; set; }	    //	机种
        [StringLength(50)]
        public string Qcrd003 { get; set; }	    //	Lot

        public Decimal Qcrd004 { get; set; }	//	直接人员赁率
        public Decimal Qcrd005 { get; set; }	//	间接人员赁率

        [StringLength(500)]
        public string Qcrd006 { get; set; }     //  检讨・调查・试验内容

        public Decimal Qcrd007 { get; set; }	//	检讨・调查・试验费用
        public Int64 Qcrd008 { get; set; }	//	检讨会使用时间(分)
        public int Qcrd009 { get; set; }	//	直接人员参加人数
        public int Qcrd010 { get; set; }	//	间接人员参加人数
        public Int64 Qcrd011 { get; set; }	//	调查评价试验工作时间(分)
        public Decimal Qcrd012 { get; set; }	//	交通费、旅费
        public Decimal Qcrd013 { get; set; }	//	其他费用
        public Int64 Qcrd014 { get; set; }	//	其他作业時間(分)
        public Decimal Qcrd015 { get; set; }	//	其他设备购入费,工程费,搬运费

        [Required]
        public bool Qcrd016 { get; set; }   //需要不良改修对应

        [StringLength(500)]
        public string Qcrd017 { get; set; }     //	备注
        [StringLength(50)]
        public string Qcrdqarec { get; set; }     //	品质问题対応记录者
        //不良对应
        [StringLength(8)]
        public string Qcrd018 { get; set; }     //日期
        [StringLength(500)]
        public string Qcrd019 { get; set; }	    //	不良内容

        public Decimal Qcrd020 { get; set; }	//	选别・改修费用
        public Int64 Qcrd021 { get; set; }	    //	选别・改修时间(分)
        public Int64 Qcrd022 { get; set; }	    //	再检查时间(分)
        public Decimal Qcrd023 { get; set; }	//	交通费、旅费
        public Decimal Qcrd024 { get; set; }	//	仓库管理费
        public Decimal Qcrd025 { get; set; }	//	选别・改修其他费用

        [StringLength(500)]
        public string Qcrd026 { get; set; }	    //	选别・改修备注

        public Decimal Qcrd027 { get; set; }	//	向顾客的费用请求

        [StringLength(50)]
        public string Qcrd028 { get; set; }	    //	顾客名
        [StringLength(50)]
        public string Qcrd029 { get; set; }     //	Debit Note No
        public Decimal Qcrd030 { get; set; }    //	请求费用
        public Decimal Qcrd031 { get; set; }	//	其他费用

        [StringLength(500)]
        public string Qcrd032 { get; set; }     //	备注
        [StringLength(50)]
        public string Qcrdmcrec { get; set; }     //	生管品质问题対応记录者
        [StringLength(8)]
        public string Qcrd033 { get; set; }     //日期

        [StringLength(500)]
        public string Qcrd034 { get; set; }     //	不良内容

        public Decimal Qcrd035 { get; set; }	//	选别・改修费用
        public Int64 Qcrd036 { get; set; }	    //	选别・改修时间(分)
        public Int64 Qcrd037 { get; set; }	    //	再检查时间(分)
        public Decimal Qcrd038 { get; set; }	//	交通费、旅费
        public Decimal Qcrd039 { get; set; }	//	仓库管理费
        public Decimal Qcrd040{ get; set; }	//	选别・改修其他费用

        [StringLength(500)]
        public string Qcrd041 { get; set; }	    //	选别・改修备注

        public Decimal Qcrd042 { get; set; }	//	向顾客的费用请求

        [StringLength(50)]
        public string Qcrd043 { get; set; }	    //	顾客名
        [StringLength(50)]
        public string Qcrd044 { get; set; }     //	Debit Note No
        public Decimal Qcrd045 { get; set; }    //	请求费用
        public Decimal Qcrd046 { get; set; }	//	其他费用

        [StringLength(500)]
        public string Qcrd047 { get; set; } //	备注
        [StringLength(50)]
        public string Qcrdassrec { get; set; }     //	M/L不良改修対応记录者
        [StringLength(8)]
        public string Qcrd048 { get; set; }     //日期
        [StringLength(500)]
        public string Qcrd049 { get; set; }     //不良内容

        public Decimal Qcrd050 { get; set; }	//	选别・改修费用
        public Int64 Qcrd051 { get; set; }	    //	选别・改修时间(分)
        public Int64 Qcrd052 { get; set; }	    //	再检查时间(分)
        public Decimal Qcrd053 { get; set; }	//	交通费、旅费
        public Decimal Qcrd054 { get; set; }	//	仓库管理费
        public Decimal Qcrd055 { get; set; }	//	选别・改修其他费用

        [StringLength(500)]
        public string Qcrd056 { get; set; }	    //	选别・改修备注

        public Decimal Qcrd057 { get; set; }	//	向顾客的费用请求

        [StringLength(50)]
        public string Qcrd058 { get; set; }	    //	顾客名
        [StringLength(50)]
        public string Qcrd059 { get; set; }     //	Debit Note No
        public Decimal Qcrd060 { get; set; }    //	请求费用
        public Decimal Qcrd061 { get; set; }    //	其他费用

        [StringLength(500)]
        public string Qcrd062 { get; set; }	//	备注
        [StringLength(50)]
        public string Qcrdpcbrec { get; set; }	    //	PCBA不良改修対応记录者

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