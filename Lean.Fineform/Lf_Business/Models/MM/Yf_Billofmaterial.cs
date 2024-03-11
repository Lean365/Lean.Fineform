using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Fine.Lf_Business.Models.MM
{
    public class YF_Billofmaterial : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [StringLength(20)]
        public string Serialno { get; set; } //序号
        [StringLength(20)]
        public string Stratum { get; set; } //层级
        [StringLength(50)]
        public string Ecno { get; set; } //设变号码
        [StringLength(20)]
        public string SubMatAttribute { get; set; } //物料属性
        [StringLength(10)]
        public string SubMatGroup { get; set; } //物料组
        [StringLength(20)]
        public string EngSequences { get; set; } //工程顺
        [StringLength(200)]
        public string EngLocation { get; set; } //位置
        [StringLength(20)]
        public string TopMaterial { get; set; } //上阶物料
        [StringLength(20)]
        public string SubMaterial { get; set; } //子物料

        public Decimal Usageqty { get; set; } //用量
        [StringLength(20)]
        public string Supplier { get; set; } //供应商
        [StringLength(10)]
        public string Currency { get; set; } //币种
        //public Decimal StdPrice { get; set; } //标价
        //public Decimal StdAmount { get; set; } //标价汇总=标准价格*用量
        public Decimal LastPrice { get; set; } //进价
        //public Decimal LastAmount { get; set; } //进价汇总=进价*用量
        public Decimal Inventory { get; set; } //库存
        //public Decimal InvAmount { get; set; } //库存金额=库存*进价
        [StringLength(10)]
        public string Purchaser { get; set; } //采购组
        [StringLength(100)]
        public string SubMatText { get; set; } //描述
        public int Leadtime { get; set; } //LT
        public int Moq { get; set; } //MOQ
        [StringLength(100)]
        public string Maker { get; set; } //制造商
        [StringLength(100)]
        public string MakerMaterial { get; set; } //制造商
        [StringLength(100)]
        public string MakerMatText { get; set; } //制造商
        [StringLength(8)]
        public string ProcurementRegion { get; set; } //采购地区
        [StringLength(20)]
        public string SubMatCategory { get; set; } //物料类别


    }

}