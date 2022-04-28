using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Links
{
    public class OnboardingLinkTests : TestBase
    {
        [Test]
        public async Task ShouldCreateOnboardingLink()
        {
            string accountId = await StripeHelper.CreateConnectedAccount();

            CreateOnboardingLinkVm request = new()
            {
                AccountId = accountId,
                RefreshUrl = TestConstants.TestMentor.ChannelLink,
                SuccessUrl = TestConstants.TestMentor.ChannelLink
            };

            TestResponse<OnboardingLinkVm> response = await TestApi.PostJsonAsync<CreateOnboardingLinkVm, OnboardingLinkVm>(
                "links/onboarding",
                request);

            Assert.IsTrue(response.Success);
            Assert.IsNotEmpty(response.Response.Url);
        }
    }
}
