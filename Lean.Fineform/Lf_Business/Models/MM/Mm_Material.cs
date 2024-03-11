using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Fine.Lf_Business.Models.MM
{
    public class Mm_Material : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(8)]
        public string isDate { get; set; }//更新日期
        [Required, StringLength(4)]
        public string Plnt { get; set; }    //4	//	Plnt
        [Required, StringLength(20)]
        public string MatItem { get; set; }   //20	//	品目
        [Required, StringLength(1)]
        public string Industry { get; set; }    //1	//	行业领域
        [Required, StringLength(4)]
        public string MatType { get; set; }	//4	//	物料类型
                [Required, StringLength(40)]
        public string MatDescription { get; set; }	//40	//	品目テキスト
                [Required, StringLength(4)]
        public string BaseUnit { get; set; }	//4	//	基本计量单位
                [ StringLength(40)]
        public string ProHierarchy { get; set; }	//40	//	品目階層
                [Required, StringLength(10)]
        public string MatGroup { get; set; }	//10	//	品目 Group
                [StringLength(4)]
        public string PurGroup { get; set; }	//4	//	采购组
                [Required, StringLength(1)]
        public string PurType { get; set; }	//1	//	采购类型
                [StringLength(2)]
        public string SpecPurType { get; set; }	//2	//	特殊采购类
                [StringLength(1)]
        public string BulkMat { get; set; }	//1	//	散装物料

        public int Moq { get; set; }	//13	//	最小起订量


        public int RoundingVal { get; set; }	//13	//	丸め数量


        public int LeadTime { get; set; }	//3	//	计划交货时间

        public Decimal ProDays { get; set; }	//8	//	自制生产天数
                [StringLength(1)]
        public String isCheck { get; set; }	//1	//	过账到检验库存
                [Required, StringLength(5)]
        public string ProfitCenter { get; set; }	//5	//	利益センタ
                [StringLength(10)]
        public string DiffCode { get; set; }	//10	//	差异码
                [StringLength(1)]
        public string isLot { get; set; }	//1	//	批次管理
                [StringLength(40)]
        public string MPN { get; set; }	//40	//	制造商零件编号
                [StringLength(40)]
        public string Mfrs { get; set; }	//40	//	製造業者
                [Required, StringLength(4)]
        public string EvaluationType { get; set; }	//4	//	评估类

        public decimal MovingAvg { get; set; }	//13	//	移動平均
                [Required, StringLength(4)]
        public string Currency { get; set; }	//4	//	通貨
                [Required, StringLength(1)]
        public string PriceControl { get; set; }	//1	//	价格控制
                [Required]
        public int PriceUnit { get; set; }	//4	//	价格单位
                [StringLength(4)]
        public string SLoc { get; set; }	//4	//	生产仓储地点
                [StringLength(4)]
        public string ESLoc { get; set; }	//4	//	外部采购仓储地点
                [StringLength(40)]
        public string LocPosition { get; set; }	//40	//	库存仓位
        public decimal Inventory { get; set; }  //13	//	库存
        [StringLength(2)]
        public string LocEol { get; set; }	//40	//	生产停止标记
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