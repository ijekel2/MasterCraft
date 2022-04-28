using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Learners
{
    public class CreateLearnerService : DomainService<LearnerVm, Learner>
    {
        readonly IDbContext _dbContext;

        public CreateLearnerService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;

        }

        internal override async Task Validate(LearnerVm learner, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }

        internal override async Task<Learner> Handle(LearnerVm request, CancellationToken token = new())
        {
            Learner learner = Map<LearnerVm, Learner>(request);
            learner.ApplicationUserId = Services.CurrentUserService.UserId;

            await _dbContext.AddAsync(learner, token);
            await _dbContext.SaveChangesAsync(token);
            return learner;
        }
    }
}
