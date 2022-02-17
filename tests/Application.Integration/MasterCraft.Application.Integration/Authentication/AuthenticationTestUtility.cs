using MasterCraft.Application.Authentication.Commands.RegisterUser;
using MasterCraft.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MasterCraft.Application.Integration.Testing;

namespace MasterCraft.Application.Integration.Authentication
{
    public class AuthenticationTestUtility
    {
        public static async Task<ApplicationUser> CreateTestUser()
        {
            var command = new RegisterUserCommand()
            {
                FirstName = "Test",
                LastName = "Testington",
                Email = "testuser@local",
                Password = "password",
                ConfirmPassword = "password"
            };

            return await SendAsync(command);
        }
    }
}
