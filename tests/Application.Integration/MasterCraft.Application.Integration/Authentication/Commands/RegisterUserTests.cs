using MasterCraft.Application.Authentication.Commands.RegisterUser;
using MasterCraft.Application.Common.Exceptions;
using MasterCraft.Core.Entities;
using NUnit.Framework;
using System.Threading.Tasks;
using static MasterCraft.Application.Integration.Testing;

namespace MasterCraft.Application.Integration.Authentication.Commands
{
    [TestFixture]
    public class RegisterUserTests : TestBase
    {
        [Test]
        public async Task ShouldFailIfAccountAlreadyExistsForEmail()
        {
            ApplicationUser user = await AuthenticationTestUtility.CreateTestUser();

            var command = new RegisterUserCommand()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                ConfirmPassword = user.Password
            };

            Assert.ThrowsAsync<McValidationException>(async () => await SendAsync(command));
        }

        [Test]
        public void ShouldCreateUserForValidCommand()
        {
            ApplicationUser user = null; 
            Assert.DoesNotThrowAsync(async () => user = await AuthenticationTestUtility.CreateTestUser());
            Assert.IsNotNull(user);
        }
    }
}