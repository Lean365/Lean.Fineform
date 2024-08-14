using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_Tracking : IKeyGUID
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
        public string Pro_Lot { get; set; }//生产批次

        [Required, StringLength(10)]
        public string Pro_Control { get; set; }//流水号

        [Required, StringLength(20)]
        public string Pro_Stime { get; set; }//开始时间

        [Required, StringLength(20)]
        public string Pro_Etime { get; set; }//结束时间

        [Required, StringLength(50)]
        public string Pro_QrCode { get; set; }//二维信息

        [Required,]
        public Decimal Pro_OperatingTime { get; set; }//作业时间

        [Required,]
        public Decimal Pro_StdDeviation { get; set; }//标准差

        [Required, StringLength(40)]
        public string Pro_Cputmp { get; set; }//站点CPU

        [Required, StringLength(40)]
        public string Pro_Mactmp { get; set; }//站点MAC

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