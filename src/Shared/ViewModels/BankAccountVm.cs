using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class BankAccountVm
    {
        [Required]
        [MaxLength(64)]
        public string Country { get; set; }

        [MaxLength(64)]
        public string Currency { get; set; }

        [Required]
        [MaxLength(64)]
        public string RoutingNumber { get; set; }

        [Required]
        [MaxLength(64)]
        public string AccountNumber { get; set; }
    }
}
