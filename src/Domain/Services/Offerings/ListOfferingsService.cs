using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Parameters;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Offerings
{
    public class ListOfferingsService : DomainService<OfferingParameters, List<OfferingVm>>
    {
        readonly IDbContext _dbContext;

        public ListOfferingsService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<List<OfferingVm>> Handle(OfferingParameters parameters, CancellationToken token = default)
        {
            IQueryable<Offering> list = _dbContext.Offerings;

            if (parameters.MentorId != 0)
            {
                list = list.Where(offering => offering.MentorId == parameters.MentorId);
            }

            List<Offering> offerings = await PagedList(list, parameters, token);
            return offerings.Select(offering => Map<Offering, OfferingVm>(offering)).ToList();
        }

        internal override async Task Validate(OfferingParameters parameters, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
