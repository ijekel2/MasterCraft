using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class OfferingViewModel
    {
        [Range(1, int.MaxValue)]
        public int MentorId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1, 60)]
        public int FeedbackMinutes { get; set; }

        [Range(1, 30)]
        public int DeliveryDays { get; set; }

        [Range(0.01, 1000)]
        public decimal Price { get; set; }

        [Required]
        public string SampleQuestion1 { get; set; }

        [Required]
        public string SampleQuestion2 { get; set; }

        [Required]
        public string SampleQuestion3 { get; set; }

        public string SampleQuestion4 { get; set; }

        public string SampleQuestion5 { get; set; }
    }
}
