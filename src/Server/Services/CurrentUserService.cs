using System.Security.Claims;
using MasterCraft.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MasterCraft.Server.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor cHttpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            cHttpContextAccessor = httpContextAccessor;
        }

        public string UserId => cHttpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    }
}


