using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using MasterCraft.Domain.Models;
using MasterCraft.Shared.ViewModels.Aggregates;

namespace MasterCraft.Domain.Services.Checkouts
{
    public class CreateCheckoutService : DomainService<CheckoutDetailsVm, CheckoutSessionVm>
    {
        readonly IDbContext _dbContext;
        readonly IPaymentService _paymentService;

        public CreateCheckoutService(IDbContext dbContext,
            IPaymentService paymentService, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
            _paymentService = paymentService;
        }

        internal override async Task Validate(CheckoutDetailsVm mentor, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }

        internal override async Task<CheckoutSessionVm> Handle(CheckoutDetailsVm checkoutDetails, CancellationToken token = new())
        {
            checkoutDetails.ApplicationFee = new ApplicationFee(checkoutDetails.Offering.Price).Value;
            checkoutDetails.Currency = "USD";
            checkoutDetails.ServiceCharge = new ServiceCharge(checkoutDetails.Offering.Price).Value;

            CheckoutSessionVm session = await _paymentService.CreateCheckout(checkoutDetails, token);

            return session;
        }
    }
}
