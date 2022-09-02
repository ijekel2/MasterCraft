using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels.Aggregates
{
    public class MentorProfileVm
    {
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string ProfileId { get; set; } = string.Empty;

        public string StripeAccountId { get; set; } = string.Empty;

        public string VideoEmbedCode { get; set; } = string.Empty;

        public string VideoEmbedUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(512)]
        public string SampleQuestion1 { get; set; } = string.Empty;

        [Required]
        [MaxLength(512)]
        public string SampleQuestion2 { get; set; } = string.Empty;

        [Required]
        [MaxLength(512)]
        public string SampleQuestion3 { get; set; } = string.Empty;

        [MaxLength(512)]
        public string SampleQuestion4 { get; set; } = string.Empty;

        [MaxLength(512)]
        public string SampleQuestion5 { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(256)]
        public string ProfileImageUrl { get; set; } = string.Empty;

        public IEnumerable<OfferingVm> Offerings { get; set; } = new List<OfferingVm>();
    }
}
