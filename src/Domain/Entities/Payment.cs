using MasterCraft.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int FeedbackRequestId { get; set; }

        public decimal Price { get; set; }

        public int PaymentCardId { get; set; }

        public int BankAccountId { get; set; }

        public string AuthorizationCode { get; set; }

        public string TransactionId { get; set; }

        public DateTime? AuthorizationDate { get; set; }

        public DateTime? CaptureDate { get; set; }

    }
}
