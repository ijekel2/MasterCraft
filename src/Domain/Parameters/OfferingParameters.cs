﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Parameters
{
    public class OfferingParameters : QueryStringParameters
    {
        public int MentorId { get; set; }
    }
}
