using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Parameters;
using MasterCraft.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Videos
{
    public class ListVideosService : DomainService<VideoParameters, List<VideoViewModel>>
    {
        readonly IDbContext _dbContext;

        public ListVideosService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override Task<List<VideoViewModel>> Handle(VideoParameters parameters, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        internal override Task Validate(VideoParameters parameters, DomainValidator validator, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
