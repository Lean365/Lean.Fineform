﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Adm_Power : IKeyID
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(200)]
        public string NavigateUrl { get; set; }
        [StringLength(500)]
        public string Remark { get; set; }


        public virtual ICollection<Adm_Role> Roles { get; set; }

    }
}