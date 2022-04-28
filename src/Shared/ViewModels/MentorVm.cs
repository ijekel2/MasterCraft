using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class MentorVm
    {
        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        [MaxLength(1024)]
        public string ChannelLink { get; set; }

        [Required]
        [MaxLength(64)]
        public string ChannelName { get; set; }

        [Required]
        [MaxLength(64)]
        public string PersonalTitle { get; set; }

        [Required]
        [MaxLength(64)]
        public string ProfileCustomUri { get; set; }

        [MaxLength(256)]
        public string ProfileImageUrl { get; set; }
    }
}
