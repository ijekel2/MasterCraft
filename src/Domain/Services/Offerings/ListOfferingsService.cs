using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Offerings
{
    public class ListOfferingsService : DomainService<OfferingParameters, List<Offering>>
    {
        readonly IDbContext cDbContext;

        public ListOfferingsService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            cDbContext = dbContext;
        }

        internal override async Task<List<Offering>> Handle(OfferingParameters parameters, CancellationToken token = default)
        {
            IQueryable<Offering> list;
            if (parameters.MentorId != 0)
            {
                list = cDbContext.Offerings.Where(offering => offering.MentorId == parameters.MentorId);
            }
            else
            {
                list = cDbContext.Offerings;
            }

            return await PagedList(list, parameters, token);
        }

        internal override async Task Validate(OfferingParameters parameters, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
