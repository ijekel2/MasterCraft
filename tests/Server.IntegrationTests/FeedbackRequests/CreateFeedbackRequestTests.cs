﻿using MasterCraft.Domain.Entities;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static MasterCraft.Server.IntegrationTests.TestConstants;

namespace MasterCraft.Server.IntegrationTests.FeedbackRequests
{
    public class CreateFeedbackRequestTests : TestBase
    {
        [Test]
        public async Task ShouldSaveFeedbackRequest()
        {
            Mentor mentor = TestConstants.TestMentor;
            Learner learner = TestConstants.TestLearner;
            Offering offering = TestConstants.TestOffering;

            await SeedDatabase(mentor);
            await SeedDatabase(learner);

            offering.MentorId = mentor.Id;
            await SeedDatabase(offering);

            FeedbackRequestViewModel request = new()
            {
                Status = TestFeedbackRequest.Status,
                ContentLink = TestFeedbackRequest.ContentLink,
                MentorId = mentor.Id,
                LearnerId = learner.Id,
                OfferingId = offering.Id
            };

            //-- Send create mentor request and validate the response.
            TestResponse<Empty> response = await TestApi.PostJsonAsync<FeedbackRequestViewModel, Empty>(
                "feedbackrequests",
                request);

            Assert.IsTrue(response.Success);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Headers.Location);
            Assert.IsTrue(int.TryParse(response.Headers.Location.Last().ToString(), out int id));

            //-- Select record and validate.
            FeedbackRequest feedbackRequest = await AppDbContext.FeedbackRequests.FirstOrDefaultAsync(request => request.Id == id);
            Assert.IsNotNull(feedbackRequest);
        }
    }
}