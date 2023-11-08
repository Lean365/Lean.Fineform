using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Oa_Position_Village : IKeyID
    {
        [Key]
        public int ID { get; set; }

        [Required]//镇ID
        public Int64 Town_ID { get; set; }

        [Required]//村ID
        public Int64 Village_ID { get; set; }

        [Required, StringLength(64)]//村名
        public string Village_Name { get; set; }

        [Required, StringLength(10)]//邮编
        public string Zip_Code { get; set; }
        [Required, StringLength(10)]//区号
        public string City_Code { get; set; }
        [Required, StringLength(64)]//简称
        public string Short_Name { get; set; }
        [Required, StringLength(100)]//组合
        public string Merger_Name { get; set; }
        [Required, StringLength(200)]//拼音
        public string Pinyin_Name { get; set; }
        [Required, StringLength(64)]//经度
        public string Ing_Name { get; set; }
        [Required, StringLength(64)]//纬度
        public string Lat_Name { get; set; }

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