using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Parameters;
using MasterCraft.Domain.Services.Checkouts;
using MasterCraft.Shared.Enums;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    [Authorize]
    public class CheckoutsController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<CheckoutSessionVm>> Create(CheckoutDetailsVm request, [FromServices] CreateCheckoutService checkoutService)
        {
            return await checkoutService.HandleRequest(request);
        }
    }
}
