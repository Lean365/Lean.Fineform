using System;
using System.ComponentModel.DataAnnotations;

namespace LeanFine
{
    public class Adm_Online : IKeyID
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string IPAdddress { get; set; }

        public DateTime LoginTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int LoginNum { get; set; }

        public virtual Adm_User User { get; set; }
    }
}