using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Domain.Common.RequestHandling;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.MentorProfiles
{
    public class SetProfileVideo : RequestHandler<SetProfileVideoRequest, Empty>
    {
        readonly IDbContext cDbContext;

        public SetProfileVideo(IDbContext dbContext, RequestHandlerService handlerService) : base(handlerService)
        {
            cDbContext = dbContext;
        }

        internal override Task<Empty> Handle(SetProfileVideoRequest request, CancellationToken token = new())
        {
            throw new NotImplementedException();
        }

        internal override Task Validate(SetProfileVideoRequest request, Validator validator, CancellationToken token = new())
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
