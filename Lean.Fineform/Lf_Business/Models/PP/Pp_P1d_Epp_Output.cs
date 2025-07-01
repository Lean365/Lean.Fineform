using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_P1d_Epp_Output : IKeyID, IKeyGUID
    {
        //生产日报

        [Required, Key]
        public int ID { get; set; }

        [Required]
        public Guid GUID { get; set; }

        [Required, StringLength(20)]
        public string Proordertype { get; set; }//生产订单类别

        [Required, StringLength(20)]
        public string Proorder { get; set; }//生产订单

        [Required, StringLength(20)]
        public string Prolinename { get; set; }//生产组别

        [Required, StringLength(20)]
        public string Prodate { get; set; } //生产日期

        [Required]
        public int Prodirect { get; set; }//直接人员

        [Required]
        public int Proindirect { get; set; }//间接人员

        [Required]
        [StringLength(20)]
        public string Prolot { get; set; }//生产LOT

        [Required, StringLength(20)]
        public string Prohbn { get; set; }//生产品号

        [Required, StringLength(200)]
        public string Prosn { get; set; }//生产批号LOT

        [Required]
        public Decimal Proorderqty { get; set; }//订单台数

        [Required, StringLength(50)]
        public string Promodel { get; set; }//机种名

        public Decimal Prost { get; set; }//工时

        public Decimal Prostdcapacity { get; set; }//标准产能

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