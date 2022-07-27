using MasterCraft.Domain.Interfaces;
using MasterCraft.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Entities
{
    public class Video : BaseEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public VideoType VideoType { get; set; }

        public string MentorId { get; set; }
        public Mentor Mentor { get; set; }

        public string LearnerId { get; set; }
        public User Learner { get; set; }

        public string FeedbackRequestId { get; set; }
        public FeedbackRequest FeedbackRequest { get; set; }

        public string GetStreamUrl(IFileStorage fileStorage, IStreamingService streamingService)
        {
            throw new NotImplementedException();
        }

    }
}
