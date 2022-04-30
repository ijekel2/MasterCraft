using MasterCraft.Domain.Entities;
using MasterCraft.Server.IntegrationTests.Api;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Learners
{
    public class GetLearnerTests : TestBase
    {
        [Test]
        public async Task ShouldReturnMentorForId()
        {
            Learner learner = await SeedHelper.SeedTestLeaner();

            TestResponse<Mentor> response = await TestApi.GetAsync<Mentor>($"learners/{learner.ApplicationUserId}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.AreEqual(1, response.Response.ApplicationUserId);
        }
    }
}
