using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Oa_Vehicle_Maintain : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(8)]//保养日期
        public string Vehicle_Date { get; set; }

        [Required, StringLength(7)]//牌照号码
        public string Vehicle_License { get; set; }

        [Required, StringLength(20)]//保养类别
        public string Vehicle_Type { get; set; }

        [Required, StringLength(500)]//保养说明
        public string Vehicle_Text { get; set; }
        [Required, StringLength(100)]//保养场所
        public string Vehicle_Place { get; set; }

        [Required]//保养费用
        public decimal Vehicle_Cost { get; set; }

        [Required, StringLength(50)]//费用承担
        public string Vehicle_Assume { get; set; }

        [Required, StringLength(500)]//事故原因
        public string Vehicle_Reason { get; set; }

        [Required, StringLength(50)]//事故责任
        public string Vehicle_Responsibility { get; set; }
        [Required]//状态
        public byte Vehicle_State { get; set; }


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