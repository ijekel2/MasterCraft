using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Shared.Enums;
using MasterCraft.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.FeedbackRequests
{
    public class FulfillFeedbackRequestService : DomainService<FulfillFeedbackRequestVm, EmptyVm>
    {
        readonly IDbContext _dbContext;
        readonly IPaymentService _paymentService;

        public FulfillFeedbackRequestService(IDbContext dbContext, IPaymentService paymentService, 
            DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
            _paymentService = paymentService;
        }

        internal override async Task<EmptyVm> Handle(FulfillFeedbackRequestVm requestVm, CancellationToken token = default)
        {
            FeedbackRequest request = await _dbContext.FeedbackRequests.FirstOrDefaultAsync(request => request.Id == requestVm.FeedbackRequestId);

            if (request == null)
            {
                throw new Exception();
            }

            request.Status = FeedbackRequestStatus.Fulfilled;
            request.ResponseDate = DateTime.Now;

            await _paymentService.CapturePayment(request.PaymentIntentId);

            await _dbContext.SaveChangesAsync();

            return EmptyVm.Value;
        }

        internal override async Task Validate(FulfillFeedbackRequestVm request, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
