using MasterCraft.Client.Common.Api;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.MentorProfiles
{
    public class GetOfferingTests : TestBase
    {
        [Test]
        public async Task ShouldReturnMentorProfileForId()
        {
            await SeedDatabase(new MentorProfile()
            {
                PersonalTitle = "Tester",
                ChannelName = "The Testy Tester",
                ChannelLink = "channel.local/testytester",
                ProfileCustomUri = "https://www.mastercraft.cc/mentor/profile/testytester"
            });

            TestResponse<MentorProfile> response = await TestApi.GetAsync<MentorProfile>("mentorprofiles/1");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.AreEqual(1, response.Response.Id);
        }
    }
}
