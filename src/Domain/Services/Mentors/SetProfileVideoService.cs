using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Mentors
{
    public class SetProfileVideoService : DomainService<SetProfileVideoRequest, Empty>
    {
        readonly IDbContext cDbContext;

        public SetProfileVideoService(IDbContext dbContext, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            cDbContext = dbContext;
        }

        internal override Task<Empty> Handle(SetProfileVideoRequest request, CancellationToken token = new())
        {
            throw new NotImplementedException();
        }

        internal override Task Validate(SetProfileVideoRequest request, DomainValidator validator, CancellationToken token = new())
        {
            throw new NotImplementedException();
        }
    }

    public class SetProfileVideoRequest
    {
        public byte[] Video { get; set; } = null!;

        public int ProfileId { get; set; }
    }
}
