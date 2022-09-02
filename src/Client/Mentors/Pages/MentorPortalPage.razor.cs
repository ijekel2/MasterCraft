using MasterCraft.Client.Common;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Client.Shared.Components;
using MasterCraft.Client.Shared.Models;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Mentors.Pages
{
    public partial class MentorPortalPage : ComponentBase
    {
        [Inject] public ApiClient ApiClient { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public CurrentUserService UserService { get; set; }
        [CascadingParameter] public ErrorHandler Error { get; set; }

        private UserVm cCurrentUser  = new();
        private IEnumerable<FeedbackRequestQueueItemVm> cFeedbackRequests = Enumerable.Empty<FeedbackRequestQueueItemVm>();
        private EarningsSummaryVm cEarningsSummary = new();
        private FeedbackRequestDetailVm cRequestDetail = new();
        private bool cInRecordingMode = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                cCurrentUser = await UserService.GetCurrentUser();

                //-- Request queue
                ApiResponse<List<FeedbackRequestQueueItemVm>> queueResponse =
                    await ApiClient.GetAsync<List<FeedbackRequestQueueItemVm>>($"mentors/{cCurrentUser.Id}/getRequestQueue?pagesize=50");

                if (queueResponse.Success)
                {
                    cFeedbackRequests = queueResponse.Response;
                }

                //-- Request detail
                await OnQueueItemClick(cFeedbackRequests.FirstOrDefault());

                //-- Earnings summary
                ApiResponse<EarningsSummaryVm> summaryResponse =
                    await ApiClient.GetAsync<EarningsSummaryVm>($"mentors/{cCurrentUser.Id}/getEarningsSummary");

                if (summaryResponse.Success)
                {
                    cEarningsSummary = summaryResponse.Response;
                }

                //-- For some reason the toolbar wants this.
                StateHasChanged();
            }
            catch (Exception ex)
            {
                await Error.ProcessError(ex);
            }
        }

        private async Task OnQueueItemClick(FeedbackRequestQueueItemVm queueItem)
        {
            if (queueItem != null)
            {
               ApiResponse<FeedbackRequestDetailVm> detailResponse =
                    await ApiClient.GetAsync<FeedbackRequestDetailVm>($"feedbackRequests/{queueItem.FeedbackRequestId}/getDetail");

                if (detailResponse.Success)
                {
                    cRequestDetail = detailResponse.Response;
                } 
            }
        }

        private async Task OnSubmitFeedbackClick(LoomVideo video, string html)
        {
            FulfillFeedbackRequestVm fulfillRequest = new FulfillFeedbackRequestVm()
            {
                FeedbackRequestId = cRequestDetail.FeedbackRequest.Id,
                MentorId = cRequestDetail.FeedbackRequest.MentorId,
                LearnerId = cRequestDetail.FeedbackRequest.LearnerId,
                VideoUrl = video.sharedUrl,
            };

            ApiResponse<EmptyVm> fulfillResponse =
                    await ApiClient.PostAsync<FulfillFeedbackRequestVm, EmptyVm>($"feedbackRequests/fulfill", fulfillRequest);

            if (fulfillResponse.Success)
            {
                Navigation.NavigateTo($"/feedback/{cRequestDetail.FeedbackRequest.Id}");
            }
        }

        private string GetSubmitterName(FeedbackRequestQueueItemVm item)
        {
            return $"{item.FirstName} {item.LastName}";
        }

        private string GetTimeSinceSubmission(FeedbackRequestQueueItemVm item)
        {
            TimeSpan diff = DateTime.Now - item.SubmissionDate;
            bool lHours = diff >= new TimeSpan(1, 0, 0);

            string lTimePhrase;
            if (lHours)
            {
                int hours = (int)diff.TotalHours;
                lTimePhrase = $"{Math.Min(hours, 99999)} hrs";
            }
            else
            {
                int minutes = (int)diff.TotalMinutes;
                lTimePhrase = $"{minutes} min";
            }

            return lTimePhrase;
        }

        private string GetPrice(FeedbackRequestQueueItemVm item)
        {
            return item.Price.FormatPrice();
        }

        private string GetRequestDetailCssClass()
        {
            return cInRecordingMode ? "studio-request-detail" : "queue-request-detail";
        }

        private void ExitRecordingMode()
        {
            cInRecordingMode = false;
            StateHasChanged();
        }

        private void EnterRecordingMode()
        {
            cInRecordingMode = true;
            StateHasChanged();
        }
    }
}
