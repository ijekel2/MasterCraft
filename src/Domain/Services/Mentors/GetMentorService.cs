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

namespace MasterCraft.Domain.Services.Mentors
{
    public class GetMentorService : DomainService<int, Mentor>
    {
        readonly IDbContext _dbContext;

        public GetMentorService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<Mentor> Handle(int id, CancellationToken token = default)
        {
            return await _dbContext.Mentors.FirstOrDefaultAsync(mentor => mentor.Id == id, token);

        }

        internal override async Task Validate(int request, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
