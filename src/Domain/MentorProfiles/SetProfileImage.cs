using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Domain.Common.RequestHandling;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.MentorProfiles
{
    public class SetProfileImage : RequestHandler<SetProfileImageRequest, Empty>
    {
        readonly IDbContext cDbContext;
        readonly IFileStorage cFileStorage;

        public SetProfileImage(IDbContext dbContext, IFileStorage fileStorage, RequestHandlerService handlerService) : base(handlerService)
        {
            cDbContext = dbContext;
            cFileStorage = fileStorage;
        }

        internal override async Task<Empty> Handle(SetProfileImageRequest request, CancellationToken token = new())
        {
            MentorProfile? profile = cDbContext.MentorProfiles.FirstOrDefault(profile => profile.Id == request.ProfileId);

            if (profile is not null)
            {
                using MemoryStream stream = new(request.Image);
                Uri filePath = await cFileStorage.SaveFileAsync(stream);
                profile.ProfileImageUrl = filePath.AbsolutePath;
                await cDbContext.SaveChangesAsync();
            }

            return Empty.Value;
        }

        internal override Task Validate(SetProfileImageRequest request, Validator validator, CancellationToken token = new())
        {
            return Task.CompletedTask;
        }
    }

    public class SetProfileImageRequest
    {
        public byte[] Image { get; set; } = null!;

        public int ProfileId { get; set; }
    }
}
