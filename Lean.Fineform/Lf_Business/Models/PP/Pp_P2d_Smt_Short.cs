using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine.Lf_Business.Models.PP
{
    public class Pp_P2d_Smt_Short : IKeyGUID
    {
        //SMT点数
        [Required, Key]
        public Guid GUID { get; set; }

        [Required, StringLength(4)]
        public string Proplnt { get; set; }//工厂

        [Required, StringLength(40)]
        public string Promodel { get; set; }//机种类别

        [StringLength(20)]
        public string Proitem { get; set; }//完成品物料

        [StringLength(40)]
        public string Proitemtext { get; set; }//物料文本

        [StringLength(20)]
        public string Propcbitem { get; set; }//SAP物料

        [StringLength(40)]
        public string Propcbtext { get; set; }//SAP物料文本

        public int Propcbshort { get; set; }//SAP点数

        [Required, StringLength(20)]
        public string Propcba { get; set; }//PCB品号

        [StringLength(40)]
        public string Propcbatext { get; set; }//PCB品名

        [Required]
        public int Proconvertshort { get; set; } //折算后的点数

        [Required]
        public int Promachineshort { get; set; }//机器点数

        public int Promanualshort { get; set; }//手贴点数

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