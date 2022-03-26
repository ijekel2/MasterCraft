using MasterCraft.Domain.Offerings;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    public class OfferingsController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create(CreateOfferingRequest request, [FromServices] CreateOffering handler)
        {
            await handler.HandleRequest(request);
            return Ok();
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<ActionResult<Offering>> Get(int id, [FromServices] GetOffering handler)
        {
            return await handler.HandleRequest(id);
        }

        [HttpPut]
        public async Task<ActionResult> Update(CreateOfferingRequest request, [FromServices] CreateOffering handler)
        {
            await handler.HandleRequest(request);
            return Ok();
        }
    }
}
