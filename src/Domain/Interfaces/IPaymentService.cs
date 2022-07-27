using MasterCraft.Domain.Entities;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
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
        Task<string> CreateConnectedAccount(UserVm user, CancellationToken token = default);
        Task<OnboardingLinkVm> CreateOnboardingLink(CreateOnboardingLinkVm accountId, CancellationToken token = default);
        Task<CheckoutSessionVm> CreateCheckout(CheckoutDetailsVm request, CancellationToken token = default);
        Task<string> CreateCustomer(LearnerVm learner, CancellationToken token = default);
        Task CapturePayment(string paymentIntentId);
        Task CancelPayment(string paymentIntentId);


    }
}
