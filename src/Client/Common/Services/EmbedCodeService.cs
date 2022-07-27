using System.Text.RegularExpressions;

namespace MasterCraft.Client.Common.Services
{
    public class EmbedCodeService
    {
        public string ParseSourceUrl(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }

            Regex regex = new Regex("src=\"(.*?)\"");
            Match match = regex.Match(html);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                throw new System.Exception("Invalid input");
            }
        }
    }
}
