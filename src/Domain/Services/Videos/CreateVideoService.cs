using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Videos
{
    public class CreateVideoService : DomainService<VideoVm, Video>
    {
        readonly IDbContext _dbContext;

        public CreateVideoService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<Video> Handle(VideoVm viewModel, CancellationToken token = default)
        {
            Video video = Map<VideoVm, Video>(viewModel);

            await _dbContext.AddAsync(video, token);
            await _dbContext.SaveChangesAsync(token);
            return video;
        }

        internal override async Task Validate(VideoVm request, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
