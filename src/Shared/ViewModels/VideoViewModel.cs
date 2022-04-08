﻿using MasterCraft.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class VideoViewModel
    {
        public int MentorId { get; set; }

        public int LearnerId { get; set; }

        public int FeedbackRequestId { get; set; }

        public VideoType VideoType { get; set; }

        public string Url { get; set; }

    }
}