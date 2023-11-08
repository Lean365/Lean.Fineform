using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Pp_EcSub : IKeyGUID
    {
        //设计变更SUB
        [Key]
        public Guid GUID { get; set; }

        [StringLength(20)]
        public string Ec_no { get; set; }//设变号码 

        [StringLength(200)]
        public string Ec_model { get; set; }//技术机种明细  
        [StringLength(20)]
        public string Ec_bomitem { get; set; }//完成品  
        [StringLength(20)]
        public string Ec_bomsubitem { get; set; }//上阶品号  
        [StringLength(20)]
        public string Ec_olditem { get; set; }   //技术旧品号 

        [StringLength(40)]
        public string Ec_oldtext { get; set; }   //技术旧品名 

        public decimal Ec_oldqty { get; set; }   //技术旧品号 数量

        [StringLength(20)]
        public string Ec_oldset { get; set; }   //技术旧品号位置 


        [StringLength(20)]
        public string Ec_newitem { get; set; }//技术新品号
        [StringLength(40)]
        public string Ec_newtext { get; set; }   //技术新品名 

        public decimal Ec_newqty { get; set; }   //技术新品号 数量

        [StringLength(20)]
        public string Ec_newset { get; set; }   //技术新品号位置

        [StringLength(1)]
        public string Ec_procurement { get; set; }   //采购区分
        [StringLength(1)]
        public string isCheck { get; set; }   //检验否
        [StringLength(4)]
        public string Ec_location { get; set; }   //存放位置

        [StringLength(2)]
        public string Ec_eol { get; set; }   //EOL标志
        [Required]
        public byte isConfirm { get; set; }  //是否管理判断

        [StringLength(4)]
        public string Ec_bomno { get; set; }//BOM番号
        [StringLength(4)]
        public string Ec_change { get; set; }//互换
        [StringLength(4)]
        public string Ec_local { get; set; }//区分
        [StringLength(4)]
        public string Ec_note { get; set; }//指示

        [StringLength(4)]
        public string Ec_process { get; set; }//处理
        [StringLength(8)]
        public string Ec_bomdate { get; set; }//BOM生效日期


        [Required]
        [StringLength(8)]
        public string Ec_entrydate { get; set; }//技术登录日期
        [StringLength(8)]
        public string Ec_purdate { get; set; }//采购新品订单发行日期
        [StringLength(20)]
        public string Ec_purorder { get; set; }//采购新品订单号码

        [StringLength(200)]
        public string Ec_pursupplier { get; set; }//采购供应状况

        [StringLength(255)]
        public string Ec_purnote { get; set; }//采购注意事项 

        [StringLength(50)]
        public string ppModifier { get; set; }//采购修改
        public DateTime? ppModifyTime { get; set; }//采购修改日期

        [StringLength(8)]
        public string Ec_pmcdate { get; set; }//生管预定投入日期

        [StringLength(255)]
        public string Ec_pmclot { get; set; } //生管预定投入LOT 

        [StringLength(255)]
        public string Ec_pmcmemo { get; set; }//处理说明

        [StringLength(255)]
        public string Ec_pmcnote { get; set; }//注意事项 

        public decimal Ec_bstock { get; set; }//旧品在库

        [StringLength(50)]
        public string pmcModifier { get; set; }//生管修改
        public DateTime? pmcModifyTime { get; set; }//生管修改日期
        [StringLength(8)]
        public string Ec_iqcdate { get; set; }//IQC品管确认日期

        [StringLength(20)]
        public string Ec_iqcorder { get; set; }//IQC品管确认订单号
        [StringLength(255)]
        public string Ec_iqcnote { get; set; }//IQC品管确认说明

        [StringLength(50)]
        public string iqcModifier { get; set; }//iqc修改
        public DateTime? iqcModifyTime { get; set; }//iqc修改日期
        [StringLength(8)]
        public string Ec_mmdate { get; set; }//部管新品出库日期 
        [StringLength(255)]
        public string Ec_mmlot { get; set; }//部管新品出库LOT 
        [StringLength(20)]
        public string Ec_mmlotno { get; set; }//部管新品出库单号
        [StringLength(255)]
        public string Ec_mmnote { get; set; }//部管说明 

        [StringLength(50)]
        public string mmModifier { get; set; }//部管修改
        public DateTime? mmModifyTime { get; set; }//部管修改日期



        [StringLength(8)]
        public string Ec_p1ddate { get; set; }//制一课投入日期

        [StringLength(20)]
        public string Ec_p1dline { get; set; }//制一课班别

        [StringLength(255)]
        public string Ec_p1dlot { get; set; }//制一课投入LOT

        [StringLength(255)]
        public string Ec_p1dnote { get; set; }//制一课投入LOT说明
        [StringLength(50)]
        public string p1dModifier { get; set; }//制一课修改
        public DateTime? p1dModifyTime { get; set; }//制一课修改日期

        [StringLength(8)]
        public string Ec_p2ddate { get; set; }//制二课投入日期


        [StringLength(255)]
        public string Ec_p2dlot { get; set; }//制二课投入LOT


        [StringLength(255)]
        public string Ec_p2dnote { get; set; }//制二课投入LOT说明
        [StringLength(50)]
        public string p2dModifier { get; set; }//制二课修改
        public DateTime? p2dModifyTime { get; set; }//制二课修改日期
        [StringLength(8)]
        public string Ec_qadate { get; set; }//QA品管确认日期

        [StringLength(255)]
        public string Ec_qalot { get; set; }//QA品管确认LOT

        [StringLength(255)]
        public string Ec_qanote { get; set; }//QA品管确认说明
        [StringLength(50)]
        public string qaModifier { get; set; }//QA修改
        public DateTime? qaModifyTime { get; set; }//QA修改日期

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
        public int UDF51 { get; set; }

        public int UDF52 { get; set; }

        public int UDF53 { get; set; }
        public Decimal UDF54 { get; set; }

        public Decimal UDF55 { get; set; }

        public Decimal UDF56 { get; set; }

        [StringLength(5000)]
        public string Remark { get; set; }//备注
        [Required]
        public byte isDelete { get; set; }  //13	//	删除标记

        [StringLength(50)]
        public string Creator { get; set; }
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string Modifier { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}