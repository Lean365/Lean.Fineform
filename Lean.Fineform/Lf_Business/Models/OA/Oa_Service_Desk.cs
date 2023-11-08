using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Oa_Service_Desk : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required, StringLength(40)]
        public string Fault_Location { get; set; }//申报信息：故障区域
        [Required, StringLength(40)]
        public string Declaration_Date  { get; set; }//申报信息：申报日期
        [Required, StringLength(40)]
        public string Maintenance_Category { get; set; }//申报信息：维护类别

        [Required, StringLength(40)]
        public string Priority_Level { get; set; }//申报信息：优先级别
        [Required, StringLength(40)]
        public string Maintenance_Contact { get; set; }//申报信息：联系人

        [ StringLength(40)]
        public string Maintenance_Ext { get; set; }//申报信息：机
        [ StringLength(40)]
        public string Maintenance_Phone { get; set; }//申报信息：电话
        [Required,StringLength(2000)]
        public string Fault_Description { get; set; }//申报信息：故障描述
        [StringLength(100)]
        public string Fault_Aattachment { get; set; }//申报信息：附件

        [StringLength(200)]
        public string Equipment_Number { get; set; }//申报信息：设备编号
        [Required, StringLength(40)]
        public string Fees_Category { get; set; }//申报信息：收费类别
        [Required, StringLength(2000)]
        public string Maintenance_Company { get; set; }//维修信息：维修公司
        [Required, StringLength(40)]
        public string Maintenance_Personnel { get; set; }//维修信息：维修人员

        [Required, StringLength(8)]
        public string Maintenance_Date { get; set; }//维修信息：维修日期


        [Required, StringLength(2000)]
        public string Maintenance_Instructions { get; set; }//维修信息：维修说明
        [Required]
        public int Spend_Time { get; set; }//维修信息：花费时间

        [Required,]
        public decimal Required_Cost { get; set; }//维修信息：收费

        [Required]
        public byte Service_Status { get; set; }//维修信息：状态



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