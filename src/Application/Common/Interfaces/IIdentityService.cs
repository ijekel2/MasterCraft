using MasterCraft.Core.Entities;
using MasterCraft.Core.ReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> IsValidUserNameAndPassword(string password, string username);

        Task<AccessTokenReportModel> GenerateToken(string username);

        Task<string> GetUserNameAsync(string userId);

        Task<ApplicationUser> FindUserByEmailAsync(string email);

        Task CreateUserAsync(ApplicationUser user);
    }
}
