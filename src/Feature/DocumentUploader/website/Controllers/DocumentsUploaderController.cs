namespace LionTrust.Feature.DocumentUploader.Controllers
{
    using LionTrust.Feature.DocumentUploader.Repository;
    using LionTrust.Feature.DocumentUploader.Models;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    /// <summary>
    /// Custom REST API that uses Sitecore SSC routing so that no additional web service has to be created
    /// </summary>
    //[Authorize]
    public class DocumentsUploaderController : SitecoreController
    {
        /// <summary>
        /// Instance of Action class that contains the business logic
        /// </summary>
        private readonly IDocumentUploadRepository _documentUploadRepository;

        /// <summary>
        /// Constructor init
        /// </summary>
        /// <param name="repository"></param>
        public DocumentsUploaderController(IDocumentUploadRepository repository)
        {
            _documentUploadRepository = repository;
        }

        /// <summary>
        /// Method to upload files into sitecore media library and  link it to the fund
        /// </summary>
        /// <param name="id"></param>
        /// <param name="documentUploadEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadDocuments(DocumentUploaderViewModel documentUploadEntity)
        {
            var uploadResult = _documentUploadRepository.UploadDocuments(documentUploadEntity);
            return Json(uploadResult, JsonRequestBehavior.AllowGet);
        }
    }
}