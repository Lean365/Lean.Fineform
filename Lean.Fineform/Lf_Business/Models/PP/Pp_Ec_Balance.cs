using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_Ec_Balance : IKeyGUID
    {
        //设变Balance
        [Key]
        public Guid GUID { get; set; }

        [StringLength(8)]
        public string Ec_balancedate { get; set; }//更新日期

        [StringLength(40)]
        public string Ec_olditem { get; set; }//旧物料

        public decimal Ec_oldqty { get; set; }//在库
        public decimal Ec_poqty { get; set; }//PO残

        public decimal Ec_balanceqty { get; set; }//结余

        [StringLength(40)]
        public string Ec_newitem { get; set; }//新品号

        public decimal Ec_newqty { get; set; }//在库

        [StringLength(400)]
        public string Ec_precess { get; set; }//处理方法

        [StringLength(400)]
        public string Ec_note { get; set; }//注意事项

        [StringLength(100)]
        public string Ec_no { get; set; }//设变号码

        [StringLength(8)]
        public string Ec_issuedate { get; set; }//设变日期

        [StringLength(40)]
        public string Ec_status { get; set; }//设变状态

        [StringLength(400)]
        public string Ec_model { get; set; }//机种

        [StringLength(20)]
        public string Ec_item { get; set; }//完成品

        [Required]
        public byte isEnd { get; set; }//结束标志

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