using MasterCraft.Domain.Entities;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Stripe;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Mentors
{
    public class CreateMentorTests : TestBase
    {
        [Test]
        public async Task ShouldSaveMentorAndPicture()
        {
            MentorVm request = new()
            {
                ChannelName = TestConstants.TestMentor.ChannelName,
                ChannelLink = TestConstants.TestMentor.ChannelLink,
                PersonalTitle = TestConstants.TestMentor.PersonalTitle,
                ProfileCustomUri = TestConstants.TestMentor.ProfileCustomUri
            };

            //-- Send create mentor request and validate the response.
            TestResponse<MentorCreatedVm> response = await TestApi.PostJsonAsync<MentorVm, MentorCreatedVm> (
                "mentors",
                request);

            Assert.IsTrue(response.Success);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotEmpty(response.Response.ApplicationUserId);
            Assert.IsNotEmpty(response.Response.StripeAccountId);

            using var context = GetDbContext();
            Mentor mentor = await context.Mentors.FirstOrDefaultAsync(mentor => mentor.ApplicationUserId.ToString() == response.Response.ApplicationUserId);
            Assert.IsNotNull(mentor);

            //-- Validate that the image was saved 
            //Assert.IsNotEmpty(mentor.ProfileImageUrl);

            ////-- Read image from storage and validate.
            //using FileStream stream = FileStorage.OpenRead(mentor.ProfileImageUrl);
            //using MemoryStream memoryStream = new();
            //await stream.CopyToAsync(memoryStream);
            //byte[] fileBytes = memoryStream.ToArray();
            //Assert.IsNotNull(fileBytes);
            //Assert.IsTrue(fileBytes.Length > 1);

            //-- Validate Stripe account was created
            Assert.IsNotEmpty(mentor.StripeAccountId);
            var service = new AccountService();
            Account account = await service.GetAsync(mentor.StripeAccountId);
            Assert.IsNotNull(account);
        }
    }
}
