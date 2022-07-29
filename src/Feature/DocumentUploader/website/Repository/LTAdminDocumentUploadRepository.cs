using LionTrust.Feature.DocumentUploader.Models;
using LionTrust.Feature.DocumentUploader.Services;
using Sitecore.Diagnostics;
using System;
using System.Linq;

namespace LionTrust.Feature.DocumentUploader.Repository
{
    /// <summary>
    /// Implementation of REST API methods define in ILTAdminDocumentUploadRepository interface
    /// </summary>
    public class LTAdminDocumentUploadRepository : ILTAdminDocumentUploadRepository<LTAdminDocumentUploadViewModel>
    {
        private readonly LTAdminService _ltAdminService;

        public LTAdminDocumentUploadRepository()
        {
            _ltAdminService = new LTAdminService(new DocumentUploadRepository());
        }

        /// <summary>
        /// Method to load document upload page with dropdown list values
        /// </summary>        
        /// <returns></returns>
        public LTAdminDocumentUploadViewModel LoadDocumentUploadPage()
        {
            return _ltAdminService.LoadDocumentUploadPage();
        }

        /// <summary>
        /// Method to upload files into sitecore media library and link it to the fund
        /// </summary>
        /// <param name="documentUploadEntity"></param>
        /// <returns></returns>
        public LTAdminDocumentUploadViewModel UploadDocuments(LTAdminDocumentUploadViewModel documentUploadEntity)
        {
            var returningObj = new LTAdminDocumentUploadViewModel();
            try
            {
                var uploadErrorDictionary = _ltAdminService.UploadDocuments(documentUploadEntity);                
                returningObj.UploadErrorDictionary = uploadErrorDictionary;
                returningObj.UploadSuccess = true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured when Uploading documents.", ex, this);
                returningObj.UploadSuccess = false;
            }

            return returningObj;
        }

        public void Add(LTAdminDocumentUploadViewModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(LTAdminDocumentUploadViewModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(LTAdminDocumentUploadViewModel entity)
        {
            throw new NotImplementedException();
        }

        public LTAdminDocumentUploadViewModel FindById(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LTAdminDocumentUploadViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(LTAdminDocumentUploadViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}