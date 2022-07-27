using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class MentorUserVm
    {
        public string UserId { get; set; }

        [Required]
        [MaxLength(64)]
        public string ProfileId { get; set; }

        public string StripeAccountId { get; set; }

        public string VideoEmbedCode { get; set; }

        public string VideoEmbedUrl { get; set; }


        [Required]
        [MaxLength(512)]
        public string SampleQuestion1 { get; set; }

        [Required]
        [MaxLength(512)]
        public string SampleQuestion2 { get; set; }

        [Required]
        [MaxLength(512)]
        public string SampleQuestion3 { get; set; }

        [MaxLength(512)]
        public string SampleQuestion4 { get; set; }

        [MaxLength(512)]
        public string SampleQuestion5 { get; set; }

        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        public string LastName { get; set; }

        [MaxLength(256)]
        public string ProfileImageUrl { get; set; }
    }
}
