using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Common.Api
{
    public class ApiResponse<TResponse>
    {
        public TResponse Response { get; set; }

        public ProblemDetails ErrorDetails { get; set; }
    }
}