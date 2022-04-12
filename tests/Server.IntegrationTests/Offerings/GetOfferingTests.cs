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
            Mentor mentor = TestConstants.TestMentor;
            Offering offering = TestConstants.TestOffering;

            await SeedDatabase(mentor);

            offering.MentorId = mentor.Id;
            await SeedDatabase(offering);

            TestResponse<OfferingVm> response = await TestApi.GetAsync<OfferingVm>($"offerings/{offering.Id}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
        }
    }
}
