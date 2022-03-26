using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Domain.Common.RequestHandling;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Offerings
{
    public class GetOffering : RequestHandler<int, Offering>
    {
        readonly IDbContext cDbContext;

        public GetOffering(IDbContext dbContext, RequestHandlerService handlerService) : base(handlerService)
        {
            cDbContext = dbContext;
        }

        internal override async Task<Offering> Handle(int id, CancellationToken pToken = default)
        {
            return await cDbContext.Offerings.FirstOrDefaultAsync(offering => offering.Id == id);

        }

        internal override async Task Validate(int request, Validator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
