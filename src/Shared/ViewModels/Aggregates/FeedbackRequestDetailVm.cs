using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels.Aggregates
{
    public class FeedbackRequestDetailVm
    {
        public FeedbackRequestVm FeedbackRequest { get; set; } = new();

        public OfferingVm Offering { get; set; } = new();

        public UserVm Learner { get; set; } = new();
    }
}
