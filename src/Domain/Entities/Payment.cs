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
        public int Id { get; set; }
        public decimal Price { get; set; }

        public DateTime? AuthorizationDate { get; set; }
        public DateTime? CaptureDate { get; set; }

        public string FeedbackRequestId { get; set; }
        public FeedbackRequest FeedbackRequest { get; set; }

        public string StripePaymentId { get; set; }

    }
}
