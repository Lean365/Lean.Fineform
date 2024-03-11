using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Fine.Lf_Business.Models.FICO
{
    //固定资产
    public class Fico_ActualReport : IKeyGUID
    {

        [Key]
        public Guid GUID { get; set; }
        [Required, StringLength(20)]
        public string Bc_Plnt { get; set; }//工厂
        [Required, StringLength(6)]
        public string Bc_FY { get; set; }//期间

        [Required, StringLength(6)]
        public string Bc_YM { get; set; }//年月

        [Required, StringLength(1)]
        public int Bc_MgtCategory { get; set; }//类别

        [Required, StringLength(100)]
        public string Bc_MgtName { get; set; }//名称

        [Required, StringLength(100)]
        public string Bc_MgtObjectives { get; set; }//明细

        [Required,]
        public Decimal Bc_ActualData { get; set; }//实绩
        [Required, StringLength(8)]
        public int Bc_Balancedate { get; set; }//日期

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