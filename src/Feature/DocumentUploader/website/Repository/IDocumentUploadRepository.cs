namespace LionTrust.Feature.DocumentUploader.Repository
{
    using System;
    using System.Collections.Generic;
    using Sitecore.Data.Items;

    public interface IDocumentUploadRepository
    {
        Guid GetDocuTypeIdForFactsheet();

        Guid GetDocTypeIdForKiid();

        void UploadDocuments(string selectedFund, string selectedDocType, string fundDocumenttName, string documentNameFieldValue, string fileName, string fileAsBinary, string mediaLibraryPath, bool overwriteMediaItem);

        Item GetFundDocumentByDocumentName(Item fundItem, string documentName);

        IDictionary<Guid, string> GetDoctTypesForLTAdminDocUploadModule();

        Guid GetFundIdByName(string fundName);
    }
}
