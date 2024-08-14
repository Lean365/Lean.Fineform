using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.QM
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