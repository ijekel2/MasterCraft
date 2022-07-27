using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Models
{
    public class ServiceCharge
    {
        decimal _price;

        public ServiceCharge(decimal price)
        {
            _price = price;
        }

        public decimal Value
        {
            get => CalculateServiceCharge();
        }

        public decimal CalculateServiceCharge()
        {
            return Math.Round(_price * 0.025m, 2);
        }
    }
}
