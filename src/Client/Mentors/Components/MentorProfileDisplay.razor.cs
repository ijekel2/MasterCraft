using MasterCraft.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Mentors.Components
{
    public partial class MentorProfileDisplay : ComponentBase
    {
        [Parameter]
        public MentorProfile Profile { get; set; }

        public List<Offering> Offerings { get; set; }
    }
}
