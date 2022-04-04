using System.ComponentModel.DataAnnotations;

namespace MasterCraft.Domain.Entities
{
    public class Learner : BaseEntity
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}
