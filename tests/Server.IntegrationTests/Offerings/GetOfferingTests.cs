using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;
using MasterCraft.Shared.ViewModels;

namespace MasterCraft.Server.IntegrationTests.Offerings
{
    public class GetOfferingTests : TestBase
    {
        [Test]
        public async Task ShouldReturnOfferingForId()
        {
            Offering offering = await SeedHelper.SeedTestOffering();

            TestResponse<OfferingVm> response = await TestApi.GetAsync<OfferingVm>($"offerings/{offering.Id}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
        }
    }
}
