using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fine
{
    //词典
    public class Adm_TheDict : IKeyGUID
    {
        //技术电子辞典
        [Key]
        public Guid GUID { get; set; }


        [StringLength(20)]
        public string Dist { get; set; }//主字母
        [StringLength(500)]
        public string Type { get; set; }//类别
        [StringLength(500)]
        public string Words { get; set; }
        [StringLength(500)]
        public string English { get; set; }

        [StringLength(500)]
        public string Japanese { get; set; }
        [StringLength(500)]
        public string Korean { get; set; }
        [StringLength(500)]
        public string Simplified { get; set; }

        [StringLength(500)]
        public string Traditional{ get; set; }

        [StringLength(4000)]
        public string Example { get; set; }

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