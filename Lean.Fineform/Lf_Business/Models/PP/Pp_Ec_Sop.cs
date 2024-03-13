using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_Ec_Sop : IKeyGUID
    {
        //设计变更SUB
        [Key]
        public Guid GUID { get; set; }

        [StringLength(8)]
        public string Ec_issuedate { get; set; }//发行日期

        [StringLength(20)]
        public string Ec_no { get; set; }//设变号码

        [Required]
        [StringLength(8)]
        public string Ec_entrydate { get; set; }//技术登录日期

        [StringLength(50)]
        public string Ec_leader { get; set; }//技术DTA担当

        [StringLength(200)]
        public string Ec_model { get; set; }//技术机种明细

        [Required]
        public byte ispengaModifysop { get; set; }///制技确认SOP

        [StringLength(8)]
        public string Ec_pengadate { get; set; }//制技确认日期

        [StringLength(255)]
        public string Ec_penganote { get; set; }///制技确认SOP修改

        [StringLength(50)]
        public string pengaModifier { get; set; }///制技修改

        public DateTime? pengaModifyDate { get; set; }///制技修改日期

        [Required]
        public byte ispengpModifysop { get; set; }///制技确认SOP

        [StringLength(8)]
        public string Ec_pengpdate { get; set; }//制技确认日期

        [StringLength(255)]
        public string Ec_pengpnote { get; set; }///制技确认SOP修改

        [StringLength(50)]
        public string pengpModifier { get; set; }///制技修改

        public DateTime? pengpModifyDate { get; set; }///制技修改日期

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
        public byte isDeleted { get; set; }	//13	//	删除标记

        [StringLength(400)]
        public string Remark { get; set; }//备注

        [StringLength(50)]
        public string Creator { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string Modifier { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}