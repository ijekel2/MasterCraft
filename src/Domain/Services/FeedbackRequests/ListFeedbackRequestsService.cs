using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Parameters;
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
    public class ListFeedbackRequestsService : DomainService<FeedbackRequestParameters, List<FeedbackRequestVm>>
    {
        readonly IDbContext _dbContext;

        public ListFeedbackRequestsService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<List<FeedbackRequestVm>> Handle(FeedbackRequestParameters parameters, CancellationToken token = default)
        {
            IQueryable<FeedbackRequest> list = _dbContext.FeedbackRequests;

            if (!string.IsNullOrEmpty(parameters.MentorId))
            {
                list = list.Where(feedbackRequest => feedbackRequest.MentorId == parameters.MentorId);
            }

            if (parameters.Status != FeedbackRequestStatus.Unknown)
            {
                list = list.Where(feedbackRequest => feedbackRequest.Status == parameters.Status);
            }

            List<FeedbackRequest> feedbackRequests = await PagedList(list, parameters, token);

            return feedbackRequests
                .Select(request => Map<FeedbackRequest, FeedbackRequestVm>(request))
                .ToList();
        }

        internal override async Task Validate(FeedbackRequestParameters parameters, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
