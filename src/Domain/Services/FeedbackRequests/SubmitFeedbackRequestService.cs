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
    public class SubmitFeedbackRequestService : DomainService<SubmitFeedbackRequestVm, EmptyVm>
    {
        readonly IDbContext _dbContext;

        public SubmitFeedbackRequestService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<EmptyVm> Handle(SubmitFeedbackRequestVm requestVm, CancellationToken token = default)
        {
            FeedbackRequest request = new()
            {
                Status = FeedbackRequestStatus.Pending
            };

            await _dbContext.AddAsync(request);

            Video video = new()
            {
                MentorId = requestVm.MentorId,
                LearnerId = request.LearnerId,
                Url = requestVm.VideoUrl,
                VideoType = VideoType.FeedbackRequest
            };

            await _dbContext.AddAsync(video);

            await _dbContext.SaveChangesAsync();

            return EmptyVm.Value;
        }

        internal override async Task Validate(SubmitFeedbackRequestVm request, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
