using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Oa_Notice : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(8)]
        public string Notice_Pubdate { get; set; }//日期

        [Required, StringLength(4)]//类别
        public string Notice_Type { get; set; }
        [Required, StringLength(4)]//分类
        public string Notice_Category { get; set; }
        [Required, StringLength(100)]//标题
        public string Notice_Title { get; set; }

        [Required, StringLength(20)]//发行者
        public string Notice_Issuer { get; set; }

        [StringLength(2000)]//附件
        public string Notice_Attachments { get; set; }

        [Required, StringLength(4000)]//内容
        public string Notice_Contents { get; set; }

        [Required]//状态
        public byte Notice_Flag { get; set; }
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