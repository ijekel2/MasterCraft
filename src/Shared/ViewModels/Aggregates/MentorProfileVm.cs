using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels.Aggregates
{
    public class MentorProfileVm
    {
        public MentorUserVm MentorUser { get; set; } = new();

        public IEnumerable<OfferingVm> Offerings { get; set; } = new List<OfferingVm>();
    }
}
