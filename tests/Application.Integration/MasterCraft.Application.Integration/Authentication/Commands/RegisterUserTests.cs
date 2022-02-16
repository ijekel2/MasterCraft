using MasterCraft.Application.Authentication.Commands.RegisterUser;
using MasterCraft.Application.Common.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var command = new RegisterUserCommand()
            {
                FirstName = "Test",
                LastName = "Testington",
                Email = "test@local",
                Password = "password",
                ConfirmPassword = "password"
            };

            await SendAsync(command);

            Assert.ThrowsAsync<McValidationException>(async () => await SendAsync(command));
        }
    }
}