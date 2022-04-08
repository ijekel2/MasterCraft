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

namespace MasterCraft.Server.IntegrationTests.Videos
{
    public class ListVideosTests : TestBase
    {
        [Test]
        public async Task ShouldReturnListOfVideosForMentor()
        {
            Mentor mentor = TestConstants.TestMentor;
            Learner learner = TestConstants.TestLearner;
            Offering offering = TestConstants.TestOffering;
            FeedbackRequest request = TestConstants.TestFeedbackRequest;
            Video video = TestConstants.TestVideo;

            await SeedDatabase(mentor);
            await SeedDatabase(learner);

            offering.MentorId = mentor.Id;
            await SeedDatabase(offering);

            request.MentorId = mentor.Id;
            request.LearnerId = learner.Id;
            request.OfferingId = offering.Id;
            await SeedDatabase(TestConstants.TestFeedbackRequest);

            video.MentorId = mentor.Id;
            video.LearnerId = learner.Id;
            video.FeedbackRequestId = request.Id;
            await SeedDatabase(video);

            TestResponse<List<Video>> response = await TestApi.GetAsync<List<Video>>($"videos?mentorid={mentor.Id}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.IsTrue(response.Response.Count == 1);
        }
    }
}
