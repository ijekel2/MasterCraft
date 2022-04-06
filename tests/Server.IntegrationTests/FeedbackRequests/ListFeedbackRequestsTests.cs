using MasterCraft.Client.Common.Api;
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
        public async Task ShouldReturnMentorForId()
        {
            Mentor mentor = TestMentor;
            await SeedDatabase(mentor);

            FeedbackRequest feedbackRequest = TestFeedbackRequest;
            feedbackRequest.MentorId = TestMentor.Id;
            await SeedDatabase(feedbackRequest);

            TestResponse<List<FeedbackRequest>> response = await TestApi.GetAsync<List<FeedbackRequest>>($"offerings?mentorid={mentor.Id}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.IsTrue(response.Response.Count == 1);
        }
    }
}
