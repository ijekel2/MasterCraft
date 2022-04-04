using MasterCraft.Domain.Entities;
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
        Task<string> AuthorizePayment(Payment payment, CancellationToken token = default);

        Task<string> CapturePayment(Payment payment, CancellationToken token = default);

        Task<string> CancelAuthorization(Payment payment, CancellationToken token = default);
    }
}
