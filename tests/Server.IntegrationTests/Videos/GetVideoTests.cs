using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Videos
{
    public class GetVideoTests : TestBase
    {
        [Test]
        public async Task ShouldReturnVideoForId()
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

            TestResponse<Video> response = await TestApi.GetAsync<Video>($"videos/{video.Id}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.AreEqual(1, response.Response.Id);
        }
    }
}
