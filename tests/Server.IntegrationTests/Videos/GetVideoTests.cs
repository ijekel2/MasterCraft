using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;
using MasterCraft.Shared.ViewModels;

namespace MasterCraft.Server.IntegrationTests.Videos
{
    public class GetVideoTests : TestBase
    {
        [Test]
        public async Task ShouldReturnVideoForId()
        {
            Video video = await SeedHelper.SeedTestVideo();

            TestResponse<VideoVm> response = await TestApi.GetAsync<VideoVm>($"videos/{video.Id}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
        }
    }
}
