using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.FeedbackRequests
{
    public class GetFeedbackRequestTests : TestBase
    {
        [Test]
        public async Task ShouldReturnFeedbackRequestForId()
        {
            FeedbackRequest request = await SeedHelper.SeedTestFeedbackRequest();

            TestResponse<FeedbackRequest> response = await TestApi.GetAsync<FeedbackRequest>($"feedbackrequests/{request.Id}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.AreEqual(1, response.Response.Id);
        }
    }
}
