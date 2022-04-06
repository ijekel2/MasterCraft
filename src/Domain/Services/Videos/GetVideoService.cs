using MasterCraft.Domain.Common.Utilities;
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
    public class GetVideoService : DomainService<int, VideoViewModel>
    {
        readonly IDbContext _dbContext;

        public GetVideoService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override Task<VideoViewModel> Handle(int id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        internal override Task Validate(int id, DomainValidator validator, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
