﻿using MasterCraft.Client.Common.Api;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MasterCraft.Server.IntegrationTests.TestConstants;
using MasterCraft.Shared.ViewModels;

namespace MasterCraft.Server.IntegrationTests.Videos
{
    public class ListVideosTests : TestBase
    {
        [Test]
        public async Task ShouldReturnListOfVideosForMentor()
        {
            Video video = await SeedHelper.SeedTestVideo();

            TestResponse<List<VideoVm>> response = await TestApi.GetAsync<List<VideoVm>>($"videos?mentorid={video.MentorId}");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.IsTrue(response.Response.Count == 1);
        }
    }
}
