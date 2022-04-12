using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Videos
{
    public class GetVideoService : DomainService<int, VideoVm>
    {
        readonly IDbContext _dbContext;

        public GetVideoService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<VideoVm> Handle(int id, CancellationToken token = default)
        {
            Video video = await _dbContext.Videos.FirstOrDefaultAsync(offering => offering.Id == id, token);
            return Map<Video, VideoVm>(video);
        }

        internal override async Task Validate(int id, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
