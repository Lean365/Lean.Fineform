using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_SapEcn : IKeyGUID
    {
        //设计变更SAP
        [Required, Key]
        public Guid GUID { get; set; }

        [StringLength(6)]
        public string D_SAP_ZPABD_Z001 { get; set; }//	设变号码

        [StringLength(10)]
        public string D_SAP_ZPABD_Z002 { get; set; }//	机种

        [StringLength(40)]
        public string D_SAP_ZPABD_Z003 { get; set; }//	标题

        [StringLength(40)]
        public string D_SAP_ZPABD_Z004 { get; set; }//	状态

        [StringLength(8)]
        public string D_SAP_ZPABD_Z005 { get; set; }//	发行日期

        [StringLength(20)]
        public string D_SAP_ZPABD_Z006 { get; set; }//	TCJ担当

        [StringLength(20)]
        public string D_SAP_ZPABD_Z007 { get; set; }//	TCJ依赖

        [StringLength(20)]
        public string D_SAP_ZPABD_Z008 { get; set; }//	设变会议

        [StringLength(10)]
        public string D_SAP_ZPABD_Z009 { get; set; }//	PP番号

        [StringLength(10)]
        public string D_SAP_ZPABD_Z010 { get; set; }//	技联书

        [StringLength(40)]
        public string D_SAP_ZPABD_Z011 { get; set; }//	实施

        [StringLength(40)]
        public string D_SAP_ZPABD_Z012 { get; set; }//	主变更理由

        [StringLength(40)]
        public string D_SAP_ZPABD_Z013 { get; set; }//	次变更理由

        [StringLength(40)]
        public string D_SAP_ZPABD_Z014 { get; set; }//	安规

        [StringLength(40)]
        public string D_SAP_ZPABD_Z015 { get; set; }//	进行状况

        [StringLength(40)]
        public string D_SAP_ZPABD_Z016 { get; set; }//	机番管理

        [StringLength(40)]
        public string D_SAP_ZPABD_Z017 { get; set; }//	客户承认

        [StringLength(40)]
        public string D_SAP_ZPABD_Z018 { get; set; }//	服务手册订正

        [StringLength(40)]
        public string D_SAP_ZPABD_Z019 { get; set; }//	用户手册订正

        [StringLength(40)]
        public string D_SAP_ZPABD_Z020 { get; set; }//	宣传手册订正

        [StringLength(40)]
        public string D_SAP_ZPABD_Z021 { get; set; }//	标准书订正

        [StringLength(40)]
        public string D_SAP_ZPABD_Z022 { get; set; }//	情报发行

        [StringLength(40)]
        public string D_SAP_ZPABD_Z023 { get; set; }//	成本变动

        [StringLength(40)]
        public string D_SAP_ZPABD_Z024 { get; set; }//	成本单位

        [StringLength(13)]
        public string D_SAP_ZPABD_Z025 { get; set; }//	模具改修费

        [StringLength(210)]
        public string D_SAP_ZPABD_Z026 { get; set; }//	相关图纸

        [StringLength(4000)]
        public string D_SAP_ZPABD_Z027 { get; set; }//	设变内容

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
        public byte isDeleted { get; set; }	//13	//	删除标记

        [StringLength(400)]
        public string Remark { get; set; }//备注

        [StringLength(50)]
        public string Creator { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string Modifier { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}