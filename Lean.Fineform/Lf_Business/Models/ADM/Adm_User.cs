using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeanFine
{
    public class Adm_User : IKeyID
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

        [Required]
        public bool Enabled { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(100)]
        public string ChineseName { get; set; }

        [StringLength(100)]
        public string EnglishName { get; set; }

        [StringLength(200)]
        public string Photo { get; set; }

        [StringLength(50)]
        public string QQ { get; set; }

        [StringLength(100)]
        public string CompanyEmail { get; set; }

        [StringLength(50)]
        public string OfficePhone { get; set; }

        [StringLength(50)]
        public string OfficePhoneExt { get; set; }

        [StringLength(50)]
        public string HomePhone { get; set; }

        [StringLength(50)]
        public string CellPhone { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        [StringLength(50)]
        public string IdentityCard { get; set; }

        public DateTime? Birthday { get; set; }
        public DateTime? TakeOfficeTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<Adm_Role> Roles { get; set; }
        public virtual ICollection<Adm_Title> Titles { get; set; }

        public virtual Adm_Dept Dept { get; set; }
    }
}