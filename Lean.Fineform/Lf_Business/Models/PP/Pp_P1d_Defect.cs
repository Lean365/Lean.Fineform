using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    //生产不良
    public class Pp_P1d_Defect : IKeyID
    {
        [Required, Key]
        public int ID { get; set; }
        [Required]
        public Guid GUID { get; set; }

        [StringLength(20)]
        public string Prolot { get; set; }//生产LOT
        [StringLength(20)]
        public string Promodel { get; set; }//生产机种

        [StringLength(20)]
        public string Proorder { get; set; }//生产订单

        [StringLength(20)]
        public string Prolinename { get; set; }//生产组别 

        [StringLength(20)]
        public string Prodate { get; set; } //生产日期
        public int Proorderqty { get; set; }//订单台数

        public int Prorealqty { get; set; }//生产实绩
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