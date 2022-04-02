using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services.Mentors;
using MasterCraft.Server.Extensions;
using MasterCraft.Domain.Entities;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MasterCraft.Server.Controllers
{
    public class MentorsController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create([FromServices] CreateMentorService service,
            [FromServices] SetProfileImageService setImageHandler)
        {
            string jsonKey = HttpContext.Request.Form.Keys.First();
            HttpContext.Request.Form.TryGetValue(jsonKey, out StringValues mentorJson);
            MentorViewModel? request = JsonSerializer.Deserialize<MentorViewModel>(mentorJson.ToString());

            if (request is null)
            {
                return BadRequest();
            }

            Mentor profile = await service.HandleRequest(request);

            if (HttpContext.Request.Form.Files.Any())
            {
                SetProfileImageRequest setImageRequest = new()
                {
                    Image = await HttpContext.Request.Form.Files[0].ToByteArray(),
                    ProfileId = profile.Id
                };

                await setImageHandler.HandleRequest(setImageRequest);
            }

            return Created(profile.Id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Mentor>> Get(int id, [FromServices] GetMentorService service)
        {
            return await service.HandleRequest(id);
        }

        [HttpPost]
        [Route("[controller]/{id}/[action]")]
        public async Task<ActionResult<Empty>> UploadVideo(int id, [FromServices] SetProfileVideoService service)
        {
            if (HttpContext.Request.Form.Files.Any())
            {
                SetProfileVideoRequest setVideoRequest = new()
                {
                    Video = await HttpContext.Request.Form.Files[0].ToByteArray(),
                    ProfileId = id
                };

                return await service.HandleRequest(setVideoRequest);
            }

            return BadRequest();
        }
    }
}
