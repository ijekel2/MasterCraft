using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services.Mentors;
using MasterCraft.Shared.Enums;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.FeedbackRequests
{
    public class SubmitFeedbackRequestService : DomainService<SubmitFeedbackRequestVm, EmptyVm>
    {
        readonly IDbContext _dbContext;

        public SubmitFeedbackRequestService(IDbContext dbContext, 
            DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<EmptyVm> Handle(SubmitFeedbackRequestVm requestVm, CancellationToken token = default)
        {
            FeedbackRequest request = await _dbContext.FeedbackRequests.FirstOrDefaultAsync(request => request.Id == requestVm.FeedbackRequestId);

            if (request == null)
            {
                throw new Exception();
            }

            request.Status = FeedbackRequestStatus.Submitted;
            request.SubmissionDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return EmptyVm.Value;
        }

        internal override async Task Validate(SubmitFeedbackRequestVm request, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
