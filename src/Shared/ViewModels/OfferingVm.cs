using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class OfferingVm
    {
        public int Id { get; set; }

        [Required]
        public string MentorId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0.01, 1000)]
        public decimal Price { get; set; }
    }
}
