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
using MasterCraft.Shared.ViewModels;

namespace MasterCraft.Domain.Services.Offerings
{
    public class GetOfferingService : DomainService<int, OfferingVm>
    {
        readonly IDbContext _dbContext;

        public GetOfferingService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<OfferingVm> Handle(int id, CancellationToken token = default)
        {
            Offering offering = await _dbContext.Offerings.FirstOrDefaultAsync(offering => offering.Id == id, token);
            return Map<Offering, OfferingVm>(offering);
        }

        internal override async Task Validate(int request, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
