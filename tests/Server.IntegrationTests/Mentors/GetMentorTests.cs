using MasterCraft.Domain.Entities;
using MasterCraft.Server.IntegrationTests.Api;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Mentors
{
    public class GetMentorTests : TestBase
    {
        [Test]
        public async Task ShouldReturnMentorForId()
        {
            Mentor mentor = await SeedHelper.SeedTestMentor();

            TestResponse<Mentor> response = await TestApi.GetAsync<Mentor>($"mentors/{mentor.ApplicationUserId}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.AreEqual(1, response.Response.ApplicationUserId);
        }
    }
}
