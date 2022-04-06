using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.FeedbackRequests
{
    public class CreateFeedbackRequestService : DomainService<FeedbackRequestViewModel, FeedbackRequest>
    {
        readonly IDbContext _dbContext;

        public CreateFeedbackRequestService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<FeedbackRequest> Handle(FeedbackRequestViewModel request, CancellationToken token = new())
        {
            FeedbackRequest feedbackRequest = Map<FeedbackRequestViewModel, FeedbackRequest>(request);

            await _dbContext.AddAsync(feedbackRequest, token);
            await _dbContext.SaveChangesAsync(token);
            return feedbackRequest;
        }

        internal async override Task Validate(FeedbackRequestViewModel request, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }
    }
}
