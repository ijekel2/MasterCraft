using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.FeedbackRequests
{
    public class GetFeedbackRequestTests : TestBase
    {
        [Test]
        public async Task ShouldReturnFeedbackRequestForId()
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
            await SeedDatabase(TestConstants.TestFeedbackRequest);

            TestResponse<Mentor> response = await TestApi.GetAsync<Mentor>("offerings/1");
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Response);
            Assert.AreEqual(1, response.Response.Id);
        }
    }
}
