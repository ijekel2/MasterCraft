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
    public class FulfillFeedbackRequestService : DomainService<FulfillFeedbackRequestVm, EmptyVm>
    {
        readonly IDbContext _dbContext;

        public FulfillFeedbackRequestService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<EmptyVm> Handle(FulfillFeedbackRequestVm requestVm, CancellationToken token = default)
        {
            FeedbackRequest request = new()
            {
                Id = requestVm.FeedbackRequestId,
                Status = FeedbackRequestStatus.Fulfilled
            };

            _dbContext.Update(request);

            Video video = new()
            {
                MentorId = requestVm.MentorId,
                LearnerId = request.LearnerId,
                FeedbackRequestId = requestVm.FeedbackRequestId,
                Url = requestVm.VideoUrl,
                VideoType = VideoType.FeedbackResponse
            };

            await _dbContext.AddAsync(video);

            await _dbContext.SaveChangesAsync();

            return EmptyVm.Value;
        }

        internal override async Task Validate(FulfillFeedbackRequestVm request, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
