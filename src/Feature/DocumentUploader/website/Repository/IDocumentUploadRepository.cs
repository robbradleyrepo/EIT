namespace LionTrust.Feature.DocumentUploader.Repository
{
    using LionTrust.Feature.DocumentUploader.Models;

    public interface IDocumentUploadRepository
    {
        UploadedDocumentsResult UploadDocuments(DocumentUploaderViewModel documentUploadEntity);
    }
}
