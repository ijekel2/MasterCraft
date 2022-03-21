using System.ComponentModel.DataAnnotations;

namespace MasterCraft.Shared.Entities
{
    public class MentorProfile : BaseEntity
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [Required]
        public string ChannelLink { get; set; }

        [Required]
        public string ChannelName { get; set; }

        [Required]
        public string PersonalTitle { get; set; }

        [Required]
        public string ProfileCustomUri { get; set; }

        public string WelcomeVideoUrl { get; set; }

        public string ProfileImageUrl { get; set; }

        public string PayPalAccountId { get; set; }
    }
}
