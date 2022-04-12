using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class LearnerVm
    {
        [MaxLength(256)]
        public string ProfileImageUrl { get; set; }
    }
}
