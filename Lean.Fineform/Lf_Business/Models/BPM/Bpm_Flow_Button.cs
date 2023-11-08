using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lean.Fineform
{
    public class Bpm_Flow_Button : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required, StringLength(500)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Ico { get; set; }

        public string Script { get; set; }

        public string Note { get; set; }
        [Required]
        public int Sort { get; set; }
    }
}