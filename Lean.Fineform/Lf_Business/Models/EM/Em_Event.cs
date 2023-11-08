using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Em_Event : IKeyID
    {
        [Key]
        public int ID { get; set; }//可选，事件唯一标识，重复的事件具有相同的id
        [Required, StringLength(10)]
        public string atuser { get; set; } //用户

        [Required, StringLength(1)]
        public string attypename { get; set; }//必须，事件类型
        [Required, StringLength(40)]
        public string attitle { get; set; }//必须，事件在日历上显示的title
        [Required, StringLength(500)]
        public string atcontent { get; set; }//在日历上的事件说明
        [Required]
        public bool atallday { get; set; }//可选，true or false，是否是全天事件。
        [Required]
        public DateTime atsdate { get; set; }//必须，事件的开始时间。
        [Required]
        public DateTime atedate { get; set; }//可选，结束时间。
        [StringLength(500)]
        public string aturl { get; set; }//可选，当指定后，事件被点击将打开对应url。

        [StringLength(500)]
        public string atclassname { get; set; }//指定事件的样式。
        public bool ateditable { get; set; }//事件是否可编辑，可编辑是指可以移动, 改变大小等。
        [StringLength(500)]
        public string atsource { get; set; }//指向次event的eventsource对象。
        [StringLength(20)]
        public string atcolor { get; set; }//	背景和边框颜色。

        [StringLength(20)]
        public string atbgcolor { get; set; }//背景颜色。
        [StringLength(20)]
        public string atbdcolor { get; set; }//边框颜色。
        [StringLength(20)]
        public string attextcolor { get; set; }//文本颜色。
        [StringLength(500)]
        public string Remark { get; set; } //备注

        [Required]
        public Guid GUID { get; set; } //GUID
        [StringLength(50)]
        public string CreateUser { get; set; }
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string ModiUser { get; set; }
        public DateTime? ModiTime { get; set; }



    }
}