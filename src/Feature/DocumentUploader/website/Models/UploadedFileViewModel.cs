namespace LionTrust.Feature.DocumentUploader.Models
{
    public class UploadedFileViewModel
    {
        public string FileName { get; set; }
        public string FundName { get; set; }
        public string FactsheetName { get; set; }
        public string ShareClassName { get; set; }
        public string LanguageCode { get; set; }
        public string FileAsBinary { get; set; }
        public bool SitecoreMediaItemOverwrite { get; set; }
    }
}