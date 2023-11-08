using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Pp_EcBalance : IKeyGUID
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
        public byte isEnd { get; set; }//结束标志 
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