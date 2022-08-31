using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Shared.Enums;
using MasterCraft.Shared.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.FeedbackRequests
{
    public class CreateFeedbackRequestService : DomainService<FeedbackRequestVm, FeedbackRequestCreatedVm>
    {
        readonly IDbContext _dbContext;

        public CreateFeedbackRequestService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<FeedbackRequestCreatedVm> Handle(FeedbackRequestVm request, CancellationToken token = new())
        {
            FeedbackRequest feedbackRequest = Map<FeedbackRequestVm, FeedbackRequest>(request);
            feedbackRequest.Status = FeedbackRequestStatus.Pending;
            feedbackRequest.PaymentIntentId = request.PaymentIntentId;

            await _dbContext.AddAsync(feedbackRequest, token);
            await _dbContext.SaveChangesAsync(token);
            return new FeedbackRequestCreatedVm()
            {
                FeedbackRequestId = feedbackRequest.Id
            };
        }

        internal async override Task Validate(FeedbackRequestVm request, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }
    }
}
