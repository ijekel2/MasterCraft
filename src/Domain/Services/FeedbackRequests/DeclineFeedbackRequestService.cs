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
    public class DeclineFeedbackRequestService : DomainService<DeclineFeedbackRequestVm, EmptyVm>
    {
        readonly IDbContext _dbContext;
        readonly IPaymentService _paymentService;

        public DeclineFeedbackRequestService(IDbContext dbContext, IPaymentService paymentService, 
            DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
            _paymentService = paymentService;
        }

        internal override async Task<EmptyVm> Handle(DeclineFeedbackRequestVm requestVm, CancellationToken token = default)
        {
            FeedbackRequest request = await _dbContext.FeedbackRequests
                .Where(request => request.Id == requestVm.FeedbackRequestId)
                .Include(request => request.Mentor)
                .FirstOrDefaultAsync();

            if (request == null)
            {
                throw new Exception();
            }

            request.Status = FeedbackRequestStatus.Declined;
            request.ResponseDate = DateTime.Now;

            await _paymentService.CancelPayment(request.PaymentIntentId, request.Mentor.StripeAccountId);

            await _dbContext.SaveChangesAsync();

            return EmptyVm.Value;
        }

        internal override async Task Validate(DeclineFeedbackRequestVm request, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
