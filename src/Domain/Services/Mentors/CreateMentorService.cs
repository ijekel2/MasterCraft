using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Mentors
{
    public class CreateMentorService : DomainService<MentorVm, MentorCreatedVm>
    {
        readonly IDbContext _dbContext;
        readonly IPaymentService _paymentService;

        public CreateMentorService(IDbContext dbContext, IPaymentService paymentService, 
            DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
            _paymentService = paymentService;
        }

        internal override async Task Validate(MentorVm mentor, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }

        internal override async Task<MentorCreatedVm> Handle(MentorVm request, CancellationToken token = new())
        {
            Mentor mentor = Map<MentorVm, Mentor>(request);
            mentor.ApplicationUserId = Services.CurrentUserService.UserId;

            request.Email = Services.CurrentUserService.Email;
            mentor.StripeAccountId = await _paymentService.CreateConnectedAccount(request, token);

            await _dbContext.AddAsync(mentor, token);
            await _dbContext.SaveChangesAsync(token);
            return new MentorCreatedVm()
            {
                ApplicationUserId = mentor.ApplicationUserId.ToString(),
                StripeAccountId = mentor.StripeAccountId
            };
        }
    }
}
