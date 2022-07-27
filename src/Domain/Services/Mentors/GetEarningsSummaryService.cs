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
    public class GetEarningsSummaryService : DomainService<string, EarningsSummaryVm>
    {
        readonly IDbContext _dbContext;

        public GetEarningsSummaryService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<EarningsSummaryVm> Handle(string id, CancellationToken token = default)
        {
            decimal earned = 0;
            decimal inQueue = 0;

            var requests = await _dbContext.FeedbackRequests
                .Where(feedbackRequest => feedbackRequest.Status == FeedbackRequestStatus.Submitted || feedbackRequest.Status == FeedbackRequestStatus.Fulfilled)
                .Include(request => request.Offering)
                .Select(request => new { request.Status, request.Offering.Price })
                .ToListAsync(token);

            foreach (var request in requests)
            {
                if (request.Status == FeedbackRequestStatus.Submitted)
                {
                    inQueue += request.Price;
                }
                else if (request.Status == FeedbackRequestStatus.Fulfilled)
                {
                    earned += request.Price;
                }
            }

            return new EarningsSummaryVm()
            {
                AmountEarned = earned,
                AmountInQueue = inQueue
            };
        }

        internal override Task Validate(string id, DomainValidator validator, CancellationToken token = default)
        {
            return Task.CompletedTask;
        }
    }
}
