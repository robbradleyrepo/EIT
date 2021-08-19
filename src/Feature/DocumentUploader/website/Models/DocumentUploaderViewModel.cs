namespace LionTrust.Feature.DocumentUploader.Models
{
    using Sitecore.Services.Core.Model;
    using System;
    using System.Collections.Generic;

    public class DocumentUploaderViewModel : EntityIdentity
    {
        public DocumentUploaderViewModel()
        {
            UploadedFiles = new List<UploadedFileViewModel>();
        }

        public IDictionary<Guid, string> DocumentTypes { get; set; }
        public Guid DocTypeIdForFactsheet { get; set; }
        public Guid DocTypeIdForKiid { get; set; }
        public string SeletedDocumentType { get; set; }
        public List<UploadedFileViewModel> UploadedFiles { get; set; }
        public bool UploadSuccess { get; set; }
        public IDictionary<string, string> UploadErrorDictionary { get; set; }
    }
}