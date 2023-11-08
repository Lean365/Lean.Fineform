using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Oa_Meeting_Room : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(20)]
        public string Room_Name { get; set; }//名称
        [Required, StringLength(20)]
        public string Room_Local { get; set; }//位置
        [Required]
        public byte Room_Flag { get; set; }//状态
        [Required]
        public byte Room_Project { get; set; }//设施投影机
        [Required]
        public byte Room_Telephone { get; set; }//设施设备电话
        [Required]
        public byte Room_Wifi { get; set; }//设施设备无线
        [Required]
        public byte Room_Video { get; set; }//设施设备视频
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