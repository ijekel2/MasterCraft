using MasterCraft.Core.Dto;
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

        Task<AuthenticatedUserDto> GenerateToken(string username);

        Task<string> GetUserNameAsync(string userId);
    }
}
