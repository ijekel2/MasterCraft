using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels.Aggregates
{
    public class CheckoutDetailsVm
    {
        public string AccountId { get; set; }
        public string CancelUrl { get; set; }
        public string SuccessUrl { get; set; }
        public string Currency { get; set; }
        public decimal ApplicationFee { get; set; }
        public decimal ServiceCharge { get; set; }
        public string CustomerEmail { get; set; }
        public FeedbackRequestVm FeedbackRequest { get; set; }
        public OfferingVm Offering { get; set; }

    }
}
