using MasterCraft.Domain.Models;
using MasterCraft.Domain.Services.Authentication;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        [Route("/api/token")]
        [HttpPost]
        public async Task<ActionResult<AccessTokenViewModel>> GenerateToken(GenerateTokenViewModel request, [FromServices] GenerateTokenService service)
        {
            return await service.HandleRequest(request);
        }

        [Route("/api/register")]
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> RegisterUser(RegisterUserViewModel request, [FromServices] RegisterUserService service)
        {
            return await service.HandleRequest(request);
        }
    }
}
