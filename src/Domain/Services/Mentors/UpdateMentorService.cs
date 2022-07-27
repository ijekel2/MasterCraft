using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace MasterCraft.Domain.Services.Mentors
{
        public class UpdateMentorService : DomainService<MentorVm, EmptyVm>
        {
            readonly IDbContext _dbContext;

            public UpdateMentorService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
            {
                _dbContext = dbContext;
            }

            internal override async Task<EmptyVm> Handle(MentorVm mentorVm, CancellationToken token = default)
            {
                Mentor mentor = await _dbContext.Mentors.FirstOrDefaultAsync(mentor => mentor.UserId == Services.CurrentUserService.UserId);

                Map(mentorVm, mentor);

                await _dbContext.SaveChangesAsync();

                return EmptyVm.Value;
        
            }

            internal override async Task Validate(MentorVm mentorVm, DomainValidator validator, CancellationToken pToken = default)
            {
                await Task.CompletedTask;
            }
        }
}
