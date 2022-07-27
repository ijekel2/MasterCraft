using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Parameters;
using MasterCraft.Domain.Services.Mentors;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    public class MentorsController : ApiBaseController
    {
        [HttpPost]
        [Route("setupProfile")]
        public async Task<ActionResult<MentorCreatedVm>> SetupProfile([FromServices] SetupMentorProfileService service, [FromServices] IFileStorage fileService)
        {
            string jsonKey = HttpContext.Request.Form.Keys.First();
            HttpContext.Request.Form.TryGetValue(jsonKey, out StringValues profileJson);
            MentorProfileVm? request = JsonSerializer.Deserialize<MentorProfileVm>(profileJson.ToString());

            MentorCreatedVm mentorCreated = await service.HandleRequest(request);

            //-- Handle any files sent through
            foreach (var file in HttpContext.Request.Form.Files)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    Uri uri = await fileService.SaveFileAsync(stream);
                    mentorCreated.ProfileImageUrl = uri.AbsoluteUri;
                }
            }

            return mentorCreated;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<MentorVm>> Get(string id, [FromServices] GetMentorsService service)
        {
            return await service.HandleRequest(id);
        }

        [HttpGet]
        [Route("getProfile")]
        public async Task<ActionResult<MentorProfileVm>> getProfile([FromQuery] MentorParameters parameters, [FromServices] GetMentorProfilesService service)
        {
            return (await service.HandleRequest(parameters)).FirstOrDefault() ?? new();
        }

        [HttpGet]
        [Route("{id}/getRequestQueue")]
        public async Task<ActionResult<List<FeedbackRequestQueueItemVm>>> GetRequestQueue(string id,
            [FromQuery] RequestQueueParameters parameters, [FromServices] GetRequestQueueService service)
        {
            parameters.MentorId = id;
            return await service.HandleRequest(parameters);
        }

        [HttpGet]
        [Route("{id}/getEarningsSummary")]
        public async Task<ActionResult<EarningsSummaryVm>> GetEarningsSummary(string id, [FromServices] GetEarningsSummaryService service)
        {
            return await service.HandleRequest(id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<EmptyVm>> Put(MentorVm request, [FromServices] UpdateMentorService service)
        {
            return await service.HandleRequest(request);
        }
    }
}
