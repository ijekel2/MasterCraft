using MasterCraft.Domain.Entities;
using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Checkouts
{
    public class CreateCheckoutTests : TestBase
    {
        [Test]
        public async Task ShouldCreateCheckoutSessionWithUrl()
        {
            string accountId = await StripeHelper.CreateConnectedAccount();

            CheckoutDetailsVm request = new CheckoutDetailsVm()
            {
                AccountId = accountId,
                CancelUrl = TestConstants.TestUrl,
                SuccessUrl = TestConstants.TestUrl,
                Offering = new OfferingVm() { Price = TestConstants.TestOffering.Price }
            };

            //-- Send create mentor request and validate the response.
            TestResponse<CheckoutSessionVm> response = await TestApi.PostJsonAsync<CheckoutDetailsVm, CheckoutSessionVm>(
                "checkouts",
                request);

            Assert.IsTrue(response.Success);
            Assert.IsNotEmpty(response.Response.CheckoutUrl);
        }
    }
}
