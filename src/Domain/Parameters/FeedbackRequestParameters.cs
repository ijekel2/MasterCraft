﻿using MasterCraft.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Parameters
{
    public class FeedbackRequestParameters : QueryStringParameters
    {
        public string MentorId { get; set; }
        public string LearnerId { get; set; }
        public FeedbackRequestStatus Status { get; set; }
    }
}
