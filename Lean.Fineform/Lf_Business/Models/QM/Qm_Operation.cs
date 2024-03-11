using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fine.Lf_Business.Models.QM
{
    public class Qm_Operation : IKeyGUID
    {
        //部品・製品品質保証業務
        [Key]
        public Guid GUID { get; set; }
        [Required, StringLength(6)]
        public string Qcod001 { get; set; }//日期

        public Decimal Qcod002 { get; set; }//	间接人員赁率

        public Decimal Qcod003 { get; set; }//	受入检查业务费用555
        public Int64 Qcod004 { get; set; }//	检查时间(分)
        public Decimal Qcod005 { get; set; }//	交通费、旅费
        public Decimal Qcod006 { get; set; }//	检查其他费用
        [StringLength(500)]
        public string Qcod007 { get; set; }//	检查备注

        public Decimal Qcod008 { get; set; }//First Article Qualification /初期检定?定期检定业务费用555
        public Int64 Qcod009 { get; set; }//	检定作业时间(分)
        public Decimal Qcod010 { get; set; }//	检定其他费用
        [StringLength(500)]
        public string Qcod011 { get; set; }//	检定备注

        public Decimal Qcod012 { get; set; }//	测定器校正业务费用555
        public Int64 Qcod013 { get; set; }//	校正作业时间(分)
        public Decimal Qcod014 { get; set; }//	外部委托费、运搬费
        public Decimal Qcod015 { get; set; }//	校正其他费用
        [StringLength(500)]
        public string Qcod016 { get; set; }//	校正备注

        public Decimal Qcod017 { get; set; }//	其他通常业务费用555
        public Int64 Qcod018 { get; set; }//	通常业务作业时间(分)
        public Decimal Qcod019 { get; set; }//	通常业务其他费用
        [StringLength(500)]
        public string Qcod020 { get; set; }//	通常业务其他备注

        [StringLength(50)]
        public string Qcodqcr { get; set; }     //	IQC品质问题対応记录者
        public Decimal Qcod021 { get; set; }//	出荷检查业务费用
        public Int64 Qcod022 { get; set; }//	检查时间(分)
        public Decimal Qcod023 { get; set; }//	检查其他费用
        [StringLength(500)]
        public string Qcod024 { get; set; }//	检查备注


        public Decimal Qcod025 { get; set; }//	信赖性评价・ORT业务费用555
        public int Qcod026 { get; set; }//	评价作业时间(分)
        public Decimal Qcod027 { get; set; }//	评价其他费用
        [StringLength(500)]
        public string Qcod028 { get; set; }//	评价备注

        public Decimal Qcod029 { get; set; }//	顾客品质要求对应业务费用555
        public Int64 Qcod030 { get; set; }//	评价作业时间(分)
        public Decimal Qcod031 { get; set; }//	评价其他费用
        [StringLength(500)]
        public string Qcod032 { get; set; }//	评价备注

        public Decimal Qcod033 { get; set; }//	其他通常业务费用555
        public Int64 Qcod034 { get; set; }//	通常业务作业时间(分)
        public Decimal Qcod035 { get; set; }//	通常业务其他费用
        [StringLength(500)]
        public string Qcod036 { get; set; }//	通常业务其他备注

        [StringLength(50)]
        public string Qcodqar { get; set; }     //	QA品质问题対応记录者
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