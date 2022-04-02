using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Domain.Entities;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MasterCraft.Server.IntegrationTests.Mentors
{
    public class CreateMentorTests : TestBase
    {
        [Test]
        public async Task ShouldSaveMentorAndPicture()
        {
            MentorViewModel request = new()
            {
                ChannelName = TestConstants.TestMentor.ChannelName,
                ChannelLink = TestConstants.TestMentor.ChannelLink,
                PersonalTitle = TestConstants.TestMentor.PersonalTitle,
                ProfileCustomUri = TestConstants.TestMentor.ProfileCustomUri
            };

            //-- Send create mentor request and validate the response.
            TestResponse<Empty> response = await TestApi.PostFormAsync<MentorViewModel, Empty>(
                "mentors",
                request,
                new() { TestConstants.TestImage });

            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Response);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Headers.Location);
            Assert.IsTrue(int.TryParse(response.Headers.Location.Last().ToString(), out int id));
            Mentor mentor = await AppDbContext.Mentors.FirstOrDefaultAsync(mentor => mentor.Id == id);
            Assert.IsNotNull(mentor);
            Assert.IsNotEmpty(mentor.ProfileImageUrl);

            //-- Read image from storage and validate.
            using FileStream stream = FileStorage.OpenRead(mentor.ProfileImageUrl);
            using MemoryStream memoryStream = new();
            await stream.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            Assert.IsNotNull(fileBytes);
            Assert.IsTrue(fileBytes.Length > 1);

        }
    }
}
