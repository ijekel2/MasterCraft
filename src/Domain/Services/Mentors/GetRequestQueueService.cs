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

namespace MasterCraft.Domain.Services.Mentors
{
    public class GetRequestQueueService : DomainService<RequestQueueParameters, List<FeedbackRequestQueueItemVm>>
    {
        readonly IDbContext _dbContext;

        public GetRequestQueueService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<List<FeedbackRequestQueueItemVm>> Handle(RequestQueueParameters parameters, CancellationToken token = default)
        {
            var requests = _dbContext.FeedbackRequests
                .Where(feedbackRequest => feedbackRequest.MentorId == parameters.MentorId && feedbackRequest.Status == FeedbackRequestStatus.Submitted)
                .Include(request => request.Learner)
                .Include(request => request.Offering);

            Page(requests, parameters);

            return await (from request in requests
                    select new FeedbackRequestQueueItemVm()
                    {
                        FeedbackRequestId = request.Id,
                        FirstName = request.Learner.FirstName,
                        LastName = request.Learner.LastName,
                        ProfileImageUrl = request.Learner.ProfileImageUrl,
                        Price = request.Offering.Price,
                        SubmissionDate = request.SubmissionDate
                    }).ToListAsync();
        }

        internal override Task Validate(RequestQueueParameters id, DomainValidator validator, CancellationToken token = default)
        {
            return Task.CompletedTask;
        }
    }
}
