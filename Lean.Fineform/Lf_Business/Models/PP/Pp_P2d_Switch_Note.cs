using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_P2d_Switch_Note : IKeyID
    {
        //生产日报SUB
        [Required, Key]
        public int ID { get; set; }

        [Required]
        public Guid GUID { get; set; }

        [StringLength(20)]
        public string Prodate { get; set; } //生产日期

        [Required]
        public int ProSmtSwitchNum { get; set; }//SMT切换次数

        [Required]
        public int ProSmtSwitchTotalTime { get; set; }//SMT总切换时间

        [Required]
        public int ProAitSwitchNum { get; set; }//自插切换次数

        [Required]
        public int ProAiStopTime { get; set; }//自插总切换时间

        [Required]
        public int ProHandSopTime { get; set; }//手插读SOP时间

        [Required]
        public int ProHandPerson { get; set; }//手插人数

        [Required]
        public int ProHandSopTotalTime { get; set; }//手插读SOP总时间

        [Required]
        public int ProHandSwitchNum { get; set; }//手插切换次数

        [Required]
        public int ProHandSwitchTime { get; set; }//手插切换时间

        [Required]
        public int ProHandSwitchTotalTime { get; set; }//手插切换总时间

        [Required]
        public int ProRepairSopTime { get; set; }//修正读SOP时间

        [Required]
        public int ProRepairPerson { get; set; }//修正人数

        [Required]
        public int ProRepairSopTotalTime { get; set; }//修正读SOP总时间

        [Required]
        public int ProRepairSwitchNum { get; set; }//修正切换次数

        [Required]
        public int ProRepairSwitchTime { get; set; }//修正切换时间

        [Required]
        public int ProRepairSwitchTotalTime { get; set; }//修正切换总时间

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