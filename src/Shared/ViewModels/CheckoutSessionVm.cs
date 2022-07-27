using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class CheckoutSessionVm
    {
        public string PaymentIntentId { get; set; }
        public string CheckoutSessionId { get; set; }
        public string CheckoutUrl { get; set; }
    }
}
