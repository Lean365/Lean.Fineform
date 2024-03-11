using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fine.Lf_Business.Models.PP
{
    //设计变更
    public class Pp_Ec : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [StringLength(8)]
        public string Ec_issuedate { get; set; }//发行日期

        [StringLength(20)]
        public string Ec_no { get; set; }//设变号码 

        [StringLength(40)]
        public string Ec_status { get; set; }//设变状态 
        [StringLength(500)]
        public string Ec_title { get; set; }//技术设变标题

        [StringLength(4000)]
        public string Ec_details { get; set; }//技术设变内容

        [StringLength(50)]
        public string Ec_leader { get; set; }//技术DTA担当

        public Decimal Ec_lossamount { get; set; }//仕损金额

        public int Ec_distinction { get; set; }//管理区分
        [StringLength(500)]
        public string Ec_documents { get; set; }//SAP设变PDF
        [StringLength(10)]
        public string Ec_letterno { get; set; }//技联NO
        [StringLength(500)]
        public string Ec_letterdoc { get; set; }//技联NO
        [StringLength(10)]
        public string Ec_eppletterno { get; set; }//P番联络书
        [StringLength(500)]
        public string Ec_eppletterdoc { get; set; }//P番联络书

        [StringLength(10)]
        public string Ec_teppletterno { get; set; }//P番联络书TCJ
        [StringLength(500)]
        public string Ec_teppletterdoc { get; set; }//P番联络书TCJ
        [Required]
        public byte isModifysop { get; set; }  //13	//	SOP修改标记
        [Required]
        public byte isConfirm { get; set; }  //13	//	管理标记
        [Required]
        [StringLength(8)]
        public string Ec_entrydate { get; set; }//技术登录日期

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