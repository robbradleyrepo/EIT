namespace LionTrust.Feature.Listings.Models
{
    public class MediaItemResponseModel
    {
        public ImageResponseModel HeadShotImage { get; set; }
        public ImageResponseModel FullBodyImage { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
    }
}