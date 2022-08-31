﻿using MasterCraft.Domain.Interfaces;
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
using Microsoft.AspNetCore.Authorization;

namespace MasterCraft.Server.Controllers
{
    [Authorize]
    public class VideosController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create(VideoVm video, [FromServices] CreateVideoService service)
        {
            Video savedVideo = await service.HandleRequest(video);

            return Created(savedVideo.Id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<VideoVm>> Get(int id, [FromServices] GetVideoService service)
        {
            return await service.HandleRequest(id);
        }
    
        [HttpGet]
        public async Task<ActionResult<List<VideoVm>>> List([FromQuery] VideoParameters parameters, [FromServices] ListVideosService service)
        {
            return await service.HandleRequest(parameters);
        }
    }
}
