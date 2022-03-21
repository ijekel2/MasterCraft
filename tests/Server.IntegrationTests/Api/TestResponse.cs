using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Api
{
    public class TestResponse<TResponse>
    {
        public TResponse Response { get; set; }

        public ProblemDetails ErrorDetails { get; set; }

        public bool Success => Response is not null;
    }
}