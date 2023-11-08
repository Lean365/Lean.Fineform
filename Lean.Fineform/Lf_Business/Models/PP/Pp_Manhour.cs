using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Pp_Manhour : IKeyGUID
    {
        //生产工时
        [Key]
        public Guid GUID { get; set; }

        [StringLength(8)]
        public string Prodate { get; set; } //更新日期

        [Required, StringLength(4)]
        public string Proplnt { get; set; }//工厂
        [StringLength(20)]
        public string Proitem { get; set; }//品号
        [Required, StringLength(50)]
        public string Prowcname { get; set; }//工作中心
        [Required, StringLength(50)]
        public string Promodel { get; set; }//机种名
        [StringLength(100)]
        public string Protext { get; set; }//品号TEXT 
        [StringLength(100)]
        public string Prowctext { get; set; }//工作中心文本 

        public Decimal Proshort { get; set; }//short工时
        [StringLength(1)]
        public string Propset { get; set; }//short单位

        public Decimal Prorate { get; set; }//short换算工时Rate
        public Decimal Prost { get; set; }//工时
        [StringLength(3)]
        public string Proset { get; set; }//单位
        [StringLength(20)]
        public string Prodesc { get; set; }//仕向け地


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