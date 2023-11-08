using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lean.Fineform
{
    //签拟单
    public class Fico_Counter_Signature : IKeyGUID
    {

        [Key]
        public Guid GUID { get; set; }
        [Required]
        public int Beno { get; set; }//签拟单号码

        [Required]
        public int Beflag { get; set; }//是否急件处理

        [Required, StringLength(6)]
        public string Befy { get; set; }//预算期间

        [Required, StringLength(6)]
        public string Befm { get; set; }//预算年月

        [Required, StringLength(8)]
        public string Bedate { get; set; }//申请日期



        [Required, StringLength(20)]
        public string Bename { get; set; }//申请人员

        [Required]
        public int Becode { get; set; }//申请人员工号



        [Required]
        public bool Beenabled { get; set; }//是否有预算

        [Required]
        public int Betitle { get; set; }//预算科目代码

        [Required, StringLength(100)]
        public string Beclass { get; set; }//预算科目名称

        [Required]
        public int Betitlesub { get; set; }//明细科目代码

        [Required, StringLength(100)]
        public string Beclasssub { get; set; }//明细科目名称

        [Required, StringLength(255)]
        public string Beclassmemo { get; set; }//说明



        public Decimal Bebtmoney { get; set; }//预算金额

        public Decimal Beatmoney { get; set; }//申请金额

        [Required]
        public bool Beover { get; set; }//是否超预算

        public Decimal Bediffmoney { get; set; }//预算-实际金额

        [Required, StringLength(100)]
        public string Becaption { get; set; }//签拟单主题

        [Required, StringLength(1000)]
        public string Beexplanation { get; set; }//申请事由

        [StringLength(8)]
        public string Becheckdate { get; set; }//预算审核
        [StringLength(5000)]
        public string UDF01 { get; set; }
        [StringLength(5000)]
        public string UDF02 { get; set; }
        [StringLength(5000)]
        public string UDF03 { get; set; }
        [StringLength(5000)]
        public string UDF04 { get; set; }
        [StringLength(5000)]
        public string UDF05 { get; set; }
        [StringLength(5000)]
        public string UDF06 { get; set; }
        public Decimal UDF51 { get; set; }

        public Decimal UDF52 { get; set; }

        public Decimal UDF53 { get; set; }
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