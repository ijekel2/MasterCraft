using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Mentors
{
    public class SetProfileImageService : DomainService<SetProfileImageRequest, Empty>
    {
        readonly IDbContext cDbContext;
        readonly IFileStorage cFileStorage;

        public SetProfileImageService(IDbContext dbContext, IFileStorage fileStorage, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            cDbContext = dbContext;
            cFileStorage = fileStorage;
        }

        internal override async Task<Empty> Handle(SetProfileImageRequest request, CancellationToken token = new())
        {
            Mentor? mentor = cDbContext.Mentors.FirstOrDefault(profile => profile.Id == request.ProfileId);

            if (mentor is not null)
            {
                using MemoryStream stream = new(request.Image);
                Uri filePath = await cFileStorage.SaveFileAsync(stream);
                mentor.ProfileImageUrl = filePath.AbsolutePath;
                await cDbContext.SaveChangesAsync(token);
            }

            return Empty.Value;
        }

        internal override Task Validate(SetProfileImageRequest request, DomainValidator validator, CancellationToken token = new())
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
