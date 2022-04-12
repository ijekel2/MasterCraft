using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.FeedbackRequests
{
    public class ListFeedbackRequestsService : DomainService<FeedbackRequestParameters, List<FeedbackRequest>>
    {
        readonly IDbContext _dbContext;

        public ListFeedbackRequestsService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<List<FeedbackRequest>> Handle(FeedbackRequestParameters parameters, CancellationToken token = default)
        {
            IQueryable<FeedbackRequest> list;
            if (parameters.MentorId != 0)
            {
                list = _dbContext.FeedbackRequests.Where(feedbackRequest => feedbackRequest.MentorId == parameters.MentorId);
            }
            else
            {
                list = _dbContext.FeedbackRequests;
            }

            return await PagedList(list, parameters, token);
        }

        internal override async Task Validate(FeedbackRequestParameters parameters, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
