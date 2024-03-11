using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fine.Lf_Business.Models.QM
{
    public class Qm_CheckAQL : IKeyGUID//生产AQL表
    {
        //生产AQL表

        [Key]
        public Guid GUID { get; set; }//管理ID

        [Required,StringLength(40)]
        public string cLevel { get; set; }//抽样水准
        [Required]
        public int minQty { get; set; }//最小数
        [Required]
        public int maxQty { get; set; }//最大数
        [Required]
        public int samQty { get; set; }//抽样数


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