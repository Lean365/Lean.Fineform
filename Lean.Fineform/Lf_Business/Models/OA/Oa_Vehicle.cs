using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Oa_Vehicle : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        public virtual Adm_Dept Dept { get; set; }//用车部门
        [Required, StringLength(5000)]//用车原因
        public string Vehicle_Reason { get; set; }
        [Required, StringLength(100)]//使用人
        public string Vehicle_Use { get; set; }
        [Required, StringLength(11)]//用车人或司机电话
        public string Vehicle_UseTel { get; set; }
        [Required]//人数含司机
        public int Vehicle_UseNum { get; set; }
        //预定起始时间
        [Required]
        public DateTime? Vehicle_Stime { get; set; }

        //预定返回时间
        [Required]
        public DateTime? Vehicle_Etime { get; set; }
        [Required, StringLength(20)]//汽车名称
        public string Vehicle_Name { get; set; }

        [Required, StringLength(20)]//驾驶员
        public string Vehicle_Driver { get; set; }

        [Required, StringLength(50)]//去程起始地点
        public string Vehicle_TripSplace { get; set; }

        [Required, StringLength(50)]//去程结束地点
        public string Vehicle_TripEplace { get; set; }
        [StringLength(100)]//去程结束地点
        public string Vehicle_RouteTripEplace { get; set; }

        //往返
        [Required]
        public bool Vehicle_Return { get; set; }
        [ StringLength(50)]//回程起始地点
        public string Vehicle_RetunSplace { get; set; }

        [ StringLength(50)]//回程结束地点
        public string Vehicle_RetunEplace { get; set; }
        [StringLength(100)]//回程途经地点
        public string Vehicle_RouteRetunEplace { get; set; }

        [StringLength(50)]//去程方式
        public string Vehicle_Inbound { get; set; }

        [StringLength(50)]//回程方式
        public string Vehicle_Outbound { get; set; }
        [Required]//审批状态
        public byte Vehicle_DeptConfirm { get; set; }
        [Required]//审批状态
        public byte Vehicle_GMConfirm { get; set; }
        [Required]//审批状态
        public byte Vehicle_GAConfirm { get; set; }

        [Required]//审批状态
        public byte Vehicle_Confirm { get; set; }

        [Required]
        public bool Vehicle_allday { get; set; }//可选，true or false，是否是全天事件。
        [StringLength(500)]
        public string Vehicle_url { get; set; }//可选，当指定后，事件被点击将打开对应url。
        [StringLength(500)]
        public string Vehicle_classname { get; set; }//指定事件的样式。
        public bool Vehicle_editable { get; set; }//事件是否可编辑，可编辑是指可以移动, 改变大小等。
        [StringLength(500)]
        public string Vehicle_source { get; set; }//指向次event的eventsource对象。
        [StringLength(20)]
        public string Vehicle_color { get; set; }//	背景和边框颜色。

        [StringLength(20)]
        public string Vehicle_bgcolor { get; set; }//背景颜色。
        [StringLength(20)]
        public string Vehicle_bdcolor { get; set; }//边框颜色。
        [StringLength(20)]
        public string Vehicle_textcolor { get; set; }//文本颜色。


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