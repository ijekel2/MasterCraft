using MasterCraft.Domain.Entities;
using MasterCraft.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MasterCraft.Shared.ViewModels;

namespace MasterCraft.Server.IntegrationTests.Helpers
{
    public class SeedDatabaseHelper
    {
        ApplicationDbContext _dbContext;

        public SeedDatabaseHelper(IServiceScope pServiceScope)
        {
            _dbContext = pServiceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); ;
        }

        public async Task<Mentor> SeedTestMentor()
        {            
            Mentor mentor = Map<MentorVm, Mentor>(TestConstants.TestMentor);
            await SeedDatabase(mentor);

            return mentor;
        }


        public async Task<Offering> SeedTestOffering()
        {
            Mentor mentor = Map<MentorVm, Mentor>(TestConstants.TestMentor);
            Offering offering = Map<OfferingVm, Offering>(TestConstants.TestOffering);

            await SeedDatabase(mentor);

            offering.MentorId = mentor.UserId;
            await SeedDatabase(offering);

            return offering;
        }

        public async Task<FeedbackRequest> SeedTestFeedbackRequest()
        {
            Mentor mentor = Map<MentorVm, Mentor>(TestConstants.TestMentor);
            User learner = Map<UserVm, User>(TestConstants.TestUser);
            Offering offering = Map<OfferingVm, Offering>(TestConstants.TestOffering);
            FeedbackRequest request = Map<FeedbackRequestVm, FeedbackRequest>(TestConstants.TestFeedbackRequest);

            await SeedDatabase(mentor);
            await SeedDatabase(learner);

            offering.MentorId = mentor.UserId;
            await SeedDatabase(offering);

            request.MentorId = mentor.UserId;
            request.LearnerId = learner.Id;
            request.OfferingId = offering.Id;
            await SeedDatabase(request);

            return request;
        }

        public async Task<Video> SeedTestVideo()
        {
            Mentor mentor = Map<MentorVm, Mentor>(TestConstants.TestMentor);
            User learner = Map<UserVm, User>(TestConstants.TestUser);
            Offering offering = Map<OfferingVm, Offering>(TestConstants.TestOffering);
            FeedbackRequest request = Map<FeedbackRequestVm, FeedbackRequest>(TestConstants.TestFeedbackRequest);
            Video video = Map<VideoVm, Video>(TestConstants.TestVideo);

            await SeedDatabase(mentor);
            await SeedDatabase(learner);

            offering.MentorId = mentor.UserId;
            await SeedDatabase(offering);

            request.MentorId = mentor.UserId;
            request.LearnerId = learner.Id;
            request.OfferingId = offering.Id;
            await SeedDatabase(request);

            video.MentorId = mentor.UserId;
            video.LearnerId = learner.Id;
            video.FeedbackRequestId = request.Id;
            await SeedDatabase(video);

            return video;
        }

        protected async Task SeedDatabase<TEntity>(params TEntity[] records)
        {
            foreach (TEntity record in records)
            {
                await _dbContext.AddAsync(record);
            }

            await _dbContext.SaveChangesAsync();
        }

        protected static TDestination Map<TSource, TDestination>(TSource source)
        {
            var mapperConfig = new MapperConfiguration(config => config.CreateMap<TSource, TDestination>());
            TDestination destination = mapperConfig.CreateMapper().Map<TDestination>(source);
            return destination;
        }
    }
}
