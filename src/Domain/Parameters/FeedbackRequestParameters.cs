﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Parameters
{
    public class FeedbackRequestParameters : QueryStringParameters
    {
        public int MentorId { get; set; }
        public int LearnerId { get; set; }
    }
}
