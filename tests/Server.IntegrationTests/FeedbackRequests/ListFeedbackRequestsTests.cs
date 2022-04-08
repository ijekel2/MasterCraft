﻿using MasterCraft.Client.Common.Api;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MasterCraft.Server.IntegrationTests.TestConstants;

namespace MasterCraft.Server.IntegrationTests.FeedbackRequests
{
    public class ListFeedbackRequestsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnListOfFeedbackRequestsForMentor()
        {
            Mentor mentor = TestConstants.TestMentor;
            Learner learner = TestConstants.TestLearner;
            Offering offering = TestConstants.TestOffering;
            FeedbackRequest request = TestConstants.TestFeedbackRequest;

            await SeedDatabase(mentor);
            await SeedDatabase(learner);

            offering.MentorId = mentor.Id;
            await SeedDatabase(offering);

            request.MentorId = mentor.Id;
            request.LearnerId = learner.Id;
            request.OfferingId = offering.Id;
            await SeedDatabase(TestConstants.TestFeedbackRequest);

            TestResponse<List<FeedbackRequest>> response = await TestApi.GetAsync<List<FeedbackRequest>>($"feedbackrequests?mentorid={mentor.Id}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.IsTrue(response.Response.Count == 1);
        }
    }
}
