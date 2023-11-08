using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lean.Fineform
{
    public class Bpm_Flow_Task : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required]
        public Guid PrevID { get; set; }
        [Required]
        public Guid PrevStepID { get; set; }
        [Required]
        public Guid FlowID { get; set; }
        [Required]
        public Guid StepID { get; set; }
        [Required,StringLength(500)]
        public string StepName { get; set; }
        [Required,StringLength(50)]
        public string InstanceID { get; set; }
        [Required]
        public Guid GroupID { get; set; }
        [Required]
        public int Type { get; set; }

        [Required,StringLength(500)]
        public string Title { get; set; }
        [Required, StringLength(40)]
        public string SenderID { get; set; }
        [Required,StringLength(50)]
        public string SenderName { get; set; }
        [Required]
        public DateTime SenderTime { get; set; }
        [Required]
        public Guid ReceiveID { get; set; }
        [Required, StringLength(50)]
        public string ReceiveName { get; set; }
        [Required, ]
        public DateTime ReceiveTime { get; set; }

        public DateTime OpenTime { get; set; }

        public DateTime CompletedTime { get; set; }

        public DateTime CompletedTime1 { get; set; }
        [ StringLength(4000)]
        public string Comment { get; set; }

        public int IsSign { get; set; }
        [Required]
        public int Status { get; set; }

        public string Note { get; set; }
        [Required]
        public int Sort { get; set; }

        public string SubFlowGroupID { get; set; }

        public int OtherType { get; set; }

        public string Files { get; set; }
        [Required]
        public int IsExpiredAutoSubmit { get; set; }
        [Required]
        public int StepSort { get; set; }
    }
}