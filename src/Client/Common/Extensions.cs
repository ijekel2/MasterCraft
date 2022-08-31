using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Specialized;
using System.Web;

namespace MasterCraft.Client.Common
{
    public static class Extensions
    {
        public static string FormatPrice(this decimal price)
        {
            return string.Format("{0:C}", price);
        }

        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        public static string QueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString().Get(key);
        }
    }
}
