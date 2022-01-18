using MasterCraft.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    [Route("/api")]
    public class TokenController : Controller
    {
        private readonly IDbContext cDbContext;
        private readonly IIdentityService cIdentityService;

        public TokenController(IDbContext pDbContext, IIdentityService pIdentityService)
        {
            cDbContext = pDbContext;
            cIdentityService = pIdentityService;
        }

        [Route("/token")]
        [HttpPost]
        public async Task<IActionResult> Create(string username, string password)
        {
            if (await cIdentityService.IsValidUserNameAndPassword(username, password))
            {
                return new ObjectResult(await cIdentityService.GenerateToken(username));
            }
            else
            {
                throw new Exception("Bad");
            }
        }
    }
}
