using MasterCraft.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Stripe;
using MasterCraft.Shared.ViewModels;
using Stripe.Checkout;

namespace MasterCraft.Infrastructure.Payments
{
    internal class StripePaymentService : IPaymentService
    {
        public async Task<string> CreateConnectedAccount(MentorVm mentor, CancellationToken token = default)
        {
            var options = new AccountCreateOptions
            {
                Type = "express",
                
                Email = mentor.Email,
                Capabilities = new AccountCapabilitiesOptions
                {
                    CardPayments = new AccountCapabilitiesCardPaymentsOptions
                    {
                        Requested = true,
                    },
                    Transfers = new AccountCapabilitiesTransfersOptions
                    {
                        Requested = true,
                    }
                },
                BusinessType = "individual",
                Individual = new AccountIndividualOptions
                {
                    FirstName = mentor.FirstName,
                    LastName = mentor.LastName
                },
                BusinessProfile = new AccountBusinessProfileOptions
                {
                    Url = mentor.ChannelLink,
                    Mcc = "8299" //-- MCC code for Educational Services
                },
                Settings = new AccountSettingsOptions
                {
                    Branding = new AccountSettingsBrandingOptions
                    {
                        PrimaryColor = "#00765A",
                        SecondaryColor = "#3D3D3D"
                    }
                }
            };

            var service = new AccountService();
            Account account = await service.CreateAsync(options, cancellationToken: token);
            return account.Id;
        }

        public async Task<OnboardingLinkVm> CreateOnboardingLink(CreateOnboardingLinkVm request, CancellationToken token = default)
        {
            var options = new AccountLinkCreateOptions
            {
                Account = request.AccountId,
                RefreshUrl = request.RefreshUrl,
                ReturnUrl = request.SuccessUrl,
                Type = "account_onboarding",
            };
      
            var service = new AccountLinkService();
            AccountLink accountLink = await service.CreateAsync(options, cancellationToken: token);
            return new OnboardingLinkVm()
            {
                Url = accountLink.Url
            };
        }

        public async Task<CheckoutVm> CreateCheckout(CreateCheckoutVm request, CancellationToken token = default)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = request.SuccessUrl,
                CancelUrl = request.CancelUrl,
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)request.Price * 10,
                            Currency = request.Currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = request.OfferingName,
                                Description = request.OfferingDescription
                            }
                        },
                        Quantity = 1
                    },
                },
                PaymentIntentData = new SessionPaymentIntentDataOptions()
                {
                    ApplicationFeeAmount = (long)request.ApplicationFee * 10,
                    CaptureMethod = "manual",
                    SetupFutureUsage = "on_session",
                },
     
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "payment",
            };

            var requestOptions = new RequestOptions
            {
                StripeAccount = request.AccountId
            };

            //-- Create Checkout
            var service = new SessionService();
            Session session = await service.CreateAsync(options, requestOptions, token);
            return new CheckoutVm()
            {
                CheckoutSessionId = session.Id,
                CheckoutUrl = session.Url,
            };
        }

        public async Task<string> CreateCustomer(LearnerVm learner, CancellationToken token = default)
        {
            var options = new CustomerCreateOptions
            {
                Description = learner.Email,
            };

            var service = new CustomerService();
            Customer customer = await service.CreateAsync(options);
            return customer.Id;
        }

    }
}
