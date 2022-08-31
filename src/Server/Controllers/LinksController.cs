using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Parameters;
using MasterCraft.Shared.Enums;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    [Authorize]
    public class LinksController : ApiBaseController
    {
        [HttpPost]
        [Route("onboarding")]
        public async Task<ActionResult<OnboardingLinkVm>> Onboarding(CreateOnboardingLinkVm request, [FromServices] IPaymentService paymentService)
        {
            return await paymentService.CreateOnboardingLink(request);
        }
    }
}
