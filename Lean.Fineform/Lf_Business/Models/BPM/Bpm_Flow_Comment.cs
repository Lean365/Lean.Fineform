using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Bpm_Flow_Comment : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required,] 
        public string MemberID{ get; set; }
        [Required, StringLength(500)]
        public string Comment { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public int Sort { get; set; }
    }
}