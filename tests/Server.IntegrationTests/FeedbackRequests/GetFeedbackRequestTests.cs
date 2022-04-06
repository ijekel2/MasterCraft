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
            await SeedDatabase(TestConstants.TestFeedbackRequest);

            TestResponse<Mentor> response = await TestApi.GetAsync<Mentor>("offerings/1");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.AreEqual(1, response.Response.Id);
        }
    }
}
