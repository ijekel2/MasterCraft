using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Offerings
{
    public class CreateOfferingService : DomainService<OfferingViewModel, Offering>
    {
        readonly IDbContext _dbContext;

        public CreateOfferingService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<Offering> Handle(OfferingViewModel request, CancellationToken token = new())
        {
            Offering offering = Map<OfferingViewModel, Offering>(request);

            await _dbContext.AddAsync(offering, token);
            await _dbContext.SaveChangesAsync(token);
            return offering;
        }

        internal async override Task Validate(OfferingViewModel request, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }
    }
}
