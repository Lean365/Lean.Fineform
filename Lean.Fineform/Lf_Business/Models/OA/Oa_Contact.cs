using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System;

namespace Fine.Lf_Business.Models.OA
{
    public class Oa_Contact : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(2)]

        public string Su_Type { get; set; }//类别[A:邮件信息B：系统用户C：SAP用户]

        [Required, StringLength(20)]

        public string Su_Code { get; set; }//用户代码
        [Required, StringLength(50)]
        public string Su_Name_CN { get; set; }//英文简称

        [Required, StringLength(50)]
        public string Su_Name_EN { get; set; }//英文简称

        [Required, StringLength(50)]
        public string Su_Dept { get; set; }//部门简称

        [Required, StringLength(50)]
        public string Su_Corp { get; set; }//公司代码
        [StringLength(50)]
        public string Su_Email { get; set; }//电子邮箱

        [StringLength(50)]
        public string Su_Title { get; set; }//职称

        [Required]
        public bool Su_Enabled { get; set; }

        [Required, StringLength(8)]
        public string Su_ActiveDate { get; set; }//生效

        [Required, StringLength(8)]
        public string Su_ExpiryDate { get; set; }//失效

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