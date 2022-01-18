using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Core.Dto;
using MasterCraft.Infrastructure.Persistence;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> cUserManager;
        private readonly ApplicationDbContext cDbContext;

        public IdentityService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            cUserManager = userManager;
            cDbContext = dbContext;
        }

        public async Task<AuthenticatedUserDto> GenerateToken(string username)
        {
            var user = await cUserManager.FindByEmailAsync(username);

            var roles = from userRole in cDbContext.UserRoles
                        join role in cDbContext.Roles on userRole.RoleId equals role.Id
                        where userRole.UserId == user.Id
                        select new { userRole.UserId, userRole.RoleId, role.Name };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySaltIsTheBestSalt")),
                        SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            AuthenticatedUserDto output = new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Username = username
            };

            return output;
        }

        public async Task<bool> IsValidUserNameAndPassword(string password, string username)
        {
            var user = await cUserManager.FindByEmailAsync(username);
            return await cUserManager.CheckPasswordAsync(user, password);
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await cUserManager.FindByIdAsync(userId);
            return await cUserManager.GetUserNameAsync(user);
        }
    }
}
