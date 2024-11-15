﻿using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine
{
    //内部计算公式表
    public class Adm_Formula : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }//主键GUID

        [Required, StringLength(10)]
        public string FormulaType { get; set; }//类别

        [Required, StringLength(50000)]
        public string Formula_CN { get; set; }//年度目标

        [Required, StringLength(50000)]
        public string Formula_EN { get; set; }//年度目标

        [Required, StringLength(50000)]
        public string Formula_JA { get; set; }//年度目标

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