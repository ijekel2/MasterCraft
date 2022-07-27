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
        public string Description { get; set; }
        public decimal Price { get; set; } 

        public string MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }
}
