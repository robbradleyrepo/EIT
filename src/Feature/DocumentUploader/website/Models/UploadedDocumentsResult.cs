namespace LionTrust.Feature.DocumentUploader.Models
{
    using System.Collections.Generic;

    public class UploadedDocumentsResult
    {
        public bool Result { get; set; }
        public List<UploadedFileViewModel> DocumentsUploaded { get; set; }
    }
}