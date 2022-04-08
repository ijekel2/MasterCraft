using MasterCraft.Client.Common.Api;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MasterCraft.Server.IntegrationTests.TestConstants;

namespace MasterCraft.Server.IntegrationTests.Offerings
{
    public class ListOfferingsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnListOfOfferingsForMentor()
        {
            Mentor mentor = TestMentor;
            await SeedDatabase(mentor);

            Offering offering = TestOffering;
            offering.MentorId = TestMentor.Id;
            await SeedDatabase(offering);

            TestResponse<List<Offering>> response = await TestApi.GetAsync<List<Offering>>($"offerings?mentorid={mentor.Id}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.IsTrue(response.Response.Count == 1);
        }
    }
}
