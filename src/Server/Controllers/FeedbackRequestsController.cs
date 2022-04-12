using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Parameters;
using MasterCraft.Domain.Services.FeedbackRequests;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    public class FeedbackRequestsController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create(FeedbackRequestVm request, [FromServices] CreateFeedbackRequestService service)
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

        [HttpPost]
        [Route("submit")]
        public async Task<ActionResult> Submit(SubmitFeedbackRequestVm request, [FromServices] SubmitFeedbackRequestService service)
        {
            await service.HandleRequest(request);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/fulfill")]
        public async Task<ActionResult> Fulfill(int id, FulfillFeedbackRequestVm request, [FromServices] FulfillFeedbackRequestService service)
        {
            request.FeedbackRequestId = id;
            await service.HandleRequest(request);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/decline")]
        public async Task<ActionResult> Decline(int id, DeclineFeedbackRequestVm request, [FromServices] DeclineFeedbackRequestService service)
        {
            request.FeedbackRequestId = id;
            await service.HandleRequest(request);
            return Ok();
        }
    }
}
