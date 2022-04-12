using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Parameters;
using MasterCraft.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Videos
{
    public class ListVideosService : DomainService<VideoParameters, List<VideoVm>>
    {
        readonly IDbContext _dbContext;

        public ListVideosService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<List<VideoVm>> Handle(VideoParameters parameters, CancellationToken token = default)
        {
            IQueryable<Video> list = _dbContext.Videos;

            if (parameters.FeedbackRequestId != 0)
            {
                list = list.Where(video => video.FeedbackRequestId == parameters.FeedbackRequestId);
            }
            else
            {
                if (parameters.MentorId != 0)
                {
                    list = list.Where(video => video.MentorId == parameters.MentorId);
                }

                if (parameters.LearnerId != 0)
                {
                    list = list.Where(video => video.LearnerId == parameters.LearnerId);
                }
            }
            
            List<Video> videos = await PagedList(list, parameters, token);
            return videos.Select(video => Map<Video, VideoVm>(video)).ToList();
        }

        internal override async Task Validate(VideoParameters parameters, DomainValidator validator, CancellationToken token = default)
        {
            await Task.CompletedTask;
        }
    }
}
