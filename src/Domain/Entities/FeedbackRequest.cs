using MasterCraft.Domain.Interfaces;
using MasterCraft.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Entities
{
    public class FeedbackRequest : BaseEntity
    {
        public int Id { get; set; }
        public string ContentLink { get; set; }
        public FeedbackRequestStatus Status { get; set; }
        public DateTime? FinalizedDate { get; set; }

        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }

        public int LearnerId { get; set; }
        public Learner Learner { get; set; }

        public int OfferingId { get; set; }
        public Offering Offering { get; set; }
    }
}
