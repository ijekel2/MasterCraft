using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasterCraft.Domain.Entities
{
    public class Mentor : BaseEntity
    {
        public string UserId { get; set; }
        public string ProfileId { get; set; }
        public string VideoEmbedUrl { get; set; }
        public string StripeAccountId { get; set; }
        public string SampleQuestion1 { get; set; }
        public string SampleQuestion2 { get; set; }
        public string SampleQuestion3 { get; set; }
        public string SampleQuestion4 { get; set; }
        public string SampleQuestion5 { get; set; }
        public bool Active { get; set; }

        public virtual User User { get; set; }

        public virtual List<Offering> Offerings { get; set; }
    }
}
