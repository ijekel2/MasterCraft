using MasterCraft.Application.Authentication.RegisterUser;
using MasterCraft.Core.Entities;
using MasterCraft.Core.Requests;
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
            var request = new RegisterUserRequest()
            {
                FirstName = "Test",
                LastName = "Testington",
                Email = "testuser@local",
                Password = "password",
                ConfirmPassword = "password"
            };

            return await SendAsync(request, GetService<RegisterUserHandler>());
        }
    }
}
