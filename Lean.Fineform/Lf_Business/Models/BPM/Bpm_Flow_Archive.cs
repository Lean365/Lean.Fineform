using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lean.Fineform
{
    public class Bpm_Flow_Archive : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required]
        public Guid FlowID { get; set; }
        [Required]
        public Guid StepID { get; set; }
        [Required, StringLength(500)]
        public string FlowName { get; set; }
        [Required, StringLength(500)]
        public string StepName { get; set; }
        [Required]
        public Guid TaskID { get; set; }
        [Required]
        public Guid GroupID { get; set; }
        [Required, StringLength(500)]
        public string InstanceID { get; set; }
        [Required, StringLength(4000)]
        public string Title { get; set; }
        [Required,]
        public string Contents { get; set; }
        [Required, ]
        public string Comments { get; set; }
        [Required]
        public DateTime WriteTime { get; set; }

    }
}