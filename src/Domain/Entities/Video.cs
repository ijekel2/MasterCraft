﻿using MasterCraft.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Entities
{
    public class Video : BaseEntity
    {
        public int Id { get; set; }

        public int MentorId { get; set; }

        public int LearnerId { get; set; }

        public int FeedbackRequestId { get; set; }

        public string Uri { get; set; }

        public VideoType VideoType { get; set; }

    }
}
