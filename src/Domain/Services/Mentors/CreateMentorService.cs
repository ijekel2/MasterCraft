using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Mentors
{
    public class CreateMentorService : DomainService<MentorVm, Mentor>
    {
        readonly IDbContext _dbContext;
        readonly ICurrentUserService _userService;

        public CreateMentorService(IDbContext dbContext, ICurrentUserService currentUserService, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
            _userService = currentUserService;
        }

        internal override async Task Validate(MentorVm mentor, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }

        internal override async Task<Mentor> Handle(MentorVm request, CancellationToken token = new())
        {
            Mentor mentor = Map<MentorVm, Mentor>(request);
            mentor.ApplicationUserId = _userService.UserId;

            await _dbContext.AddAsync(mentor, token);
            await _dbContext.SaveChangesAsync(token);
            return mentor;
        }
    }
}
