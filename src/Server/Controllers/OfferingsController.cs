using MasterCraft.Domain.Offerings;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    public class OfferingsController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<Empty>> Create(Offering request, [FromServices] CreateOffering handler)
        {
            return await handler.HandleRequest(request);
        }

        [HttpPost]
        public async Task<ActionResult<Empty>> Update(Offering request, [FromServices] CreateOffering handler)
        {
            return await handler.HandleRequest(request);
        }
    }
}
