using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    //生产不良
    public class Pp_P2d_Defect : IKeyID
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
        public string Propcbtype { get; set; }//板别

        [StringLength(20)]
        public string Prolinename { get; set; }//生产组别        

        public int Prorealqty { get; set; }//生产实绩

        [StringLength(30)]
        public string Propcbcardno { get; set; }//卡号
        [StringLength(30)]
        public string Propcbchecktype { get; set; }//检查状况

        [StringLength(30)]
        public string Propcbcheckout { get; set; }//检出工程
        public int Pronobadqty { get; set; }//无不良数量

        [StringLength(20)]//不良类别
        public string Prongdept { get; set; }
        public int Probadqty { get; set; }//不良数量

        public int Probadtotal { get; set; }//不良台数（同一LOT同一类别集计数量）      
        public int Probadamount { get; set; }//不良台数（同一LOT同一类别集计数量）    
        [StringLength(200)]
        public string Probadnote { get; set; }//不良症状
        [StringLength(200)]
        public string Probadset { get; set; }//不良个所
        [StringLength(200)]
        public string Probadreason { get; set; }//不良原因

        [StringLength(200)]
        public string Probadprop { get; set; }//不良性质

        [StringLength(200)]
        public string Probadserial { get; set; }//序列号
        [StringLength(200)]
        public string Probadresponsibility { get; set; }//责任归属

        [StringLength(200)]
        public string Probadrepairman { get; set; }//修理
        [StringLength(255)]
        public string Udf001 { get; set; }
        [StringLength(255)]
        public string Udf002 { get; set; }
        [StringLength(255)]
        public string Udf003 { get; set; }

        public Decimal Udf004 { get; set; }

        public Decimal Udf005 { get; set; }

        public Decimal Udf006 { get; set; }

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