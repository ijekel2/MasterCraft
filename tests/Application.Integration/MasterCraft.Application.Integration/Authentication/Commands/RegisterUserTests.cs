using MasterCraft.Application.Authentication.RegisterUser;
using MasterCraft.Application.Common.Exceptions;
using MasterCraft.Core.Entities;
using MasterCraft.Core.Requests;
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

            var command = new RegisterUserRequest()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                ConfirmPassword = user.Password
            };


            Assert.ThrowsAsync<ValidationException>(async () => await SendAsync(command, GetService<RegisterUserHandler>()));
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