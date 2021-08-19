namespace LionTrust.Feature.DocumentUploader.Models
{
    public class UploadedFileViewModel
    {
        public string FileName { get; set; }
        public string FileAsBinary { get; set; }
        public bool SitecoreMediaItemOverwrite { get; set; }
    }
}