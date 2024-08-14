using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_Defect_Total : IKeyGUID
    {
        //生产班组
        [Key]
        public Guid GUID { get; set; }

        [StringLength(20)]
        public string Prolot { get; set; }//生产LOT

        [StringLength(20)]
        public string Promodel { get; set; }//生产机种

        [StringLength(200)]
        public string Prolinename { get; set; }//生产组别

        [StringLength(200)]
        public string Prodate { get; set; } //生产日期

        [StringLength(10)]
        public string Proorder { get; set; } //生产订单

        public int Proorderqty { get; set; }//订单数量

        public int Prorealqty { get; set; }//生产实绩

        public int Pronobadqty { get; set; }//无不良数量

        public int Probadtotal { get; set; }//不良件数（同一LOT集计数量）

        public decimal Prodirectrate { get; set; }//直行率（同一LOT集计数量）
        public decimal Probadrate { get; set; }//不良率（同一LOT集计数量）

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