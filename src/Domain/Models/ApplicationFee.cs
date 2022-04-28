using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Models
{
    public class ApplicationFee
    {
        decimal _price;

        public ApplicationFee(decimal price)
        {
            _price = price;
        }

        public decimal Value
        {
            get => CalculateServiceCharge() + CalculatePercentageCut();
        }

        public decimal CalculateServiceCharge()
        {
            return 2.75m;
        }

        public decimal CalculatePercentageCut()
        {
            return Math.Round(_price * 0.08m, 2);
        }
    }
}
