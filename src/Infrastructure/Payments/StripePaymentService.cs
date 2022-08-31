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
using MasterCraft.Shared.ViewModels.Aggregates;

namespace MasterCraft.Infrastructure.Payments
{
    internal class StripePaymentService : IPaymentService
    {
        public async Task<string> CreateConnectedAccount(UserVm user, CancellationToken token = default)
        {
            var options = new AccountCreateOptions
            {
                Type = "express",
                
                Email = user.Email,
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
                    FirstName = user.FirstName,
                    LastName = user.LastName
                },
                BusinessProfile = new AccountBusinessProfileOptions
                {
                    ProductDescription = "Content Feedback",
                    Mcc = "8299", //-- MCC code for Educational Services
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

        public async Task<CheckoutSessionVm> CreateCheckout(CheckoutDetailsVm request, CancellationToken token = default)
        {
            long priceCents = (long)(request.Offering.Price * 100);
            long chargeCents = (long)(request.ServiceCharge * 100);
            long appFeeCents = (long)(request.ApplicationFee * 100);

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
                            UnitAmount = priceCents,
                            Currency = request.Currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Feedback"
                            }
                        },
                        Quantity = 1
                    },
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = chargeCents,
                            Currency = request.Currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Service Charge",
                            }
                        },
                        Quantity = 1
                    }
                },
                PaymentIntentData = new SessionPaymentIntentDataOptions()
                {
                    ApplicationFeeAmount = appFeeCents,
                    CaptureMethod = "manual",
                    SetupFutureUsage = "on_session",
                },
     
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "payment",
                CustomerEmail = request.CustomerEmail 
            };

            var requestOptions = new RequestOptions
            {
                StripeAccount = request.AccountId
            };

            //-- Create Checkout
            var service = new SessionService();
            Session session = await service.CreateAsync(options, requestOptions, token);
            return new CheckoutSessionVm()
            {
                CheckoutSessionId = session.Id,
                CheckoutUrl = session.Url,
                PaymentIntentId = session.PaymentIntentId
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

        public async Task CapturePayment(string paymentIntentId, string merchantAccountId)
        {
            var service = new PaymentIntentService();
            RequestOptions options = new RequestOptions()
            {
                StripeAccount = merchantAccountId,
            };

            await service.CaptureAsync(paymentIntentId, requestOptions: options);
        }

        public async Task CancelPayment(string paymentIntentId, string merchantAccountId)
        {
            var service = new PaymentIntentService();
            RequestOptions options = new RequestOptions()
            {
                StripeAccount = merchantAccountId,
            };

            await service.CancelAsync(paymentIntentId, requestOptions: options);
        }
    }
}
