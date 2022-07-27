using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterCraft.Client.Common.Services
{
    public class StripeService
    {
        private NavigationManager _navigation;
        private ApiClient _api;
        private ILocalStorageService _localStorage;
        private CurrentUserService _currentUser;

        public object TestConstants { get; private set; }

        public StripeService(NavigationManager navigation, ApiClient api, 
            ILocalStorageService localStorage, CurrentUserService currentUser)
        {
            _navigation = navigation;
            _api = api;
            _localStorage = localStorage;
            _currentUser = currentUser;
        }

        public async Task RedirectToVendorOnboarding(string userId, string refreshUrl, string successUrl)
        {
            CreateOnboardingLinkVm request = new()
            {
                AccountId = await GetStripeAccountId(userId),
                RefreshUrl = _navigation.ToAbsoluteUri(refreshUrl).AbsoluteUri,
                SuccessUrl = _navigation.ToAbsoluteUri(successUrl).AbsoluteUri
            };

            ApiResponse<OnboardingLinkVm> response = await _api.PostAsync<CreateOnboardingLinkVm, OnboardingLinkVm>(
                "links/onboarding",
                request);

            //-- Redirect to our stripe link
            _navigation.NavigateTo(response.Response.Url);
        }

        public async Task RedirectToCheckout(string accountId, string cancelUrl, string successUrl, 
            FeedbackRequestVm request, OfferingVm offering)
        {
            CheckoutDetailsVm checkoutDetails = new CheckoutDetailsVm()
            {
                AccountId = accountId, //-- Get account number from mentorvm,
                CancelUrl = _navigation.ToAbsoluteUri(cancelUrl).AbsoluteUri,
                SuccessUrl = _navigation.ToAbsoluteUri(successUrl).AbsoluteUri,
                CustomerEmail = (await _currentUser.GetCurrentUser()).Email,
                FeedbackRequest = request,
                Offering = offering
            };

            //-- Send create mentor request and validate the response.
            ApiResponse<CheckoutSessionVm> response = await _api.PostAsync<CheckoutDetailsVm, CheckoutSessionVm>(
                "checkouts",
                checkoutDetails);

            //-- Redirect to our stripe link
            _navigation.NavigateTo(response.Response.CheckoutUrl);
        }

        private async Task<string> GetStripeAccountId(string username)
        {
            Dictionary<string, string> accountIds = await GetStripeAccountIds();

            return accountIds[username];
        }

        public async Task SetStripeAccountId(string username, string acctId)
        {
            Dictionary<string, string> accountIds = await GetStripeAccountIds();

            accountIds[username] = acctId;

            await _localStorage.SetItemAsync("stripeAccounts", accountIds);
        }

        private async Task<Dictionary<string, string>> GetStripeAccountIds()
        {
            Dictionary<string, string> accountIds = await _localStorage.GetItemAsync<Dictionary<string, string>>("stripeAccounts");

            if (accountIds is null)
            {
                accountIds = new Dictionary<string, string>();
            }

            return accountIds;
        }

    }
}
