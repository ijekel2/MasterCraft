using MasterCraft.Domain.Entities;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Videos
{
    public class CreateVideoTests : TestBase
    {
        [Test]
        public async Task ShouldSaveVideo()
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
            
            VideoVm video = new()
            {
                VideoType = TestConstants.TestVideo.VideoType,
                Url = TestConstants.TestVideo.Url,
                MentorId = mentor.Id,
                LearnerId = learner.Id,
                FeedbackRequestId = request.Id
            };

            //-- Send create mentor request and validate the response.
            TestResponse<EmptyVm> response = await TestApi.PostJsonAsync<VideoVm, EmptyVm>(
                "videos",
                video);

            Assert.IsTrue(response.Success);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Headers.Location);
            Assert.IsTrue(int.TryParse(response.Headers.Location.Last().ToString(), out int id));

            //-- Select record and validate.
            Video savedVideo = await AppDbContext.Videos.FirstOrDefaultAsync(video => video.Id == id);
            Assert.IsNotNull(savedVideo);
        }
    }
}
