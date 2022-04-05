using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Mentors
{
    public class CreateMentorService : DomainService<MentorViewModel, Mentor>
    {
        readonly IDbContext _dbContext;

        public CreateMentorService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task Validate(MentorViewModel mentor, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }

        internal override async Task<Mentor> Handle(MentorViewModel request, CancellationToken token = new())
        {
            Mentor mentor = Map<MentorViewModel, Mentor>(request);

            await _dbContext.AddAsync(mentor, token);
            await _dbContext.SaveChangesAsync(token);
            return mentor;
        }
    }
}
