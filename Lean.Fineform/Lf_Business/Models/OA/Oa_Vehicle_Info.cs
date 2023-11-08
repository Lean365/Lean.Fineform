using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Oa_Vehicle_Info : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(20)]//品牌型号
        public string Vehicle_Board { get; set; }
        [Required, StringLength(20)]//车型
        public string Vehicle_Type { get; set; }
        [Required, StringLength(20)]//车系型号
        public string Vehicle_Text { get; set; }

        [Required, StringLength(7)]//牌照号码
        public string Vehicle_License { get; set; }
        [Required, StringLength(20)]//车辆识别码
        public string Vehicle_Edentification { get; set; }
        [Required, StringLength(20)]//发动机号码
        public string Vehicle_Engineno { get; set; }


        [Required, StringLength(8)]//购置日期
        public string Vehicle_Purchasedate { get; set; }

        [Required, StringLength(50)]//销售公司
        public string Vehicle_Salesname { get; set; }
        [Required, StringLength(200)]//销售公司地点
        public string Vehicle_Salesaddr { get; set; }
        [Required, StringLength(50)]//销售公司电话
        public string Vehicle_Salestel { get; set; }
        [Required, StringLength(50)]//销售公司联系人
        public string Vehicle_Salescontact { get; set; }

        [Required, StringLength(8)]//上户日期
        public string Vehicle_Boardingdate { get; set; }
        [Required, StringLength(20)]//保险公司
        public string Vehicle_Insurancename { get; set; }

        [Required, StringLength(20)]//营业网点
        public string Vehicle_Insurancenetwork { get; set; }

        [Required, StringLength(50)]//营业地址
        public string Vehicle_Insuranceaddr { get; set; }
        [Required, StringLength(8)]//保险期限
        public string Vehicle_Insurancedate { get; set; }

        //往返
        [Required, StringLength(8)]//审验日期
        public string Vehicle_Inspectiondate { get; set; }

        [Required]//车况
        public decimal Vehicle_Mileage { get; set; }

        [Required, StringLength(10)]//使用单位
        public string Vehicle_Usedept { get; set; }

        [Required, StringLength(10)]//使用负责人
        public string Vehicle_UseName { get; set; }

        [Required]//状态
        public byte Vehicle_State { get; set; }

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