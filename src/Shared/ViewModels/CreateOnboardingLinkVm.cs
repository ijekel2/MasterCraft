using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class CreateOnboardingLinkVm
    {
        public string AccountId { get; set; }

        public string RefreshUrl { get; set; }

        public string SuccessUrl { get; set; }
    }
}
