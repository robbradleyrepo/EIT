namespace LionTrust.Feature.Listings.Models
{
    public class MediaFileModel
    {
        public string MediaName { get; set; }
        public string Name { get; set; }
        public long Length { get; set; }
        public byte[] Bytes { get; set; }
    }
}