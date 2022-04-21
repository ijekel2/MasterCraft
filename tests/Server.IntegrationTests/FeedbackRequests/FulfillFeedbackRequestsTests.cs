using MasterCraft.Client.Common.Api;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MasterCraft.Server.IntegrationTests.TestConstants;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using MasterCraft.Shared.Enums;

namespace MasterCraft.Server.IntegrationTests.FeedbackRequests
{
    public class FulfillFeedbackRequestsTests : TestBase
    {
        [Test]
        public async Task ShouldChangeFeedbackRequestStatusToFulfilled()
        {
            FeedbackRequest testRequest = await SeedHelper.SeedTestFeedbackRequest();

            TestResponse<EmptyVm> response = await TestApi.PostJsonAsync<FulfillFeedbackRequestVm, EmptyVm>(
                $"feedbackrequests/{testRequest.Id}/fulfill", 
                new FulfillFeedbackRequestVm()
                {
                    MentorId = testRequest.MentorId,
                    LearnerId = testRequest.LearnerId,
                    FeedbackRequestId = testRequest.Id,
                    VideoUrl = "test url"
                });

            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);

            //-- Select record and validate.
            using var context = GetDbContext();
            FeedbackRequest feedbackRequest = await context.FeedbackRequests.FirstOrDefaultAsync(request => request.Id == testRequest.Id);
            Assert.IsNotNull(feedbackRequest);
            Assert.AreEqual(FeedbackRequestStatus.Fulfilled, feedbackRequest.Status);
        }

    }
}
