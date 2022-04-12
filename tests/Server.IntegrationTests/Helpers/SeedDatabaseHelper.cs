using MasterCraft.Domain.Entities;
using MasterCraft.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Helpers
{
    public class SeedDatabaseHelper
    {
        ApplicationDbContext _dbContext;

        public SeedDatabaseHelper(ApplicationDbContext pDbContext)
        {
            _dbContext = pDbContext;
        }

        public async Task<Mentor> SeedTestMentor()
        {
            Mentor mentor = TestConstants.TestMentor;
            await SeedDatabase(mentor);

            return mentor;
        }

        public async Task<Learner> SeedTestLeaner()
        {
            Learner learner = TestConstants.TestLearner;
            await SeedDatabase(learner);

            return learner;
        }

        public async Task<Offering> SeedTestOffering()
        {
            Mentor mentor = TestConstants.TestMentor;
            Offering offering = TestConstants.TestOffering;

            await SeedDatabase(mentor);

            offering.MentorId = mentor.Id;
            await SeedDatabase(offering);

            return offering;
        }

        public async Task<FeedbackRequest> SeedTestFeedbackRequest()
        {
            Mentor mentor = TestConstants.TestMentor;
            Learner learner = TestConstants.TestLearner;
            Offering offering = TestConstants.TestOffering;
            FeedbackRequest request = TestConstants.TestFeedbackRequest;

            await SeedDatabase(mentor);
            await SeedDatabase(learner);

            offering.MentorId = mentor.Id;
            await SeedDatabase(offering);

            request.MentorId = mentor.Id;
            request.LearnerId = learner.Id;
            request.OfferingId = offering.Id;
            await SeedDatabase(request);

            return request;
        }

        public async Task<Video> SeedTestVideo()
        {
            Mentor mentor = TestConstants.TestMentor;
            Learner learner = TestConstants.TestLearner;
            Offering offering = TestConstants.TestOffering;
            FeedbackRequest request = TestConstants.TestFeedbackRequest;
            Video video = TestConstants.TestVideo;

            await SeedDatabase(mentor);
            await SeedDatabase(learner);

            offering.MentorId = mentor.Id;
            await SeedDatabase(offering);

            request.MentorId = mentor.Id;
            request.LearnerId = learner.Id;
            request.OfferingId = offering.Id;
            await SeedDatabase(request);

            video.MentorId = mentor.Id;
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
    }
}
