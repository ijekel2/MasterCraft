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
        readonly IDbContext _dbContext;
        readonly IFileStorage _fileStorage;

        public SetProfileImageService(IDbContext dbContext, IFileStorage fileStorage, ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
            _fileStorage = fileStorage;
        }

        internal override async Task<Empty> Handle(SetProfileImageRequest request, CancellationToken token = new())
        {
            Mentor? mentor = _dbContext.Mentors.FirstOrDefault(profile => profile.Id == request.ProfileId);

            if (mentor is not null)
            {
                using MemoryStream stream = new(request.Image);
                Uri filePath = await _fileStorage.SaveFileAsync(stream);
                mentor.ProfileImageUrl = filePath.AbsolutePath;
                await _dbContext.SaveChangesAsync(token);
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
