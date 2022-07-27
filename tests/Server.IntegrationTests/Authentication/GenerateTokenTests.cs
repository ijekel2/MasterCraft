using MasterCraft.Server.IntegrationTests.Api;
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
            var request = new GenerateTokenVm() 
            { 
                Username = string.Empty,
                Password = string.Empty
            };

            TestResponse<AccessTokenVm> response = await TestApi.PostJsonAsync<GenerateTokenVm, AccessTokenVm>("token", request);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public async Task ShouldReturnAccessTokenForValidUsernameAndPassword()
        {
            var request = new GenerateTokenVm()
            {
                Username = TestUser.Username,
                Password = TestPassword
            };

            TestResponse<AccessTokenVm> response = await TestApi.PostJsonAsync<GenerateTokenVm, AccessTokenVm>("token", request);
            Assert.IsTrue(response.Success);
            Assert.IsNotEmpty(response.Response.AccessToken);
            Assert.AreEqual(TestUser.Username, response.Response.User.Username);
        }
    }
}