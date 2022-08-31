using MasterCraft.Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MasterCraft.Shared.ViewModels
{
    public class FeedbackRequestVm
    {
        public string Id { get; set; }

        [Required]
        public string MentorId { get; set; }

        [Required]
        public string LearnerId { get; set; }

        [Range(1, int.MaxValue)]
        public int OfferingId { get; set; }

        public string PaymentIntentId { get; set; }

        public string VideoEmbedCode { get; set; }

        public string VideoEmbedUrl { get; set; }

        public string Question { get; set; }

        public FeedbackRequestStatus Status { get; set; }

        public DateTime SubmissionDate { get; set; }

        public DateTime? ResponseDate { get; set; }



    }
}
