using MasterCraft.Application.Authentication.GenerateToken;
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
using MasterCraft.Core.Reports;
using MasterCraft.Application.Authentication.RegisterUser;
using MasterCraft.Core.Requests;
using MasterCraft.Core.Entities;

namespace MasterCraft.Server.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        [Route("/api/token")]
        [HttpPost]
        public async Task<ActionResult<AccessTokenReport>> GenerateToken(GenerateTokenRequest request, [FromServices] GenerateTokenHandler handler)
        {
            return await MyMediator.Send(request, handler);
        }

        [Route("/api/register")]
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> RegisterUser(RegisterUserRequest request, [FromServices] RegisterUserHandler handler)
        {
            return await MyMediator.Send(request, handler);
        }
    }
}
