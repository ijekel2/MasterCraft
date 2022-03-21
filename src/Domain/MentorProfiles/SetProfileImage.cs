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
    public class SetProfileImage : RequestHandler<SetProfileImageRequest, Empty>
    {
        readonly IDbContext cDbContext;

        public SetProfileImage(IDbContext dbContext, RequestHandlerService handlerService) : base(handlerService)
        {
            cDbContext = dbContext;
        }

        internal override Task<Empty> Handle(SetProfileImageRequest request, CancellationToken token = new())
        {
            throw new NotImplementedException();
        }

        internal override Task Validate(SetProfileImageRequest request, Validator validator, CancellationToken token = new())
        {
            throw new NotImplementedException();
        }
    }

    public class SetProfileImageRequest
    {
        public byte[] Image { get; set; } = null!;

        public int ProfileId { get; set; }
    }
}
