using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine
{
    //词典
    public class Adm_Dict : IKeyGUID
    {
        //数据字典
        //
        [Key]
        public Guid GUID { get; set; }

        [StringLength(100)]
        public string DictType { get; set; }//字典类型

        [StringLength(200)]
        public string DictName { get; set; }//字典名称

        [StringLength(100)]
        public string DictLabel { get; set; }//数据标签

        [StringLength(100)]
        public string DictValue { get; set; }//数据值

        public int DictSort { get; set; }//排序

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
        public byte IsDeleted { get; set; }	//13	//	删除标记

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