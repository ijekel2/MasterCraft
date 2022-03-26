using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using MasterCraft.Shared.Requests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MasterCraft.Server.IntegrationTests.Offerings
{
    public class CreateOfferingTests : TestBase
    {
        [Test]
        public async Task ShouldSaveOffering()
        {
            CreateOfferingRequest request = new()
            {
                //ChannelName = TestConstants.TestMentorProfile.ChannelName,
                //ChannelLink = TestConstants.TestMentorProfile.ChannelLink,
                //PersonalTitle = TestConstants.TestMentorProfile.PersonalTitle,
                //ProfileCustomUri = TestConstants.TestMentorProfile.ProfileCustomUri
            };

            //-- Send create mentor profile request and validate the response.
            TestResponse<int> response = await TestApi.PostJsonAsync<CreateOfferingRequest, int>(
                "offerings",
                request);

            Assert.IsTrue(response.Success);
            Assert.IsFalse(response.Response == 0);


            //-- Select record and validate.
            Offering offering = AppDbContext.Offerings.FirstOrDefault(offering => offering.Id == response.Response);
            Assert.IsNotNull(offering);
        }
    }
}
