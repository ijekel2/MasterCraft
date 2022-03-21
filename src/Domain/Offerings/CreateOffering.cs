using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Domain.Common.RequestHandling;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Offerings
{
    public class CreateOffering : RequestHandler<Offering, Empty>
    {
        readonly IDbContext cDbContext;

        public CreateOffering(IDbContext dbContext, RequestHandlerService handlerService) : base(handlerService)
        {
            cDbContext = dbContext;
        }

        internal override async Task<Empty> Handle(Offering offering, CancellationToken token = new())
        {
            await cDbContext.AddAsync(offering);
            await cDbContext.SaveChangesAsync();
            return Empty.Value;
        }

        internal override Task Validate(Offering offering, Validator validator, CancellationToken token = new())
        {
            throw new NotImplementedException();
        }
    }
}
