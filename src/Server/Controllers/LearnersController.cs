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
using MasterCraft.Domain.Services.Learners;

namespace MasterCraft.Server.Controllers
{
    public class LearnersController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create(LearnerVm request, [FromServices] CreateLearnerService service)
        {
            Learner learner = await service.HandleRequest(request);
            return Created(learner.Id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Learner>> Get(int id, [FromServices] GetLearnerService service)
        {
            return await service.HandleRequest(id);
        }
    }
}
