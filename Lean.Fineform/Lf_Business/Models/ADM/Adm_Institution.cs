using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System;
namespace Fine
{
    //机构信息表
    public class Adm_Institution : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [DisplayName("机构类型")]

        [Required, StringLength(20)]

        public string Category { get; set; }//机构分类

        [Required, StringLength(200)]
        public string EnName { get; set; }//英文简称

        [Required, StringLength(50)]
        public string ShortName { get; set; }//机构简称

        [Required, StringLength(200)]
        public string FullName { get; set; }//机构名称

        [Required, StringLength(200)]
        public string EnFullName { get; set; }//机构名称
        [Required, StringLength(50)]
        public string Nature { get; set; }//机构性质
        [Required, StringLength(50)]
        public string OuterPhone { get; set; }//外线电话

        [Required, StringLength(50)]
        public string InnerPhone { get; set; }//内线电话

        [Required, StringLength(50)]
        public string Fax { get; set; }//传真

        [Required, StringLength(10)]
        public string Postalcode { get; set; }//邮编

        [StringLength(50)]
        public string Email { get; set; }//电子邮箱

        [Required, StringLength(50)]
        public string OrgCode { get; set; }//机构代码

        [Required, StringLength(50)]
        public string Corporate { get; set; }//法人

        [Required, StringLength(50)]
        public string ProvinceId { get; set; }//省

        [Required, StringLength(50)]
        public string CityId { get; set; }//市区

        [Required, StringLength(50)]
        public string CountyId { get; set; }//区县
        [StringLength(50)]
        public string TownId { get; set; }//乡镇街道
        [StringLength(50)]
        public string VillageId { get; set; }//村社区

        [Required, StringLength(200)]
        public string Address { get; set; }//详细地址
        [Required, StringLength(200)]
        public string EnAddress { get; set; }//详细地址
        [StringLength(50)]
        public string WebAddress { get; set; }//web地址

        [Required]
        public DateTime FoundedTime { get; set; }//成立时间

        [Required, StringLength(200)]
        public string BusinessScope { get; set; }//经营范围
        [Required]
        public int SortCode { get; set; }//排序码

        //[Range(1, 100, ErrorMessage = "{0}在{0}与{1}之间")] //带参数的错误信息
        //[Range(1, 100, ErrorMessage = "{0}在{0}与{1}之间")] //带参数的错误信息
        [Required]
        [DefaultValue(1)]//默认值
        public byte isDelete { get; set; } = 0;//删除标记默认值
        [DefaultValue(0)]//默认值
        public byte isEnabled { get; set; } = 0;//有效标记默认值

        [Required, StringLength(200)]
        public string Slogan { get; set; }//企业理念，口号，标语
        [Required, StringLength(200)]
        public string EnSlogan { get; set; }//企业理念，口号，标语
        [StringLength(200)]
        public string JpSlogan { get; set; }//企业理念，口号，标语

        [StringLength(500)]
        public string Remark { get; set; } //备注
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

        [StringLength(50)]
        public string Creator { get; set; }
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string Modifier { get; set; }
        public DateTime? ModifyTime { get; set; }

    }
}