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
        public async Task ShouldReturnListOfFeedbackRequestsForMentor()
        {
            FeedbackRequest request = await SeedHelper.SeedTestFeedbackRequest();

            TestResponse<List<FeedbackRequest>> response = await TestApi.GetAsync<List<FeedbackRequest>>($"feedbackrequests?mentorid={request.MentorId}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.IsTrue(response.Response.Count == 1);
        }
    }
}
