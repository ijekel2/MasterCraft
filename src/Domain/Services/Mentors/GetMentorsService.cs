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

namespace MasterCraft.Domain.Services.Mentors
{
    public class GetMentorsService : DomainService<string, MentorVm>
    {
        readonly IDbContext _dbContext;

        public GetMentorsService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<MentorVm> Handle(string id, CancellationToken token = default)
        {
            var mentors = from mentor in _dbContext.Mentors
                    join user in _dbContext.Users on mentor.UserId equals user.Id
                    where mentor.UserId == id
                    select new MentorVm()
                    {
                        ProfileId = mentor.ProfileId,
                    };

            return await mentors.FirstOrDefaultAsync();

        }

        internal override async Task Validate(string id, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
