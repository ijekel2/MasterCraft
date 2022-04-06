using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.FeedbackRequests
{
    public class CompleteFeedbackService : DomainService<CompleteFeedbackViewModel, Empty>
    {
        readonly IDbContext _dbContext;

        public CompleteFeedbackService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override Task<Empty> Handle(CompleteFeedbackViewModel request, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        internal override Task Validate(CompleteFeedbackViewModel request, DomainValidator validator, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
