using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.FeedbackRequests
{
    public class GetFeedbackRequestService : DomainService<int, FeedbackRequest>
    {
        readonly IDbContext _dbContext;

        public GetFeedbackRequestService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<FeedbackRequest> Handle(int id, CancellationToken token = default)
        {
            return await _dbContext.FeedbackRequests.FirstOrDefaultAsync(feedbackRequest => feedbackRequest.Id == id, token);
        }

        internal override async Task Validate(int request, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
