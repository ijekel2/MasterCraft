using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;

namespace MasterCraft.Client.Common.State
{
    public class SubmissionState
    {
        public MentorProfileVm MentorProfile { get; set; } = new();

        public FeedbackRequestVm FeedbackRequest { get; set; } = new();
    }
}
