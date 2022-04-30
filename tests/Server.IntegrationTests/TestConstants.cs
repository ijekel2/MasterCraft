using MasterCraft.Shared.ViewModels;

namespace MasterCraft.Server.IntegrationTests
{
    public class TestConstants
    {
        public static ApplicationUserVm TestUser => new()
        {
            FirstName = "Patrick",
            LastName = "O'Sullivan",
            Username = "mastercraftdev@outlook.com",
            Email = "mastercraftdev@outlook.com",
        };

        public static MentorVm TestMentor => new()
        {
            FirstName = TestUser.FirstName,
            LastName = TestUser.LastName,
            ChannelName = "The Wandering DP",
            ChannelLink = "https://www.mastercraft.cc/",
            PersonalTitle = "Cinematographer",
            ProfileCustomUri = "patrick-o-sullivan",
            VideoEmbedCode = "<iframe title=\"vimeo - player\" src=\"https://player.vimeo.com/video/599902843?h=4e0d4221a9\" width=\"640\" height=\"360\" frameborder=\"0\" allowfullscreen></iframe>"
        };

        public static LearnerVm TestLearner => new()
        {
            ProfileImageUrl = "myimageurl"
        };

        public static OfferingVm TestOffering => new()
        {
            DeliveryDays = 7,
            RequestMinutes = 3,
            FeedbackMinutes = 10,
            Price = 100,
            SampleQuestion1 = "What is wrong with my lighting?",
            SampleQuestion2 = "Do you think this commercial is effective?",
            SampleQuestion3 = "How can improve my composition?",
            SampleQuestion4 = "How do I make this better?",
            SampleQuestion5 = "Why do my images look so flat?",
            MentorId = TestUser.Id
        };

        public static FeedbackRequestVm TestFeedbackRequest => new()
        {
            Status = Shared.Enums.FeedbackRequestStatus.Pending,
            ContentLink = "Test Link"
        };

        public static VideoVm TestVideo => new()
        {
            VideoType = Shared.Enums.VideoType.FeedbackRequest,
            Url = "MyFileUrl"

        };

        public static BankAccountVm TestBankAccount => new()
        {
            Country = "US",
            Currency = "USD",
            RoutingNumber = "110000000",
            AccountNumber = "000123456789"
        };

        public static readonly string TestImage = "Content\\TestPic.png";
        public static readonly string TestPassword = "mentor!123";

    }
}
