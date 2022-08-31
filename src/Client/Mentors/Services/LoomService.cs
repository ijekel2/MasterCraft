using MasterCraft.Client.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace MasterCraft.Client.Mentors.Services
{
    public class LoomService
    {
        IJSRuntime _js { get; set; }

        public LoomService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<string> GetVideoHtml(string videoUrl)
        {
            LoomEmbedInfo embedInfo = await _js.InvokeAsync<LoomEmbedInfo>("LoomService.getVideoEmbedHtml", new object[] { videoUrl, 800 });            
            return embedInfo.html;
        }
    }
}
