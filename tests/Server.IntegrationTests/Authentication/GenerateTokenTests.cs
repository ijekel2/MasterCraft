using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.Reports;
using MasterCraft.Shared.Requests;
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
            var request = new GenerateTokenRequest() 
            { 
                Username = string.Empty,
                Password = string.Empty
            };

            TestResponse<AccessTokenReport> response = await TestApi.PostAsync<GenerateTokenRequest, AccessTokenReport>("token", request);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public async Task ShouldReturnAccessTokenForValidUsernameAndPassword()
        {
            var request = new GenerateTokenRequest()
            {
                Username = TestMentor.Username,
                Password = TestMentor.Password
            };

            TestResponse<AccessTokenReport> response = await TestApi.PostAsync<GenerateTokenRequest, AccessTokenReport>("token", request);
            Assert.IsTrue(response.Success);
            Assert.IsNotEmpty(response.Response.AccessToken);
            Assert.AreEqual(TestMentor.Username, response.Response.Username);
        }
    }
}