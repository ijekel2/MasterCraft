using MasterCraft.Client.Common.Api;
using MasterCraft.Domain.Authentication;
using MasterCraft.Domain.Common.Exceptions;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Requests;
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
            var request = new RegisterUserRequest()
            {
                FirstName = TestMentor.FirstName,
                LastName = TestMentor.LastName,
                Email = TestMentor.Email,
                Password = TestMentor.Password,
                ConfirmPassword = TestMentor.Password
            };

            TestResponse<ApplicationUser> response = await TestApi.PostJsonAsync<RegisterUserRequest, ApplicationUser>("register", request);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public async Task ShouldCreateUserForValidCommand()
        {
            var request = new RegisterUserRequest()
            {
                FirstName = TestMentor.FirstName,
                LastName = TestMentor.LastName,
                Email = "differentmentor@local",
                Password = TestMentor.Password,
                ConfirmPassword = TestMentor.Password
            };

            TestResponse<ApplicationUser> response = await TestApi.PostJsonAsync<RegisterUserRequest, ApplicationUser>("register", request);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
        }
    }
}