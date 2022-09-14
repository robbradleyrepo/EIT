using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;

namespace LionTrust.Feature.DocumentUploader.Models
{
    /// <summary>
    /// ViewModel for document uploading SPEAK page 
    /// </summary>
    public class LTAdminDocumentUploadViewModel : EntityIdentity
    {
        public LTAdminDocumentUploadViewModel()
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
