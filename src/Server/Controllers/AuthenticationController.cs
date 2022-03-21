using MasterCraft.Domain.Authentication;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using MasterCraft.Shared.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        [Route("/api/token")]
        [HttpPost]
        public async Task<ActionResult<AccessTokenReport>> GenerateToken(GenerateTokenRequest request, [FromServices] GenerateToken handler)
        {
            return await handler.HandleRequest(request);
        }

        [Route("/api/register")]
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> RegisterUser(RegisterUserRequest request, [FromServices] RegisterUser handler)
        {
            return await handler.HandleRequest(request);
        }
    }
}
