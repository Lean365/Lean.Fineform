using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System;

namespace Lean.Fineform
{
    //年度目标表
    public class Adm_Corpkpi : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }//主键GUID
        [Required, StringLength(10)]
        public string CorpAbbrName { get; set; }//公司简称
        [Required, StringLength(4)]
        public string CorpAnnual { get; set; }//年度
        [Required, StringLength(50000)]
        public string CorpTarget_CN { get; set; }//年度目标
        [Required, StringLength(50000)]
        public string CorpTarget_EN { get; set; }//年度目标
        [Required, StringLength(50000)]
        public string CorpTarget_JA { get; set; }//年度目标

        [Required]
        [DefaultValue(1)]//默认值
        public byte isDelete { get; set; } = 0;//删除标记默认值

        [StringLength(500)]
        public string Remark { get; set; } //备注


        [StringLength(50)]
        public string Creator { get; set; }
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string Modifier { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}