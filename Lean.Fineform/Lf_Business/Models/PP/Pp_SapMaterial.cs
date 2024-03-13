using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_SapMaterial : IKeyGUID
    {
        //SAP物料
        [Required, Key]
        public Guid GUID { get; set; }

        [StringLength(4)]
        public string D_SAP_ZCA1D_Z001 { get; set; } //	Plnt

        [Required, StringLength(20)]
        public string D_SAP_ZCA1D_Z002 { get; set; } //	品目

        [StringLength(1)]
        public string D_SAP_ZCA1D_Z003 { get; set; } //	行业领域

        [StringLength(4)]
        public string D_SAP_ZCA1D_Z004 { get; set; } //	物料类型

        [StringLength(40)]
        public string D_SAP_ZCA1D_Z005 { get; set; } //	品目テキスト

        [StringLength(4)]
        public string D_SAP_ZCA1D_Z006 { get; set; } //	基本计量单位

        [StringLength(40)]
        public string D_SAP_ZCA1D_Z007 { get; set; } //	品目階層

        [StringLength(10)]
        public string D_SAP_ZCA1D_Z008 { get; set; } //	品目 Group

        [StringLength(4)]
        public string D_SAP_ZCA1D_Z009 { get; set; } //	采购组

        [StringLength(1)]
        public string D_SAP_ZCA1D_Z010 { get; set; } //	采购类型

        [StringLength(2)]
        public string D_SAP_ZCA1D_Z011 { get; set; } //	特殊采购类

        [StringLength(1)]
        public string D_SAP_ZCA1D_Z012 { get; set; } //	散装物料

        public int D_SAP_ZCA1D_Z013 { get; set; } //	最小起订量

        [StringLength(4)]
        public string D_SAP_ZCA1D_Z014 { get; set; } //	基本计量单位

        public int D_SAP_ZCA1D_Z015 { get; set; } //	丸め数量

        [StringLength(4)]
        public string D_SAP_ZCA1D_Z016 { get; set; } //	基本计量单位

        [StringLength(8)]
        public string D_SAP_ZCA1D_Z017 { get; set; } //	计划交货时间

        public Decimal D_SAP_ZCA1D_Z018 { get; set; } //	自制生产天数

        [StringLength(1)]
        public string D_SAP_ZCA1D_Z019 { get; set; } //	过账到检验库存

        [StringLength(5)]
        public string D_SAP_ZCA1D_Z020 { get; set; } //	利益センタ

        [StringLength(10)]
        public string D_SAP_ZCA1D_Z021 { get; set; } //	差异码

        [StringLength(1)]
        public string D_SAP_ZCA1D_Z022 { get; set; } //	批次管理

        [StringLength(40)]
        public string D_SAP_ZCA1D_Z023 { get; set; } //	制造商零件编号

        [StringLength(40)]
        public string D_SAP_ZCA1D_Z024 { get; set; } //	製造業者

        [StringLength(4)]
        public string D_SAP_ZCA1D_Z025 { get; set; } //	评估类

        public Decimal D_SAP_ZCA1D_Z026 { get; set; } //	移動平均

        [StringLength(4)]
        public string D_SAP_ZCA1D_Z027 { get; set; } //	通貨

        [StringLength(1)]
        public string D_SAP_ZCA1D_Z028 { get; set; } //	价格控制

        [StringLength(4)]
        public string D_SAP_ZCA1D_Z029 { get; set; } //	价格单位

        [StringLength(4)]
        public string D_SAP_ZCA1D_Z030 { get; set; } //	生产仓储地点

        [StringLength(4)]
        public string D_SAP_ZCA1D_Z031 { get; set; } //	外部采购仓储地点

        [StringLength(40)]
        public string D_SAP_ZCA1D_Z032 { get; set; } //	库存仓位

        public decimal D_SAP_ZCA1D_Z033 { get; set; } //	库存

        [StringLength(2)]
        public string D_SAP_ZCA1D_Z034 { get; set; } //	标记

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