using MasterCraft.Application.Authentication.Commands.GenerateToken;
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
using MasterCraft.Core.ReportModels;
using MasterCraft.Application.Authentication.Commands.RegisterUser;
using MasterCraft.Core.CommandModels;

namespace MasterCraft.Server.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        [Route("/api/token")]
        [HttpPost]
        public async Task<ActionResult<AccessTokenReportModel>> GenerateToken(GenerateTokenCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("/api/register")]
        [HttpPost]
        public async Task<ActionResult> RegisterUser(RegisterUserCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }
    }
}
