using LionTrust.Feature.DocumentUploader.Models;
using Sitecore.Services.Core;

namespace LionTrust.Feature.DocumentUploader.Repository
{
    /// <summary>
    /// Repository actions interface document upload module in sitecore
    /// </summary>
    /// <typeparam name="T"></typeparam> 
    public interface ILTAdminDocumentUploadRepository<T> : IRepository<T> where T : IEntityIdentity
    {        
        LTAdminDocumentUploadViewModel LoadDocumentUploadPage();
        LTAdminDocumentUploadViewModel UploadDocuments(LTAdminDocumentUploadViewModel documentUploadEntity);
    }
}
