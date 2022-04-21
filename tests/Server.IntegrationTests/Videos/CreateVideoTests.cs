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
            FeedbackRequest request = await SeedHelper.SeedTestFeedbackRequest();
            
            VideoVm video = new()
            {
                VideoType = TestConstants.TestVideo.VideoType,
                Url = TestConstants.TestVideo.Url,
                MentorId = request.MentorId,
                LearnerId = request.LearnerId,
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
            using var context = GetDbContext();
            Video savedVideo = await context.Videos.FirstOrDefaultAsync(video => video.Id == id);
            Assert.IsNotNull(savedVideo);
        }
    }
}
