using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Application.MentorProfiles.CreateMentorProfile;
using MasterCraft.Core.Entities;
using MasterCraft.Core.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empty = MasterCraft.Core.Reports.Empty;

namespace MasterCraft.Server.Controllers
{
    public class MentorProfilesController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<Empty>> Create(MentorProfile profile, [FromServices] CreateMentorProfileHandler handler)
        {
            return await MyMediator.Send(profile, handler);
        }

        [HttpPost]
        public async Task<ActionResult<Empty>> Update(MentorProfile profile, [FromServices] CreateMentorProfileHandler handler)
        {
            return await MyMediator.Send(profile, handler);
        }
    }
}
