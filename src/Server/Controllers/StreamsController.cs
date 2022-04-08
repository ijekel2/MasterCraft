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
    public class StreamsController : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<string>> Get(int id, [FromServices] GetVideoService service)
        {
            return string.Empty;
        }
   
    }
}
