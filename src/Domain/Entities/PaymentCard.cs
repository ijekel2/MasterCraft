using MasterCraft.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Entities
{
    public class PaymentCard : BaseEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public PaymentCardType CardType { get; set; }

        public PaymentCardNetwork CardNetwork { get; set; }

        public string CardNumber { get; set; }

        public string BillingAddress { get; set; } //-- Todo

        public DateTime ExpirationDate { get; set; }

        public bool Default { get; set; }
    }
}
