using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;

namespace MasterCraft.Client.Common.StateManagers
{
    public class SubmitStateManager
    {
        public MentorProfileVm MentorProfile { get; set; } = new();

        public FeedbackRequestVm FeedbackRequest { get; set; } = new();
    }
}
