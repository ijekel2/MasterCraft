using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Parameters
{
    public class RequestQueueParameters : QueryStringParameters
    {
        public string MentorId { get; set; }
    }
}
