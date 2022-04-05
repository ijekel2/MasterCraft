using System.ComponentModel.DataAnnotations;

namespace MasterCraft.Domain.Entities
{
    public class Mentor : BaseEntity
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string ChannelLink { get; set; }
        public string ChannelName { get; set; }
        public string PersonalTitle { get; set; }
        public string ProfileCustomUri { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
