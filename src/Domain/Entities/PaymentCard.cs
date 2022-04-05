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
        public PaymentCardType CardType { get; set; }
        public PaymentCardNetwork CardNetwork { get; set; }
        public string CardNumber { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string BillingCompany { get; set; }
        public string BillingStreet { get; set; }
        public string BillingPremise { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingPostalCode { get; set; }
        public string BillingCountry { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Default { get; set; }

        public int LearnerId { get; set; }
        public Learner Learner { get; set; }
    }
}
