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
using MasterCraft.Shared.ViewModels;

namespace MasterCraft.Server.IntegrationTests.Offerings
{
    public class ListOfferingsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnListOfOfferingsForMentor()
        {
            Offering offering = await SeedHelper.SeedTestOffering();

            TestResponse<List<OfferingVm>> response = await TestApi.GetAsync<List<OfferingVm>>($"offerings?mentorid={offering.MentorId}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.IsTrue(response.Response.Count == 1);
        }
    }
}
