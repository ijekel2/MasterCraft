namespace MasterCraft.Client.Shared.Models
{
    public struct LoomVideo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public string SharedUrl { get; set; }
        public string EmbedUrl { get; set; }
        public float ThumbnailHeight { get; set; }
        public float ThumbnailWidth { get; set; }
        public float Duration { get; set; }
        public string ProviderUrl { get; set; }
    }
}
