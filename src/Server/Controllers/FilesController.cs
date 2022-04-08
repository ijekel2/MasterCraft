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
    public class FilesController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Save([FromServices] IFileStorage fileService)
        {
            if (HttpContext.Request.Form.Files.Any())
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    await HttpContext.Request.Form.Files[0].CopyToAsync(stream);
                    Uri uri = await fileService.SaveFileAsync(stream);
                    return Created(uri.AbsoluteUri, null);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
