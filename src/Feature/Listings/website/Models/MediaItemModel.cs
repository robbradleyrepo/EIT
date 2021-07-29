namespace LionTrust.Feature.Listings.Models
{
    public class MediaItemModel
    {
        public ImageModel HeadShotImage { get; set; }
        public ImageModel FullBodyImage { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
    }
}