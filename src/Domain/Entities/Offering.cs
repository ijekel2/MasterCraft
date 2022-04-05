using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Entities
{
    public class Offering : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FeedbackMinutes { get; set; }
        public int DeliveryDays { get; set; }
        public decimal Price { get; set; } 
        public string SampleQuestion1 { get; set; }
        public string SampleQuestion2 { get; set; }
        public string SampleQuestion3 { get; set; }
        public string SampleQuestion4 { get; set; }
        public string SampleQuestion5 { get; set; }

        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }
}
