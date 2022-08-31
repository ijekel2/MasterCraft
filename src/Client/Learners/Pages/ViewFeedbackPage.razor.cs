using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Client.Mentors.Services;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MasterCraft.Client.Learners.Pages
{
    public partial class ViewFeedbackPage : ComponentBase
    {
        [Inject] public ApiClient ApiClient { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public CurrentUserService UserService { get; set; }
        [Inject] public LoomService Loom { get; set; }

        [Parameter] public string Id { get; set; }

        private UserVm cCurrentUser = new();
        private FeedbackRequestDetailVm cRequestDetail = new();
        private MarkupString VideoHtml;

        protected override async Task OnInitializedAsync()
        {
            cCurrentUser = await UserService.GetCurrentUser();

            //-- Request queue
            ApiResponse<FeedbackRequestDetailVm> detailResponse =
                    await ApiClient.GetAsync<FeedbackRequestDetailVm>($"feedbackRequests/{Id}/getDetail");

            if (detailResponse.Success)
            {
                cRequestDetail = detailResponse.Response;
            }

            VideoHtml = new MarkupString(await Loom.GetVideoHtml(cRequestDetail.FeedbackRequest.VideoEmbedUrl));

            //-- For some reason the toolbar wants this.
            StateHasChanged();
        }
    }


}
