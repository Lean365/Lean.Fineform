using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_Tracking_Count : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(4)]
        public string Pro_Plnt { get; set; }//工厂

        [Required, StringLength(6)]
        public string Pro_Line { get; set; }//班组

        [Required, StringLength(4)]
        public string Pro_Process { get; set; }//工序

        [Required, StringLength(4)]
        public string Pro_Station { get; set; }//站点

        [Required, StringLength(8)]
        public string Pro_Date { get; set; }//生产日期

        [Required, StringLength(20)]
        public string Pro_Item { get; set; }//机种物料

        [Required, StringLength(40)]
        public string Pro_Model { get; set; }//机种

        [Required, StringLength(10)]
        public string Pro_Region { get; set; }//仕向

        [Required, StringLength(40)]
        public string Pro_Lot { get; set; }//生产批次

        [Required,]
        public Decimal Pro_ProcessQty { get; set; }//台数

        [Required,]
        public Decimal Pro_MaxTime { get; set; }//最大

        [Required,]
        public Decimal Pro_MinTime { get; set; }//最小

        [Required,]
        public Decimal Pro_AvgTime { get; set; }//平均

        [Required,]
        public Decimal Pro_StdDev { get; set; }//标准差

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