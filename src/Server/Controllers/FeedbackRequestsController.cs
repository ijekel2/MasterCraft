using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Parameters;
using MasterCraft.Domain.Services.FeedbackRequests;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    [Authorize]
    public class FeedbackRequestsController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<FeedbackRequestCreatedVm>> Create(FeedbackRequestVm request, [FromServices] CreateFeedbackRequestService service)
        {
            return await service.HandleRequest(request);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<FeedbackRequestVm>> Get(string id, [FromServices] GetFeedbackRequestService service)
        {
            return await service.HandleRequest(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<FeedbackRequestVm>>> List([FromQuery] FeedbackRequestParameters parameters, [FromServices] ListFeedbackRequestsService service)
        {
            return await service.HandleRequest(parameters);
        }

        [HttpGet]
        [Route("{id}/getDetail")]
        public async Task<ActionResult<FeedbackRequestDetailVm>> GetDetail(
            string id, [FromServices] GetFeedbackRequestDetailService service)
        {
            return await service.HandleRequest(id);
        }

        [HttpPost]
        [Route("submit")]
        public async Task<ActionResult> Submit(SubmitFeedbackRequestVm request, [FromServices] SubmitFeedbackRequestService service)
        {
            await service.HandleRequest(request);
            return Ok();
        }

        [HttpPost]
        [Route("fulfill")]
        public async Task<ActionResult> Fulfill(FulfillFeedbackRequestVm request, [FromServices] FulfillFeedbackRequestService service)
        {
            await service.HandleRequest(request);
            return Ok();
        }

        [HttpPost]
        [Route("decline")]
        public async Task<ActionResult> Decline(DeclineFeedbackRequestVm request, [FromServices] DeclineFeedbackRequestService service)
        {
            await service.HandleRequest(request);
            return Ok();
        }
    }
}
