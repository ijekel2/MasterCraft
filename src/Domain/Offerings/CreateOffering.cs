using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Domain.Common.RequestHandling;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using MasterCraft.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace MasterCraft.Domain.Offerings
{
    public class CreateOffering : RequestHandler<CreateOfferingRequest, Offering>
    {
        readonly IDbContext cDbContext;

        public CreateOffering(IDbContext dbContext, RequestHandlerService handlerService) : base(handlerService)
        {
            cDbContext = dbContext;
        }

        internal override async Task<Offering> Handle(CreateOfferingRequest request, CancellationToken token = new())
        {
            Offering offering = Map<CreateOfferingRequest, Offering>(request);

            await cDbContext.AddAsync(offering);
            await cDbContext.SaveChangesAsync();
            return offering;
        }

        internal override Task Validate(CreateOfferingRequest request, Validator validator, CancellationToken token = new())
        {
            throw new NotImplementedException();
        }
    }
}
