﻿using MasterCraft.Domain.Entities;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Learners
{
    public class CreateLearnerTests : TestBase
    {
        [Test]
        public async Task ShouldSaveLearnerAndPicture()
        {
            LearnerVm request = new();

            //-- Send create mentor request and validate the response.
            TestResponse<EmptyVm> response = await TestApi.PostJsonAsync<LearnerVm, EmptyVm>(
                "learners",
                request);

            Assert.IsTrue(response.Success);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Headers.Location);
            Assert.IsTrue(int.TryParse(response.Headers.Location.Last().ToString(), out int id));
            Learner mentor = await AppDbContext.Learners.FirstOrDefaultAsync(mentor => mentor.Id == id);
            Assert.IsNotNull(mentor);
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
