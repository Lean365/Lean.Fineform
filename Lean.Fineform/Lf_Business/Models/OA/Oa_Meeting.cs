using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Oa_Meeting : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required, StringLength(40)]
        public string Meeting_Title { get; set; }//主题
        [Required, StringLength(10)]
        public string Meeting_Sdate { get; set; }//日期

        [Required, StringLength(5)]
        public string Meeting_Stime { get; set; }//时间
        [Required, StringLength(10)]
        public string Meeting_Edate { get; set; }//日期

        [Required, StringLength(5)]
        public string Meeting_Etime { get; set; }//时间
        [Required, StringLength(20)]
        public string Meeting_Type { get; set; }//属性
        [Required, StringLength(20)]
        public string Meeting_Rooms { get; set; }//会议室
        [Required, StringLength(2000)]
        public string Meeting_Attendees { get; set; }//参会人员        
        [Required]
        public byte Meeting_MailNotice { get; set; }//邮件通知

        [Required, StringLength(20)]
        public string Meeting_Contact { get; set; }//联系人

        [Required, StringLength(2000)]
        public string Meeting_Content { get; set; }//内容

        [Required, StringLength(100)]
        public string Meeting_Aattachment { get; set; }//附件

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