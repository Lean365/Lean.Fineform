using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lean.Fineform
{
    public class Bpm_FLow : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required,StringLength(500)]
        public string Name { get; set; }
        [Required]
        public Guid Type { get; set; }
        [Required, StringLength(5000)]
        public string Manager { get; set; }
        [Required, StringLength(5000)]
        public string InstanceManager { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required, StringLength(40)]
        public string CreateUserID { get; set; }

        public string DesignJSON { get; set; }

        public DateTime  InstallDate { get; set; }

        public Guid InstallUserID { get; set; }

        public string RunJSON { get; set; }
        [Required]
        public int Status { get; set; }


	}
}