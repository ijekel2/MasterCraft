using AutoMapper;
using AutoMapper.QueryableExtensions;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Entities;
using MasterCraft.Shared.ViewModels;
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
using System.Threading;

namespace MasterCraft.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ExtendedIdentityUser> cUserManager;
        private readonly ApplicationDbContext cDbContext;

        public IdentityService(UserManager<ExtendedIdentityUser> userManager, ApplicationDbContext dbContext)
        {
            cUserManager = userManager;
            cDbContext = dbContext;
        }

        public async Task<AccessTokenViewModel> GenerateToken(string username, CancellationToken token = default)
        {
            var user = await cUserManager.FindByEmailAsync(username);

            var roles = from userRole in cDbContext.UserRoles
                        join role in cDbContext.Roles on userRole.RoleId equals role.Id
                        where userRole.UserId == user.Id
                        select new { userRole.UserId, userRole.RoleId, role.Name };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, username),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var jwtToken = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySaltIsTheBestSalt")),
                        SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            AccessTokenViewModel output = new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Username = username
            };

            return output;
        }

        public async Task<bool> IsValidUserNameAndPassword(string username, string password, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
            {
                return false;
            }

            var user = await cUserManager.FindByEmailAsync(username);

            if (user is null)
            {
                return false;
            }

            return await cUserManager.CheckPasswordAsync(user, password);
        }

        public async Task<string> GetUserNameAsync(string userId, CancellationToken token = default)
        {
            var user = await cUserManager.FindByIdAsync(userId);
            return await cUserManager.GetUserNameAsync(user);
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string email, CancellationToken token = default)
        {
            ExtendedIdentityUser identityUser = await cUserManager.FindByEmailAsync(email);
            ApplicationUser user = null;

            if (identityUser != null)
            {
                user = new ApplicationUser()
                {
                    Id = identityUser.Id,
                    FirstName = identityUser.FirstName,
                    LastName = identityUser.LastName,
                    Email = identityUser.Email,
                    Username = identityUser.UserName,
                    Password = identityUser.PasswordHash
                };
            }
        
            return user;
        }

        public async Task CreateUserAsync(ApplicationUser user, CancellationToken token = default)
        {
            ExtendedIdentityUser identityUser = new ExtendedIdentityUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.Email,
            };

            await cUserManager.CreateAsync(identityUser, user.Password);
        }
    }
}
