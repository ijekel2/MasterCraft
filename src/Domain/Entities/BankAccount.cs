using MasterCraft.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }

        public string Institution { get; set; }

        public AccountType AccountType { get; set; }

        public string AccountNumber { get; set; }

        public string RoutingNumber { get; set; }

        public bool Default { get; set; }
    }
}
