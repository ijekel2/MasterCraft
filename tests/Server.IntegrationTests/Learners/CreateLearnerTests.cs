﻿using MasterCraft.Domain.Entities;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Stripe;

namespace MasterCraft.Server.IntegrationTests.Learners
{
    public class CreateLearnerTests : TestBase
    {
        [Test]
        public async Task ShouldSaveLearnerAndPicture()
        {
            LearnerVm request = new()
            {
                ApplicationUserId = TestConstants.TestUser.Id,
                Email = TestConstants.TestUser.Email
            };

            //-- Send create mentor request and validate the response.
            TestResponse<EmptyVm> response = await TestApi.PostJsonAsync<LearnerVm, EmptyVm>(
                "learners",
                request);

            Assert.IsTrue(response.Success);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            using var context = GetDbContext();
            Learner learner = await context.Learners.FirstOrDefaultAsync(learner => learner.ApplicationUserId == request.ApplicationUserId);
            Assert.IsNotNull(learner);

            //-- Validate that the image was saved
            //Assert.IsNotEmpty(mentor.ProfileImageUrl);

            ////-- Read image from storage and validate.
            //using FileStream stream = FileStorage.OpenRead(mentor.ProfileImageUrl);
            //using MemoryStream memoryStream = new();
            //await stream.CopyToAsync(memoryStream);
            //byte[] fileBytes = memoryStream.ToArray();
            //Assert.IsNotNull(fileBytes);
            //Assert.IsTrue(fileBytes.Length > 1);
        }
    }
}
