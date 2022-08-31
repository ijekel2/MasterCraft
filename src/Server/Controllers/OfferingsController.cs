using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Parameters;
using MasterCraft.Domain.Services.Offerings;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    [Authorize]
    public class OfferingsController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create(OfferingVm request, [FromServices] CreateOfferingService service)
        {
            Offering offering = await service.HandleRequest(request);
            return Created(offering.Id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OfferingVm>> Get(int id, [FromServices] GetOfferingService service)
        {
            return await service.HandleRequest(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<OfferingVm>>> List([FromQuery] OfferingParameters parameters, [FromServices] ListOfferingsService service)
        {
            return await service.HandleRequest(parameters);
        }

        [HttpPut]
        public async Task<ActionResult> Update(OfferingVm request, [FromServices] CreateOfferingService service)
        {
            await service.HandleRequest(request);
            return Ok();
        }
    }
}
