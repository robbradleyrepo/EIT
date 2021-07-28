namespace LionTrust.Foundation.Contact.Models.Types
{
    public class Image
    {
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public string Path { get; set; }
        public string AltText { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Path);
        }
    }
}
