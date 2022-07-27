using MasterCraft.Domain.Entities;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static MasterCraft.Server.IntegrationTests.TestConstants;

namespace MasterCraft.Server.IntegrationTests.FeedbackRequests
{
    public class CreateFeedbackRequestTests : TestBase
    {
        [Test]
        public async Task ShouldSaveFeedbackRequest()
        {
            Offering offering = await SeedHelper.SeedTestOffering();

            FeedbackRequestVm request = new()
            {
                Status = TestFeedbackRequest.Status,
                VideoEmbedCode = TestFeedbackRequest.VideoEmbedCode,
                MentorId = offering.Mentor.UserId,
                OfferingId = offering.Id
            };

            //-- Send create mentor request and validate the response.
            TestResponse<FeedbackRequestCreatedVm> response = await TestApi.PostJsonAsync<FeedbackRequestVm, FeedbackRequestCreatedVm>(
                "feedbackrequests",
                request);

            Assert.IsTrue(response.Success);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            //-- Select record and validate.
            using var context = GetDbContext();
            FeedbackRequest feedbackRequest = await context.FeedbackRequests.FirstOrDefaultAsync(request => request.Id == response.Response.FeedbackRequestId);
            Assert.IsNotNull(feedbackRequest);
        }
    }
}
