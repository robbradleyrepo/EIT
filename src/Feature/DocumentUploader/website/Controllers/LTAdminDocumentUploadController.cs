using LionTrust.Feature.DocumentUploader.Models;
using LionTrust.Feature.DocumentUploader.Repository;
using Sitecore.Diagnostics;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Services;
using System.Web.Http;

namespace LionTrust.Feature.DocumentUploader.Controllers
{
    /// <summary>
    /// Custom REST API that uses Sitecore SSC routing so that no additional web service has to be created
    /// </summary>
    [ServicesController]
    [Authorize]
    public class LTAdminDocumentUploadController : EntityServiceBase<LTAdminDocumentUploadViewModel>
    {
        /// <summary>
        /// Instance of Action class that contains the business logic
        /// </summary>
        private ILTAdminDocumentUploadRepository<LTAdminDocumentUploadViewModel> _customRepositoryActions;

        /// <summary>
        /// Required constructor for SSC
        /// </summary>
        /// <param name="repository"></param>
        public LTAdminDocumentUploadController(ILTAdminDocumentUploadRepository<LTAdminDocumentUploadViewModel> repository) : base(repository)
        {
            Log.Debug("LTAdminDocumentUploadController()", this);
            _customRepositoryActions = repository;
        }

        /// <summary>
        /// Required constructor for SSC
        /// </summary>
        public LTAdminDocumentUploadController() : this(new LTAdminDocumentUploadRepository())
        {
        }

        /// <summary>
        /// Load document upload page
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("LoadDocumentUploadPage")]
        public LTAdminDocumentUploadViewModel LoadDocumentUploadPage()
        {
            Log.Debug("LoadDocumentUploadPage()", this);            
            return _customRepositoryActions.LoadDocumentUploadPage();
        }

        /// <summary>
        /// Method to upload files into sitecore media library and  link it to the fund
        /// </summary>
        /// <param name="documentUploadEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UploadDocuments")]
        public LTAdminDocumentUploadViewModel UploadDocuments(LTAdminDocumentUploadViewModel documentUploadEntity)
        {
            Log.Debug("UploadDocuments()", this);            
            return _customRepositoryActions.UploadDocuments(documentUploadEntity);
        }
    }
}