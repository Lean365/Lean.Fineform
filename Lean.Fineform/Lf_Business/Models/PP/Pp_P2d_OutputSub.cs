using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_P2d_OutputSub : IKeyID
    {
        //生产日报SUB
        [Required, Key]
        public int ID { get; set; }

        [Required]
        public Guid GUID { get; set; }

        public int Parent { get; set; }

        [Required, StringLength(20)]
        public string Proordertype { get; set; }//生产订单类别

        [Required, StringLength(20)]
        public string Proorder { get; set; }//生产订单

        [Required]
        public Decimal Proorderqty { get; set; }//订单台数

        [StringLength(20)]
        public string Prolinename { get; set; }//生产组别

        [StringLength(20)]
        public string Prodate { get; set; } //生产日期

        public int Prodirect { get; set; }//直接人员

        public int Proindirect { get; set; }//间接人员

        [StringLength(20)]
        public string Prolot { get; set; }//LOT批次

        [StringLength(50)]
        public string Promodel { get; set; }//机种名

        [StringLength(20)]
        public string Prohbn { get; set; }//生产物料

        [StringLength(20)]
        public string Propcbatype { get; set; }//生产板别

        [StringLength(20)]
        public string Propcbaside { get; set; } //	多面板

        public Decimal Prost { get; set; }//工时

        public Decimal Proshort { get; set; }//点数
        public Decimal Prorate { get; set; }//汇率

        public Decimal Prostdcapacity { get; set; }//标准产能

        [Required]
        public bool Totaltag { get; set; }//是否参与计算标记

        [StringLength(20)]
        public string Prostime { get; set; }//生产时间段

        [StringLength(20)]
        public string Proetime { get; set; }//生产时间段

        public int Prorealqty { get; set; }//生产实绩
        public int Prorealtotal { get; set; }//累计生产数

        [StringLength(2000)]
        public string Propcbserial { get; set; }//序列号

        public int Prolinestopmin { get; set; }//停线时间

        [StringLength(200)]
        public string Prostopcou { get; set; }//停线原因

        [StringLength(200)]
        public string Prostopmemo { get; set; }//停线备注

        [StringLength(300)]
        public string Probadcou { get; set; }//未达成原因

        [StringLength(300)]
        public string Probadmemo { get; set; }//未达成备注

        public int Prolinemin { get; set; }//生产工数
        public int Prorealtime { get; set; }//实际工数

        [StringLength(20)]
        public string Propcbastated { get; set; }//	完成情况

        public int Protime { get; set; }//生产工数
        public int Prohandoffnum { get; set; }//切换次数
        public int Prohandofftime { get; set; }//切换时间
        public int Prodowntime { get; set; } //切停机时间
        public int Prolosstime { get; set; } //损失工数
        public int Promaketime { get; set; }//投入工数
        public Decimal Proworkst { get; set; }//实绩生产工时
        public Decimal Prostdiff { get; set; }//工时差异
        public int Proqtydiff { get; set; }//预计投入台数
        public int Proratio { get; set; }//稼动率 （实际生产效率)

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
        public byte isDeleted { get; set; }	//13	//	删除标记

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