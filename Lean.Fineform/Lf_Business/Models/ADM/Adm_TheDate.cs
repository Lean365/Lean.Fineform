using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    //日历表
    public class Adm_TheDate : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required]
        public int TheYear { get; set; }//当前年
        [Required]
        public int TheMonth { get; set; }//当前月
        [Required]
        public int TheDay { get; set; }//当前日
        [Required,StringLength(10)]
        public string TheWeekDay { get; set; }//星期几
        [Required, StringLength(10)]
        public string TheWeeks { get; set; }//第几周
        [Required]
        public int TheMonths { get; set; }//第几月
        [Required]
        public byte isWorkDay { get; set; }//工作日
        [Required]
        public DateTime TheDatetime { get; set; }//当前年
        [StringLength(500)]
        public string Remark { get; set; }


        [StringLength(50)]
        public string Creator { get; set; }
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string Modifier { get; set; }
        public DateTime? ModifyTime { get; set; }

    }
}