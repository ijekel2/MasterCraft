using MasterCraft.Application.Authentication.GenerateToken;
using MasterCraft.Application.Common.Exceptions;
using MasterCraft.Core.Entities;
using MasterCraft.Core.Reports;
using MasterCraft.Core.Requests;
using NUnit.Framework;
using System.Threading.Tasks;
using static MasterCraft.Application.Integration.Testing;

namespace MasterCraft.Application.Integration.Authentication.Commands
{
    [TestFixture()]
    public class GenerateTokenTests : TestBase
    {
        [Test]
        public void ShouldFailIfNoUserNameOrPassword()
        {
            var command = new GenerateTokenRequest() 
            { 
                Username = string.Empty,
                Password = string.Empty
            };

            Assert.ThrowsAsync<ValidationException>(() => SendAsync(command, GetService<GenerateTokenHandler>()));
        }

        [Test]
        public async Task ShouldReturnAccessTokenForValidUsernameAndPassword()
        {
            ApplicationUser user = await AuthenticationTestUtility.CreateTestUser();

            var command = new GenerateTokenRequest()
            {
                Username = user.UserName,
                Password = "password"
            };

            AccessTokenReport lToken = await SendAsync(command, GetService<GenerateTokenHandler>());

            Assert.IsNotEmpty(lToken.AccessToken);
            Assert.AreEqual(user.UserName, lToken.Username);
        }
    }
}