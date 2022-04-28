using MasterCraft.Server.IntegrationTests.Api;
using MasterCraft.Shared.ViewModels;
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
            string customerId = await StripeHelper.CreateCustomer();

            await Task.Delay(10000);
             
            CreateCheckoutVm request = new CreateCheckoutVm()
            {
                AccountId = accountId,
                CancelUrl = TestConstants.TestMentor.ChannelLink,
                SuccessUrl = TestConstants.TestMentor.ChannelLink,
                OfferingName = TestConstants.TestOffering.Name,
                OfferingDescription = TestConstants.TestOffering.Description,
                Price = TestConstants.TestOffering.Price
            };

            //-- Send create mentor request and validate the response.
            TestResponse<CheckoutVm> response = await TestApi.PostJsonAsync<CreateCheckoutVm, CheckoutVm>(
                "checkouts",
                request);

            Assert.IsTrue(response.Success);
            Assert.IsNotEmpty(response.Response.CheckoutUrl);
        }
    }
}
