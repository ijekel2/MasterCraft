namespace MasterCraft.Core.Entities
{
    public class MentorProfile
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public string ChannelLink { get; set; }

        public string ChannelName { get; set; }

        public string PersonalTitle { get; set; }

        public string ProfileCustomUri { get; set; }

        public string WelcomeVideoUrl { get; set; }

        public string ProfileImageUrl { get; set; }

        public string PayPalAccountId { get; set; }
    }
}
