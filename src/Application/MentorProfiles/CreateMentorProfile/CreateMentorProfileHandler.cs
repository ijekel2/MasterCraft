using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Application.Common.Models;
using MasterCraft.Application.Common.Utilities;
using MasterCraft.Core.Entities;
using MasterCraft.Core.Reports;
using MasterCraft.Core.Requests;
using System;
using System.Threading.Tasks;

namespace MasterCraft.Application.MentorProfiles.CreateMentorProfile
{
    public class CreateMentorProfileHandler : IRequestHandler<MentorProfile, Empty>
    {
        public async Task Validate(MentorProfile command, Validator validator)
        {
            throw new NotImplementedException();
        }

        public async Task<Empty> Handle(MentorProfile command)
        {
            throw new NotImplementedException();
        }
    }
}
