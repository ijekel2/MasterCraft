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
            FeedbackRequestViewModel request = new()
            {
                Status = TestFeedbackRequest.Status,
                ContentLink = TestFeedbackRequest.ContentLink,
            };

            //-- Send create mentor request and validate the response.
            TestResponse<Empty> response = await TestApi.PostJsonAsync<FeedbackRequestViewModel, Empty>(
                "offerings",
                request);

            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Response);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Headers.Location);
            Assert.IsTrue(int.TryParse(response.Headers.Location.Last().ToString(), out int id));

            //-- Select record and validate.
            FeedbackRequest offering = await AppDbContext.FeedbackRequests.FirstOrDefaultAsync(offering => offering.Id == id);
            Assert.IsNotNull(offering);
        }
    }
}
