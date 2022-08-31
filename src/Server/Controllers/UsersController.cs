using MasterCraft.Domain.Services.Users;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    [Authorize]
    public class UsersController : ApiBaseController
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserVm>> Get(string id, [FromServices] GetUsersService getUserService)
        {
            return await getUserService.HandleRequest(id);
        }
    }
}
