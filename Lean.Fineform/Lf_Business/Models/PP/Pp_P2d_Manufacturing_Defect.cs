﻿using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_P2d_Manufacturing_Defect : IKeyID
    {
        [Required, Key]
        public int ID { get; set; }

        [Required]
        public Guid GUID { get; set; }

        [StringLength(8)]
        public string Prodate { get; set; } //生产日期

        [StringLength(50)]
        public string Promodel { get; set; }//生产机种

        [StringLength(20)]
        public string Proorder { get; set; }//生产订单

        [StringLength(20)]
        public string Prolot { get; set; }//生产LOT

        public int Proorderqty { get; set; }//订单台数

        [StringLength(30)]
        public string Propcbtype { get; set; }//板别 类别 B

        public int Prorealqty { get; set; }//生产实绩

        [StringLength(20)]
        public string Prolinename { get; set; }//生产组别

        [StringLength(30)]
        public string Propcbcardno { get; set; }//卡号

        [StringLength(200)]
        public string Probadnote { get; set; }//不良症状

        [StringLength(30)]
        public string Propcbcheckout { get; set; }//检出工程 类别L

        [StringLength(200)]
        public string Probadreason { get; set; }//不良原因

        public int Probadqty { get; set; }//不良数量
        public int Probadtotal { get; set; }//不良台数（同一LOT同一类别集计数量）

        [StringLength(200)]
        public string Probadresponsibility { get; set; }//责任归属 类别C

        [StringLength(200)]
        public string Probadprop { get; set; }//不良性质  类别 E

        [StringLength(200)]
        public string Probadserial { get; set; }//序列号

        [StringLength(200)]
        public string Probadrepairman { get; set; }//修理 类别M

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