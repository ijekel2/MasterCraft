using MasterCraft.Domain.Models;
using MasterCraft.Domain.Services.Authentication;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        [Route("/api/token")]
        [HttpPost]
        public async Task<ActionResult<AccessTokenVm>> GenerateToken(GenerateTokenVm request, [FromServices] GenerateTokenService service)
        {
            return await service.HandleRequest(request);
        }

        [Route("/api/register")]
        [HttpPost]
        public async Task<ActionResult<UserVm>> RegisterUser(RegisterUserVm request, [FromServices] RegisterUserService service)
        {
            return await service.HandleRequest(request);
        }

        [Route("/api/loomtoken")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AccessTokenVm>> GetLoomToken([FromServices] IConfiguration configuration)
        {
            string privateKeyPem = System.IO.File.ReadAllText(configuration["LOOM_PRIVATE_KEY_FILE"]);

            privateKeyPem = privateKeyPem.Replace("-----BEGIN PRIVATE KEY-----", "");
            privateKeyPem = privateKeyPem.Replace("-----END PRIVATE KEY-----", "");

            byte[] privateKeyRaw = Convert.FromBase64String(privateKeyPem);

            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.ImportPkcs8PrivateKey(new ReadOnlySpan<byte>(privateKeyRaw), out _);
            RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(provider);

            var signingCredentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256); ;

            var now = DateTime.UtcNow;
            var nowUnixSeconds = new DateTimeOffset(now).ToUnixTimeSeconds();
            var exp = now.AddMinutes(2);
            var expUnixSeconds = new DateTimeOffset(exp).ToUnixTimeSeconds();

            var jwt = new JwtSecurityToken(
                claims: new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Iat, nowUnixSeconds.ToString(), ClaimValueTypes.Integer64),
                    new Claim(JwtRegisteredClaimNames.Iss, configuration["LOOM_PUBLIC_KEY"]),
                    new Claim(JwtRegisteredClaimNames.Exp, expUnixSeconds.ToString(), ClaimValueTypes.Integer64)
                },
                signingCredentials: signingCredentials
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return await Task.FromResult(new AccessTokenVm
            {
                AccessToken = token,
            });
        }
    }
}
