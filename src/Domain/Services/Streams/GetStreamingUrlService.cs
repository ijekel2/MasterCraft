using MasterCraft.Domain.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Streams
{
    public class GetStreamingUrlService : DomainService<string, string> 
    {
        public GetStreamingUrlService(DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        internal override async Task<string> Handle(string request, CancellationToken token = default)
        {
            //-- This is where we will communicate with our streaming service. For now we just return our homemade streaming endpoint.
            return string.Empty;
        }

        internal override async Task Validate(string request, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
