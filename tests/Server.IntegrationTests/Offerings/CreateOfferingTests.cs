﻿using MasterCraft.Domain.Entities;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Offerings
{
    public class CreateOfferingTests : TestBase
    {
        [Test]
        public async Task ShouldSaveOffering()
        {
            Mentor mentor = TestConstants.TestMentor;
            await SeedDatabase(mentor);

            OfferingVm request = new()
            {
                Name = TestConstants.TestOffering.Name,
                Description = TestConstants.TestOffering.Description,
                DeliveryDays = TestConstants.TestOffering.DeliveryDays,
                FeedbackMinutes = TestConstants.TestOffering.FeedbackMinutes,
                Price = TestConstants.TestOffering.Price,
                SampleQuestion1 = TestConstants.TestOffering.SampleQuestion1,
                SampleQuestion2 = TestConstants.TestOffering.SampleQuestion2,
                SampleQuestion3 = TestConstants.TestOffering.SampleQuestion3,
                MentorId = mentor.Id
            };

            //-- Send create mentor request and validate the response.
            TestResponse<EmptyVm> response = await TestApi.PostJsonAsync<OfferingVm, EmptyVm>(
                "offerings",
                request);

            Assert.IsTrue(response.Success);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Headers.Location);
            Assert.IsTrue(int.TryParse(response.Headers.Location.Last().ToString(), out int id));

            //-- Select record and validate.
            Offering offering = await AppDbContext.Offerings.FirstOrDefaultAsync(offering => offering.Id == id);
            Assert.IsNotNull(offering);
        }
    }
}
