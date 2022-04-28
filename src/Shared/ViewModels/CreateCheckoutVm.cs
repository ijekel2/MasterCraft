using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class CreateCheckoutVm
    {
        public string AccountId { get; set; }

        public string CancelUrl { get; set; }

        public string SuccessUrl { get; set; }

        public string OfferingName { get; set; }

        public string OfferingDescription { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; }

        public decimal ApplicationFee { get; set; }
    }
}
