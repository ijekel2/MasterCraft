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
    public class GetMentorService : DomainService<string, MentorVm>
    {
        readonly IDbContext _dbContext;

        public GetMentorService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<MentorVm> Handle(string id, CancellationToken token = default)
        {
            var mentors = from mentor in _dbContext.Mentors
                    join user in _dbContext.Users on mentor.ApplicationUserId equals user.Id
                    where mentor.ApplicationUserId == id
                    select new MentorVm()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ChannelLink = mentor.ChannelLink,
                        ChannelName = mentor.ChannelName,
                        PersonalTitle = mentor.PersonalTitle,
                        ProfileCustomUri = mentor.ProfileCustomUri,
                        ProfileImageUrl = mentor.ProfileImageUrl
                    };

            return await mentors.FirstOrDefaultAsync();

        }

        internal override async Task Validate(string id, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
