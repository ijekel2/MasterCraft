using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Api
{
    public class TestResponse<TResponse>
    {
        public TestResponse()
        {
            Headers = new Headers();
        }

        public TResponse Response { get; set; }

        public ProblemDetails ErrorDetails { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public bool Success { get; set; }

        public Headers Headers { get; set; }
    }

    public class Headers
    {
        public string Location { get; set; }
    }
}