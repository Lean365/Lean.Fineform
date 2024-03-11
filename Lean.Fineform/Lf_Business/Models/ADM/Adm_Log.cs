using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fine
{
    public class Adm_Log : IKeyID
    {
        [Key]
        public int ID { get; set; }


        public DateTime Date { get; set; }
        [StringLength(500)]
        public string Thread { get; set; }
        [StringLength(50)]
        public string Level { get; set; }

        [StringLength(500)]
        public string Logger { get; set; }
        [StringLength(20)]
        public string UserID { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(4000)]
        public string Message { get; set; }

        [StringLength(2000)]
        public string Exception { get; set; }

  

    }
}