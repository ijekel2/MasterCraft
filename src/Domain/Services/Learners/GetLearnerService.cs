using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Learners
{
    public class GetLearnerService : DomainService<string, Learner>
    {
        readonly IDbContext _dbContext;

        public GetLearnerService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<Learner> Handle(string id, CancellationToken token = default)
        {
            return await _dbContext.Learners.FirstOrDefaultAsync(learner => learner.ApplicationUserId == id, token);

        }

        internal override async Task Validate(string id, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
