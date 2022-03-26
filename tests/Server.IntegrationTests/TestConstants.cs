using MasterCraft.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests
{
    public class TestConstants
    {
        public static readonly ApplicationUser TestMentor = new()
        {
            FirstName = "test",
            LastName = "mentor",
            Username = "mentor@local",
            Email = "mentor@local",
            Password = "mentor!123",
        };

        public static readonly MentorProfile TestMentorProfile = new()
        {
            ChannelName = "The Testy Tester",
            ChannelLink = "Test Link",
            PersonalTitle = "Tester",
            ProfileCustomUri = "test-mentor"
        };

        public static readonly string TestImage = "Content\\TestPic.png";

    }
}
