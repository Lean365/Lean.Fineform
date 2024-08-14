using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_P2d_Inspection_Defect : IKeyID
    {
        [Required, Key]
        public int ID { get; set; }

        [Required]
        public Guid GUID { get; set; }

        [StringLength(8)]
        public string Proinspdate { get; set; } //检查日期

        [StringLength(50)]
        public string Promodel { get; set; }//生产机种

        [StringLength(30)]
        public string Propcbtype { get; set; }//板别 类别F

        [StringLength(30)]
        public string Provisualtype { get; set; }//目视别

        [StringLength(30)]
        public string Provctype { get; set; }//vc线别

        public DateTime? Prosideadate { get; set; }//实装A

        public DateTime? Prosidebdate { get; set; }//实装B

        [StringLength(20)]
        public string Prodshiftname { get; set; }//生产组别  类别

        [StringLength(20)]
        public string Procensor { get; set; }//检查员

        [StringLength(20)]
        public string Proorder { get; set; }//生产订单

        [StringLength(20)]
        public string Prolot { get; set; }//生产LOT

        public int Proorderqty { get; set; }//订单台数

        public int Prorealqty { get; set; }//生产实绩

        public int Proispqty { get; set; }//检查台数

        [StringLength(30)]
        public string Propcbchecktype { get; set; }//检查状况

        [StringLength(20)]
        public string Prolinename { get; set; }//生产组别

        public int Proinsqtime { get; set; }//检查工数
        public int Proaoitime { get; set; }//AOI工数
        public int Probadqty { get; set; }//不良数量

        [StringLength(30)]
        public string Prohandle { get; set; }//手贴部品

        [StringLength(200)]
        public string Probadserial { get; set; }//序列号

        [StringLength(30)]
        public string Probadcontent { get; set; }//不良内容

        [StringLength(200)]
        public string Probadtype { get; set; }//不良区别

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