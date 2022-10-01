namespace MasterCraft.Client.Common.Models
{
    public struct LoomVideo
    {
        public string id { get; set; }
        public string title { get; set; }
        public float height { get; set; }
        public float width { get; set; }
        public string sharedUrl { get; set; }
        public string embedUrl { get; set; }
        public float thumbnailHeight { get; set; }
        public float thumbnailWidth { get; set; }
        public float duration { get; set; }
        public string providerUrl { get; set; }
    }
}
