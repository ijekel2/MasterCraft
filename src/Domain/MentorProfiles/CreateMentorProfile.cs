using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Domain.Common.Models;
using MasterCraft.Domain.Common.RequestHandling;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using MasterCraft.Shared.Requests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.MentorProfiles
{
    public class CreateMentorProfile : RequestHandler<MentorProfile, int>
    {
        readonly IDbContext cDbContext;

        public CreateMentorProfile(IDbContext dbContext, RequestHandlerService handlerService) : base(handlerService)
        {
            cDbContext = dbContext;
        }

        internal override async Task Validate(MentorProfile profile, Validator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }

        internal override async Task<int> Handle(MentorProfile profile, CancellationToken token = new())
        {
            await cDbContext.AddAsync(profile);
            await cDbContext.SaveChangesAsync();
            return profile.Id;
        }
    }
}
