using MasterCraft.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class FeedbackRequestVm
    {
        [Required]
        public string MentorId { get; set; }

        [Required]
        public string LearnerId { get; set; }

        [Range(1, int.MaxValue)]
        public int OfferingId { get; set; }

        [MaxLength(256)]
        public string ContentLink { get; set; }

        public FeedbackRequestStatus Status { get; set; }

        public DateTime SubmissionDate { get; set; }

        public DateTime? ResponseDate { get; set; }



    }
}
