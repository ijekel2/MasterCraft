using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Shared.Enums;
using MasterCraft.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.FeedbackRequests
{
    public class DeclineFeedbackRequestService : DomainService<DeclineFeedbackRequestVm, EmptyVm>
    {
        readonly IDbContext _dbContext;

        public DeclineFeedbackRequestService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<EmptyVm> Handle(DeclineFeedbackRequestVm requestVm, CancellationToken token = default)
        {
            FeedbackRequest request = new();
            request.Id = requestVm.FeedbackRequestId;
            _dbContext.FeedbackRequests.Attach(request);

            request.Status = FeedbackRequestStatus.Declined;

            await _dbContext.SaveChangesAsync();
            
            return EmptyVm.Value;
        }

        internal override async Task Validate(DeclineFeedbackRequestVm request, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
