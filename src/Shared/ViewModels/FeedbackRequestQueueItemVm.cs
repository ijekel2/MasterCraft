using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class FeedbackRequestQueueItemVm
    {
        public string FeedbackRequestId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImageUrl { get; set; }

        public DateTime SubmissionDate { get; set; }

        public decimal Price { get; set; }
    }
}
