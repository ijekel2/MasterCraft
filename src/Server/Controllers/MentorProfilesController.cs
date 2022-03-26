using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Domain.MentorProfiles;
using MasterCraft.Server.Extensions;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using MasterCraft.Shared.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    public class MentorProfilesController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create([FromServices] CreateMentorProfile handler,
            [FromServices] SetProfileImage setImageHandler)
        {
            string jsonKey = HttpContext.Request.Form.Keys.First();
            HttpContext.Request.Form.TryGetValue(jsonKey, out StringValues profileJson);
            CreateMentorProfileRequest? request = JsonSerializer.Deserialize<CreateMentorProfileRequest>(profileJson.ToString());

            if (request is null)
            {
                return BadRequest();
            }

            MentorProfile profile = await handler.HandleRequest(request);

            if (HttpContext.Request.Form.Files.Any())
            {
                SetProfileImageRequest setImageRequest = new()
                {
                    Image = await HttpContext.Request.Form.Files.First().ToByteArray(),
                    ProfileId = profile.Id
                };

                await setImageHandler.HandleRequest(setImageRequest);
            }

            return Ok();
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<ActionResult<MentorProfile>> Get(int id, [FromServices] GetMentorProfile handler)
        {
            return await handler.HandleRequest(id);
        }

        [HttpPost]
        [Route("[controller]/{id}/[action]")]
        public async Task<ActionResult<Empty>> UploadVideo(int id, [FromServices] SetProfileVideo handler)
        {
            if (HttpContext.Request.Form.Files.Any())
            {
                SetProfileVideoRequest setVideoRequest = new()
                {
                    Video = await HttpContext.Request.Form.Files.First().ToByteArray(),
                    ProfileId = id
                };

                return await handler.HandleRequest(setVideoRequest);
            }

            return BadRequest();
        }
    }
}
