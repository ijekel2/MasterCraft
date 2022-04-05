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

namespace MasterCraft.Domain.Services.Offerings
{
    public class GetOfferingService : DomainService<int, Offering>
    {
        readonly IDbContext _dbContext;

        public GetOfferingService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<Offering> Handle(int id, CancellationToken pToken = default)
        {
            return await _dbContext.Offerings.FirstOrDefaultAsync(offering => offering.Id == id, pToken);
        }

        internal override async Task Validate(int request, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
