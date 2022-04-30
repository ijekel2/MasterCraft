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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MasterCraft.Server.Controllers
{
    public class MentorsController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<MentorCreatedVm>> Create(MentorVm request, [FromServices] CreateMentorService service)
        {
            MentorCreatedVm mentor = await service.HandleRequest(request);
            return Created(mentor.ApplicationUserId, mentor);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<MentorVm>> Get(string id, [FromServices] GetMentorService service)
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
