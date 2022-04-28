using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using MasterCraft.Domain.Models;

namespace MasterCraft.Domain.Services.Checkouts
{
    public class CreateCheckoutService : DomainService<CreateCheckoutVm, CheckoutVm>
    {
        readonly IDbContext _dbContext;
        readonly IPaymentService _paymentService;

        public CreateCheckoutService(IDbContext dbContext,
            IPaymentService paymentService, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
            _paymentService = paymentService;
        }

        internal override async Task Validate(CreateCheckoutVm mentor, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }

        internal override async Task<CheckoutVm> Handle(CreateCheckoutVm request, CancellationToken token = new())
        {
            ApplicationFee appFee = new ApplicationFee(request.Price);
            request.ApplicationFee = appFee.Value;
            request.Currency = "USD";

            return await _paymentService.CreateCheckout(request, token);
        }
    }
}
