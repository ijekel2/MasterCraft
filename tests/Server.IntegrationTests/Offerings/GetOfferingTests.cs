using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Offerings
{
    public class GetOfferingTests : TestBase
    {
        [Test]
        public async Task ShouldReturnOfferingForId()
        {
            await SeedDatabase(TestConstants.TestOffering);

            TestResponse<Mentor> response = await TestApi.GetAsync<Mentor>("offerings/1");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.AreEqual(1, response.Response.Id);
        }
    }
}
