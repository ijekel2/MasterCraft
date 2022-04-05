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
            await SeedDatabase(TestConstants.TestMentor);

            TestResponse<Mentor> response = await TestApi.GetAsync<Mentor>("mentors/1");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.AreEqual(1, response.Response.Id);
        }
    }
}
