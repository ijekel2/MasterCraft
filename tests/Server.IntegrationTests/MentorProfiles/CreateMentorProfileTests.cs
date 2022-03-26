using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using MasterCraft.Shared.Requests;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MasterCraft.Server.IntegrationTests.MentorProfiles
{
    public class CreateOfferingTests : TestBase
    {
        [Test]
        public async Task ShouldSaveMentorProfileAndPicture()
        {
            CreateMentorProfileRequest request = new()
            {
                ChannelName = TestConstants.TestMentorProfile.ChannelName,
                ChannelLink = TestConstants.TestMentorProfile.ChannelLink,
                PersonalTitle = TestConstants.TestMentorProfile.PersonalTitle,
                ProfileCustomUri = TestConstants.TestMentorProfile.ProfileCustomUri
            };

            //-- Send create mentor profile request and validate the response.
            TestResponse<int> response = await TestApi.PostFormAsync<CreateMentorProfileRequest, int>(
                "mentorprofiles",
                request,
                new() { TestConstants.TestImage });

            Assert.IsTrue(response.Success);
            Assert.IsFalse(response.Response == 0);


            //-- Select record and validate.
            MentorProfile profile = await AppDbContext.MentorProfiles.FirstOrDefaultAsync(profile => profile.Id == response.Response);
            Assert.IsNotNull(profile);
            Assert.IsNotEmpty(profile.ProfileImageUrl);

            //-- Read image from storage and validate.
            using FileStream stream = FileStorage.OpenRead(profile.ProfileImageUrl);
            using MemoryStream memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            Assert.IsNotNull(fileBytes);
            Assert.IsTrue(fileBytes.Length > 1);

        }
    }
}
