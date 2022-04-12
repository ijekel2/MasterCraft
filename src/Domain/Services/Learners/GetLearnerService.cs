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
    public class GetLearnerService : DomainService<int, Learner>
    {
        readonly IDbContext _dbContext;

        public GetLearnerService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<Learner> Handle(int id, CancellationToken token = default)
        {
            return await _dbContext.Learners.FirstOrDefaultAsync(mentor => mentor.Id == id, token);

        }

        internal override async Task Validate(int request, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
