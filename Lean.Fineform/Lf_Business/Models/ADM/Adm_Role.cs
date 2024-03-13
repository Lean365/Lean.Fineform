using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeanFine
{
    public class Adm_Role : IKeyID
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        public virtual ICollection<Adm_User> Users { get; set; }

        public virtual ICollection<Adm_Power> Powers { get; set; }
    }
}