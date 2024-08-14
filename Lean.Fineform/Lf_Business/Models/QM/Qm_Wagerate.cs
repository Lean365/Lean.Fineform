using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.QM
{
    public class Qm_Wagerate : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(6)]
        public string Qcsd001 { get; set; }//年月

        [Required, StringLength(4)]
        public string Qcsd002 { get; set; }//工厂代码

        [Required, StringLength(3)]
        public string Qcsd003 { get; set; }//币种

        [Required]
        public Decimal Qcsd004 { get; set; }//月销售额

        [Required]
        public Decimal Qcsd005 { get; set; }//上班天数

        [Required]
        public Decimal Qcsd006 { get; set; }//直接人员赁率

        [Required]
        public Decimal Qcsd007 { get; set; }//直接人员

        [Required]
        public Decimal Qcsd008 { get; set; }//直接人员加班

        [Required]
        public Decimal Qcsd009 { get; set; }//直接人员工资

        [Required]
        public Decimal Qcsd010 { get; set; }//间接人员赁率

        [Required]
        public Decimal Qcsd011 { get; set; }//间接人员

        [Required]
        public Decimal Qcsd012 { get; set; }//间接人员加班

        [Required]
        public Decimal Qcsd013 { get; set; }//间接人员工资

        [StringLength(8)]
        public string Qcsdrec { get; set; }     //	品质问题対応记录者

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
        public byte IsDeleted { get; set; }	//13	//	删除标记

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