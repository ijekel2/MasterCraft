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

namespace MasterCraft.Domain.MentorProfiles
{
    public class GetMentorProfile : RequestHandler<int, MentorProfile>
    {
        readonly IDbContext cDbContext;

        public GetMentorProfile(IDbContext dbContext, RequestHandlerService handlerService) : base(handlerService)
        {
            cDbContext = dbContext;
        }

        internal override async Task<MentorProfile> Handle(int id, CancellationToken pToken = default)
        {
            return await cDbContext.MentorProfiles.FirstOrDefaultAsync(profile => profile.Id == id);

        }

        internal override async Task Validate(int request, Validator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
