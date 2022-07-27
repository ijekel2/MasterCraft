using MasterCraft.Domain.Models;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
using NUnit.Framework;
using System.Threading.Tasks;
using static MasterCraft.Server.IntegrationTests.TestConstants;

namespace MasterCraft.Server.IntegrationTests.Authentication
{
    [TestFixture]
    public class RegisterUserTests : TestBase
    {
        [Test]
        public async Task ShouldFailIfAccountAlreadyExistsForEmail()
        {
            var request = new RegisterUserVm()
            {
                FirstName = TestUser.FirstName,
                LastName = TestUser.LastName,
                Email = TestUser.Email,
                Password = TestPassword,
                ConfirmPassword = TestPassword
            };

            TestResponse<ApplicationUser> response = await TestApi.PostJsonAsync<RegisterUserVm, ApplicationUser>("register", request);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public async Task ShouldCreateUserForValidCommand()
        {
            var request = new RegisterUserVm()
            {
                FirstName = TestUser.FirstName,
                LastName = TestUser.LastName,
                Email = "differentmentor@local",
                Password = TestPassword,
                ConfirmPassword = TestPassword
            };

            TestResponse<ApplicationUser> response = await TestApi.PostJsonAsync<RegisterUserVm, ApplicationUser>("register", request);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
        }
    }
}