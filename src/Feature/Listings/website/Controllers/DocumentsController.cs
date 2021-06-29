namespace LionTrust.Feature.Listings.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Listings.Models;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class DocumentsController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public DocumentsController(IMvcContext mvcContext) 
        {
            _mvcContext = mvcContext;
        }

        public ActionResult GetDocuments(string documentFolderId, bool sortyByAZ = true) 
        { 
            if (string.IsNullOrEmpty(documentFolderId))
            {
                return null;
            }

            var documentLister = _mvcContext.SitecoreService.GetItem<IDocumentLister>(new Guid(documentFolderId));
            var response = this.BuildDocumentsList(documentLister, sortyByAZ);

            return Json(response, JsonRequestBehavior.AllowGet); 
        }

        public List<DocumentModel> BuildDocumentsList(IDocumentLister documentLister, bool sortyByAZ)
        {
            var documentsListSorted = new List<DocumentModel>();
            if (documentLister.DocumentList != null && documentLister.DocumentList.Any())
            {
                if (sortyByAZ)
                {
                    documentsListSorted = documentLister.DocumentList.Select(x => new DocumentModel { Title = x.DocumentName, DocumentLink = x.DocumentLink?.Url, DocumentLinkText = x.DocumentLink?.Text, DocumentPageLink = x.Url }).OrderBy(x => x.Title).ToList();
                }
                else
                {
                    documentsListSorted = documentLister.DocumentList.Select(x => new DocumentModel { Title = x.DocumentName, DocumentLink = x.DocumentLink?.Url, DocumentLinkText = x.DocumentLink?.Text, DocumentPageLink = x.Url }).OrderByDescending(x => x.Title).ToList();
                }
            }

            return documentsListSorted;
        }
    }
}