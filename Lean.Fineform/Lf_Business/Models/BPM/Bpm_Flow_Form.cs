using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lean.Fineform
{
    public class Bpm_Flow_Form : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required, StringLength(500)]
        public string Name { get; set; }
        [Required]
        public Guid Type { get; set; }
        [Required,StringLength(20)]
        public string CreateUserID { get; set; }

        [Required, StringLength(50)]
        public string CreateUserName { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        [Required]
        public DateTime LastModifyTime { get; set; }
        [Required, ]
        public string Html { get; set; }
        [Required,]
        public  string SubTableJson { get; set; }
        [Required, ]
        public string EventsJson { get; set; }
        [Required,]
        public string Attribute { get; set; }
        [Required]
        public int Status { get; set; }
    }
}