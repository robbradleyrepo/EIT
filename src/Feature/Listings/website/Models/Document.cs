namespace LionTrust.Feature.Listings.Models
{
    public class Document
    {
        public string DocumentName { get; set; }
        public string Name { get; set; }
        public long Length { get; set; }
        public byte[] Bytes { get; set; }
        public string DocumentExtension { get; set; }
    }
}