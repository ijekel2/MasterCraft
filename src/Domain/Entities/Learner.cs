using System.ComponentModel.DataAnnotations;

namespace MasterCraft.Domain.Entities
{
    public class Learner : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
