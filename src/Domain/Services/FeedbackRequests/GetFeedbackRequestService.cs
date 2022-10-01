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
using MasterCraft.Shared.ViewModels;

namespace MasterCraft.Domain.Services.FeedbackRequests
{
    public class GetFeedbackRequestService : DomainService<string, FeedbackRequestVm>
    {
        readonly IDbContext _dbContext;

        public GetFeedbackRequestService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<FeedbackRequestVm> Handle(string id, CancellationToken token = default)
        {
            FeedbackRequest lRequest = await _dbContext.FeedbackRequests.FirstOrDefaultAsync(feedbackRequest => feedbackRequest.Id == id, token);
            return Map<FeedbackRequest, FeedbackRequestVm>(lRequest);
        }

        internal override async Task Validate(string request, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
