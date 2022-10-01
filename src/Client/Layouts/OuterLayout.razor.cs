using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MasterCraft.Client.Layouts
{
    public partial class OuterLayout : LayoutComponentBase
    {
        private ErrorBoundary errorBoundary;

        private void Recover()
        {
            errorBoundary.Recover();
        }
    }
}
