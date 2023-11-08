using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Em_Event_Type : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [StringLength(4)]
        public string atcntype { get; set; }//	事件类型。

        [StringLength(4)]
        public string atcntypename { get; set; }//	类型名称。

        [StringLength(500)]
        public string Remark { get; set; }

        [StringLength(50)]
        public string CreateUser { get; set; }
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string ModiUser { get; set; }
        public DateTime? ModiTime { get; set; }



    }
}