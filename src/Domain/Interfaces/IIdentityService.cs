using MasterCraft.Domain.Entities;
using MasterCraft.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> IsValidUserNameAndPassword(string password, string username);

        Task<AccessTokenViewModel> GenerateToken(string username);

        Task<string> GetUserNameAsync(string userId);

        Task<ApplicationUser> FindUserByEmailAsync(string email);

        Task CreateUserAsync(ApplicationUser user);
    }
}
