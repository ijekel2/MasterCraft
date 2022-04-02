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
        readonly IDbContext cDbContext;

        public CreateOfferingService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            cDbContext = dbContext;
        }

        internal override async Task<Offering> Handle(OfferingViewModel request, CancellationToken token = new())
        {
            Offering offering = Map<OfferingViewModel, Offering>(request);

            await cDbContext.AddAsync(offering, token);
            await cDbContext.SaveChangesAsync(token);
            return offering;
        }

        internal async override Task Validate(OfferingViewModel request, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }
    }
}
