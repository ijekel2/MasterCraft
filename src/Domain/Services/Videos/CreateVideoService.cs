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
    public class CreateVideoService : DomainService<VideoViewModel, Video>
    {
        readonly IDbContext _dbContext;

        public CreateVideoService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override Task<Video> Handle(VideoViewModel request, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        internal override Task Validate(VideoViewModel request, DomainValidator validator, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
