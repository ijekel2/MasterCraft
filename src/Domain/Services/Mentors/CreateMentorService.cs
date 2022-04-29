using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Mentors
{
    public class CreateMentorService : DomainService<MentorVm, MentorCreatedVm>
    {
        readonly IDbContext _dbContext;
        readonly ICurrentUserService _userService;
        readonly IPaymentService _paymentService;

        public CreateMentorService(IDbContext dbContext, ICurrentUserService currentUserService, 
            IPaymentService paymentService, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
            _userService = currentUserService;
            _paymentService = paymentService;
        }

        internal override async Task Validate(MentorVm mentor, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }

        internal override async Task<MentorCreatedVm> Handle(MentorVm request, CancellationToken token = new())
        {
            Mentor mentor = Map<MentorVm, Mentor>(request);
            mentor.ApplicationUserId = _userService.UserId;
            mentor.StripeAccountId = await _paymentService.CreateConnectedAccount(request, token);

            await _dbContext.AddAsync(mentor, token);
            await _dbContext.SaveChangesAsync(token);
            return new MentorCreatedVm()
            {
                Id = mentor.Id.ToString(),
                StripeAccountId = mentor.StripeAccountId
            };
        }
    }
}
