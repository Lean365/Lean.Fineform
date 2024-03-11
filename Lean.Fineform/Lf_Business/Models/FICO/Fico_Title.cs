using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Fine.Lf_Business.Models.FICO
{
    //会计科目
    public class Fico_Title : IKeyGUID
    {

        [Key]
        public Guid GUID { get; set; }
        [Required]
        public int Bacode { get; set; }//明细科目代码

        [Required, StringLength(100)]
        public string Bacnname { get; set; }//明细科目名称

        [Required, StringLength(100)]
        public string Bajpname { get; set; }//明细科目名称

        [Required, StringLength(100)]
        public string Baenname { get; set; }//明细科目名称
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