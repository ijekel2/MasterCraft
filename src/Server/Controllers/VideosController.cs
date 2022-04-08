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
        public async Task<ActionResult> Create(VideoViewModel video, [FromServices] CreateVideoService service)
        {
            Video savedVideo = await service.HandleRequest(video);

            return Created(savedVideo.Id);
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
