using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels;
using NUnit.Framework;
using System.Threading.Tasks;
using static MasterCraft.Server.IntegrationTests.TestConstants;

namespace MasterCraft.Server.IntegrationTests.Authentication
{
    [TestFixture()]
    public class GenerateTokenTests : TestBase
    {
        [Test]
        public async Task ShouldFailIfNoUserNameOrPassword()
        {
            var request = new GenerateTokenViewModel() 
            { 
                Username = string.Empty,
                Password = string.Empty
            };

            TestResponse<AccessTokenViewModel> response = await TestApi.PostJsonAsync<GenerateTokenViewModel, AccessTokenViewModel>("token", request);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public async Task ShouldReturnAccessTokenForValidUsernameAndPassword()
        {
            var request = new GenerateTokenViewModel()
            {
                Username = TestUser.Username,
                Password = TestUser.Password
            };

            TestResponse<AccessTokenViewModel> response = await TestApi.PostJsonAsync<GenerateTokenViewModel, AccessTokenViewModel>("token", request);
            Assert.IsTrue(response.Success);
            Assert.IsNotEmpty(response.Response.AccessToken);
            Assert.AreEqual(TestUser.Username, response.Response.Username);
        }
    }
}