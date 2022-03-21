using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Domain.MentorProfiles;
using MasterCraft.Server.Extensions;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    public class MentorProfilesController : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] MentorProfile profile, [FromServices] CreateMentorProfile handler, 
            [FromServices] SetProfileImage setImageHandler)
        {
            int id = await handler.HandleRequest(profile);

            if (HttpContext.Request.Form.Files.Any())
            {
                SetProfileImageRequest setImageRequest = new()
                {
                    Image = await HttpContext.Request.Form.Files.First().ToByteArray(),
                    ProfileId = id
                };

                await setImageHandler.HandleRequest(setImageRequest);
            }

            return id;
        }

        [HttpPost]
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
