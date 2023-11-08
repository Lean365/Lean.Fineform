using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lean.Fineform
{
    public class Bpm_Flow_Delegation : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required, StringLength(40)]
        public string UserID { get; set; }
        [Required,]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public Guid FlowID { get; set; }
        [Required, StringLength(40)]
        public string ToUserID { get; set; }
        [Required]
        public DateTime WriteTime { get; set; }
        [Required, StringLength(4000)]
        public string Note { get; set; }
    }
}