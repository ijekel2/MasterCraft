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
using MasterCraft.Domain.Parameters;
using MasterCraft.Domain.Services.Videos;

namespace MasterCraft.Server.Controllers
{
    public class VideosController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create([FromServices] CreateVideoService service,
            [FromServices] SetProfileImageService setImageHandler)
        {
            string jsonKey = HttpContext.Request.Form.Keys.First();
            HttpContext.Request.Form.TryGetValue(jsonKey, out StringValues mentorJson);
            VideoViewModel? request = JsonSerializer.Deserialize<VideoViewModel>(mentorJson.ToString());

            if (request is null)
            {
                return BadRequest();
            }

            Video video = await service.HandleRequest(request);

            if (HttpContext.Request.Form.Files.Any())
            {
                //-- TODO: Save video file.
            }

            return Created(video.Id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<VideoViewModel>> Get(int id, [FromServices] GetVideoService service)
        {
            return await service.HandleRequest(id);
        }
    
        [HttpGet]
        public async Task<ActionResult<List<VideoViewModel>>> List(VideoParameters parameters, [FromServices] ListVideosService service)
        {
            return await service.HandleRequest(parameters);
        }
    }
}
