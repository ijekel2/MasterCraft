using MasterCraft.Domain.Entities;
using MasterCraft.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreateConnectedAccount(MentorVm mentor, CancellationToken token = default);

        Task<OnboardingLinkVm> CreateOnboardingLink(CreateOnboardingLinkVm accountId, CancellationToken token = default);

        Task<CheckoutVm> CreateCheckout(CreateCheckoutVm request, CancellationToken token = default);

        Task<string> CreateCustomer(LearnerVm learner, CancellationToken token = default);
    }
}
