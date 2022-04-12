using MasterCraft.Domain.Models;
using MasterCraft.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> IsValidUserNameAndPassword(string password, string username, CancellationToken token = default);

        Task<AccessTokenVm> GenerateToken(string username, CancellationToken token = default);

        Task<string> GetUserNameAsync(string userId, CancellationToken token = default);

        Task<ApplicationUser> FindUserByEmailAsync(string email, CancellationToken token = default);

        Task CreateUserAsync(ApplicationUser user, CancellationToken token = default);
    }
}
