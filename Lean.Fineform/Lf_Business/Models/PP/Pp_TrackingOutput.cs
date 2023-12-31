﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lean.Fineform
{
    public class Pp_TrackingOutput : IKeyGUID
    {

        [Key]
        public Guid GUID { get; set; }

        [Required, StringLength(4)]
        public string Pro_Plnt { get; set; }//工厂
        [Required, StringLength(6)]
        public string Pro_Line { get; set; }//班组
        [Required, StringLength(4)]
        public string Pro_Process { get; set; }//工序
        [Required, StringLength(4)]
        public string Pro_Station { get; set; }//站点
        [Required, StringLength(8)]
        public string Pro_Date { get; set; }//生产日期
        [Required, StringLength(20)]
        public string Pro_Item { get; set; }//机种物料
        [Required, StringLength(40)]
        public string Pro_Model { get; set; }//机种
        [Required, StringLength(10)]
        public string Pro_Region { get; set; }//仕向

        [Required, StringLength(40)]
        public string Pro_Lot { get; set; }//生产批次
        [Required, StringLength(20)]
        public string Pro_Stime { get; set; }//开始时间
        [Required, StringLength(20)]
        public string Pro_Etime { get; set; }//结束时间

        [Required,]
        public Decimal Pro_ProcessQty { get; set; }//台数
        [Required,]
        public Decimal Pro_MaxTime { get; set; }//最大
        [Required,]
        public Decimal Pro_MinTime { get; set; }//最小
        [Required,]
        public Decimal Pro_AvgTime { get; set; }//平均

        [Required,]
        public Decimal Pro_StdDev { get; set; }//标准差


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