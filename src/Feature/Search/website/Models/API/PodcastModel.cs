namespace LionTrust.Feature.Search.Models.API
{
    using System.Collections.Generic;

    public class PodcastModel
    {
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string BackgroundImageUrl { get; set; }
        public string BackgroundImageOpacity { get; set; }
        public string MobileBackgroundImageUrl { get; set; }
        public string PodcastsLabel { get; set; }
        public IEnumerable<PodcastLink> PodcastLinks { get; set; }
    }
}