using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.Requests
{
    public class CreateMentorProfileRequest
    {
        [Required]
        public string ChannelLink { get; set; }

        [Required]
        public string ChannelName { get; set; }

        [Required]
        public string PersonalTitle { get; set; }

        [Required]
        public string ProfileCustomUri { get; set; }
    }
}
