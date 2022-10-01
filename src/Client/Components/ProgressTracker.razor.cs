using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterCraft.Client.Components
{
    public partial class ProgressTracker : ComponentBase
    {
        private int currentProgressItem;
        private Dictionary<int, ProgressTrackerItem> progressTrackerItems;

        [Parameter]
        public List<ProgressTrackerItem> Items { get; set; }

        protected override void OnInitialized()
        {
            progressTrackerItems = new();

            int count = 0;
            foreach (var item in Items)
            {
                progressTrackerItems.Add(++count, item);
            }
        }

        public void UpdateProgressTracker(int number)
        {
            currentProgressItem = number;
            StateHasChanged();
        }

        public int GetCurrentItem()
        {
            return currentProgressItem;
        }

        private void OnProgressItemClick(int itemNumber)
        {
            if (currentProgressItem > itemNumber)
            {
                progressTrackerItems[itemNumber].OnClick();
            }
        }
    }

    public class ProgressTrackerItem
    {
        public string Label { get; set; }

        public Action OnClick { get; set; }
    }
}
