using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests
{
    public class TestConstants
    {
        public static ApplicationUser TestUser => new()
        {
            FirstName = "test",
            LastName = "mentor",
            Username = "mentor@local",
            Email = "mentor@local",
            Password = "mentor!123",
        };

        public static Mentor TestMentor => new()
        {
            ChannelName = "The Testy Tester",
            ChannelLink = "Test Link",
            PersonalTitle = "Tester",
            ProfileCustomUri = "test-mentor"
        };

        public static Learner TestLearner => new()
        {
            ProfileImageUrl = "myimageurl"
        };

        public static Offering TestOffering => new()
        {
            Name = "Test Feedback",
            Description = "I offer you great test feedback.",
            DeliveryDays = 30,
            FeedbackMinutes = 10,
            Price = 50,
            SampleQuestion1 = "How can I improve my tests?",
            SampleQuestion2 = "How can I test my tests?",
            SampleQuestion3 = "Why are my tests not working",
            MentorId = TestMentor.Id
        };

        public static FeedbackRequest TestFeedbackRequest => new()
        {
            Status = Shared.Enums.FeedbackRequestStatus.Pending,
            ContentLink = "Test Link"
        };

        public static Video TestVideo => new()
        {
            VideoType = Shared.Enums.VideoType.FeedbackRequest,
            Url = "MyFileUrl"
            
        };

        public static readonly string TestImage = "Content\\TestPic.png";

    }
}
