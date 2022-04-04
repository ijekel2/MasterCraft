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

        public int OfferingId { get; set; }

        public int MentorId { get; set; }

        public int LearnerId { get; set; }

        public string ContentLink { get; set; }

        public FeedbackRequestStatus Status { get; set; }

        public DateTime? CompletedDate { get; set; }
    }
}
