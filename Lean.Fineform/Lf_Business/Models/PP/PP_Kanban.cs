using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_Kanban : IKeyGUID
    {
        //SAP机种仕向
        [Required, Key]
        public Guid GUID { get; set; }

        [Required, StringLength(8)]
        public string P_Kanban_Date { get; set; }//投入日期

        [Required, StringLength(8)]
        public string P_Kanban_Line { get; set; }//班组

        [Required, StringLength(8)]
        public string P_Kanban_Order { get; set; }//工单

        [Required, StringLength(20)]
        public string P_Kanban_Lot { get; set; }//投入Lot

        [Required, StringLength(20)]
        public string P_Kanban_Item { get; set; }//物料

        [StringLength(40)]
        public string P_Kanban_Region { get; set; }//仕向

        [StringLength(40)]
        public string P_Kanban_Model { get; set; }//机种

        public int P_Kanban_Process { get; set; }//工序

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