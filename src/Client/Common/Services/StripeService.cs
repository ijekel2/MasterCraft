using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Shared.ViewModels;
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

        public StripeService(NavigationManager navigation, ApiClient api, ILocalStorageService localStorage)
        {
            _navigation = navigation;
            _api = api;
            _localStorage = localStorage;
        }

        public async Task RedirectToVendorOnboarding(string username, string returnUrl)
        {
            CreateOnboardingLinkVm request = new()
            {
                AccountId = await GetStripeAccountId(username),
                RefreshUrl = _navigation.ToAbsoluteUri(returnUrl).AbsoluteUri,
                SuccessUrl = _navigation.ToAbsoluteUri(returnUrl).AbsoluteUri
            };

            ApiResponse<OnboardingLinkVm> response = await _api.PostAsync<CreateOnboardingLinkVm, OnboardingLinkVm>(
                "links/onboarding",
                request);

            //-- Redirect to our stripe link
            _navigation.NavigateTo(response.Response.Url);
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
