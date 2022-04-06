using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Parameters;
using MasterCraft.Domain.Services.FeedbackRequests;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    public class FeedbackRequestController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create(FeedbackRequestViewModel request, [FromServices] CreateFeedbackRequestService service)
        {
            FeedbackRequest offering = await service.HandleRequest(request);
            return Created(offering.Id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<FeedbackRequest>> Get(int id, [FromServices] GetFeedbackRequestService service)
        {
            return await service.HandleRequest(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<FeedbackRequest>>> List([FromQuery] FeedbackRequestParameters parameters, [FromServices] ListFeedbackRequestsService service)
        {
            return await service.HandleRequest(parameters);
        }

        [HttpPut]
        public async Task<ActionResult> Update(FeedbackRequestViewModel request, [FromServices] CreateFeedbackRequestService service)
        {
            await service.HandleRequest(request);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/complete")]
        public async Task<ActionResult> Complete(CompleteFeedbackViewModel request, [FromServices] CompleteFeedbackService service)
        {
            await service.HandleRequest(request);
            return Ok();
        }

    }
}
