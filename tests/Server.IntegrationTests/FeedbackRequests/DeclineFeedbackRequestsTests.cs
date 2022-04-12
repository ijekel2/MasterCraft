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
    public class DeclineFeedbackRequestsTests : TestBase
    {
        [Test]
        public async Task ShouldChangeFeedbackRequestStatusToDeclined()
        {
            FeedbackRequest testRequest = await SeedHelper.SeedTestFeedbackRequest();

            TestResponse<EmptyVm> response = await TestApi.PostJsonAsync<DeclineFeedbackRequestVm, EmptyVm>(
                $"feedbackrequests/{testRequest.MentorId}/decline", 
                new DeclineFeedbackRequestVm());

            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);

            //-- Select record and validate.
            FeedbackRequest feedbackRequest = await AppDbContext.FeedbackRequests.FirstOrDefaultAsync(request => request.Id == testRequest.Id);
            Assert.IsNotNull(feedbackRequest);
            Assert.AreEqual(feedbackRequest.Status, FeedbackRequestStatus.Declined);
        }
    }
}
