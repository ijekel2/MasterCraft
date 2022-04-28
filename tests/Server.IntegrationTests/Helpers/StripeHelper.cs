using MasterCraft.Domain.Interfaces;
using MasterCraft.Shared.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Server.IntegrationTests.Helpers
{
    public class StripeHelper
    {
        IPaymentService _paymentService;

        public StripeHelper(IServiceProvider serviceProvider)
        {
            _paymentService = serviceProvider.GetRequiredService<IPaymentService>();
        }

        public async Task<string> CreateConnectedAccount()
        {
            MentorVm testMentor = new()
            {
                Email = TestConstants.TestUser.Email,
                ChannelLink = TestConstants.TestMentor.ChannelLink
            };

            return await _paymentService.CreateConnectedAccount(testMentor);
        }

        public async Task<string> CreateCustomer()
        {
            LearnerVm testLearner = new()
            {
                Email = TestConstants.TestUser.Email,
            };

            return await _paymentService.CreateCustomer(testLearner);
        }

        public async Task<string> CreateOnboardedConnectedAccount()
        {
            MentorVm testMentor = new()
            {
                Email = TestConstants.TestUser.Email,
                ChannelLink = TestConstants.TestMentor.ChannelLink
            };

            string accountId = await _paymentService.CreateConnectedAccount(testMentor);

            //-- Update account with info needed for it to be used.
            var options = new AccountUpdateOptions
            {
                Metadata = new Dictionary<string, string>
                {

                },
            };
            var service = new AccountService();
            await service.UpdateAsync(accountId, options);

            return accountId;
        }
    }
}
