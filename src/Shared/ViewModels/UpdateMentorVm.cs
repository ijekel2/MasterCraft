using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class UpdateMentorVm
    {
        [MaxLength(1024)]
        public string ChannelLink { get; set; }

        [MaxLength(64)]
        public string ChannelName { get; set; }

        [MaxLength(64)]
        public string PersonalTitle { get; set; }

        [MaxLength(64)]
        public string ProfileCustomUri { get; set; }

        [MaxLength(256)]
        public string ProfileImageUrl { get; set; }

        public string VideoEmbedCode { get; set; }

        public bool Active { get; set; }
    }
}
