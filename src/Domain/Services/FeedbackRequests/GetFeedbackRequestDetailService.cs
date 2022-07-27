using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Parameters;
using MasterCraft.Shared.Enums;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.FeedbackRequests
{
    public class GetFeedbackRequestDetailService : DomainService<string, FeedbackRequestDetailVm>
    {
        readonly IDbContext _dbContext;

        public GetFeedbackRequestDetailService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<FeedbackRequestDetailVm> Handle(string id, CancellationToken token = default)
        {
            IQueryable<FeedbackRequest> list = _dbContext.FeedbackRequests;

            FeedbackRequest request = await list
                .Where(feedbackRequest => feedbackRequest.Id == id && feedbackRequest.Status == FeedbackRequestStatus.Submitted)
                .Include(request => request.Learner)
                .Include(request => request.Offering)
                .FirstOrDefaultAsync(token);

            if (request == null)
            {
                throw new Exception();
            }

            return new FeedbackRequestDetailVm()
            {
                FeedbackRequest = Map<FeedbackRequest, FeedbackRequestVm>(request),
                Learner = Map<User, UserVm>(request.Learner),
                Offering = Map<Offering, OfferingVm>(request.Offering)
            };
        }

        internal override Task Validate(string id, DomainValidator validator, CancellationToken token = default)
        {
            return Task.CompletedTask;
        }
    }
}
