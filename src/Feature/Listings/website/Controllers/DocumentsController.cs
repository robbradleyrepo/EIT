namespace LionTrust.Feature.Listings.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using ICSharpCode.SharpZipLib.Zip;
    using LionTrust.Feature.Listings.Helpers;
    using LionTrust.Feature.Listings.Models;
    using LionTrust.Foundation.Core.ActionResults;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class DocumentsController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public DocumentsController(IMvcContext mvcContext)
        {
            _mvcContext = mvcContext;
        }

        public ActionResult GetDocuments(string documentFolderId, string sortOrder = "ASC", int page = 1, int resultsPerPage = 10)
        {
            if (string.IsNullOrEmpty(documentFolderId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Document Folder ID required!");
            }

            Guid documentFolderGuid;

            try
            {
                documentFolderGuid = new Guid(documentFolderId);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Document Folder ID!");
            }

            var documentLister = _mvcContext.SitecoreService.GetItem<IDocumentLister>(documentFolderGuid);
            var documentsResponse = new DocumentsResponse();
            if (documentLister.DocumentList != null && documentLister.DocumentList.Any())
            {
                documentsResponse.SearchResults = documentLister.DocumentList.Select(
                    x => new DocumentModel
                    {
                        Title = x.DocumentName,
                        DocumentLink = x.DocumentLink?.Url,
                        DocumentLinkText = x.DocumentLink?.Text,
                        DocumentPageLink = x.PageLink?.Url,
                        DocumentId = x.Id,
                        DocumentVideoLink = x.VideoLink?.Url
                    });

                if (sortOrder == "ASC")
                {
                    documentsResponse.SearchResults = documentsResponse.SearchResults.OrderBy(x => x.Title);
                }
                else
                {
                    documentsResponse.SearchResults = documentsResponse.SearchResults.OrderByDescending(x => x.Title);
                }

                documentsResponse.SearchResults = documentsResponse.SearchResults.Skip((page - 1) * resultsPerPage).Take(resultsPerPage);
            }

            if (documentsResponse.SearchResults == null || !documentsResponse.SearchResults.Any())
            {
                return new HttpNotFoundResult();
            }

            documentsResponse.TotalResults = documentsResponse.SearchResults.Count();
            documentsResponse.StatusCode = 200;

            return new JsonCamelCaseResult(documentsResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DownloadDocuments(List<string> downloadFileIds)
        {
            if (downloadFileIds == null || !downloadFileIds.Any())
            {
                return;
            }

            var files = DocumentHelper.GetMediaFilesById(downloadFileIds, _mvcContext);
            if (files.Any())
            {
                DocumentHelper.TriggerGoalsForDocumentDownload(downloadFileIds, _mvcContext);
                if (files.Count == 1)
                {
                    var document = files.First();
                    WebClient client = new WebClient();
                    HttpContext.Response.ContentType = "application/pdf";
                    HttpContext.Response.AddHeader("content-length", document.Bytes.Length.ToString());
                    HttpContext.Response.BinaryWrite(document.Bytes);
                    HttpContext.Response.AppendHeader("Content-Disposition", "inline; filename=" + document.Name);
                }
                else
                {
                    HttpContext.Response.ContentType = "application/zip, application/octet-stream";
                    HttpContext.Response.AppendHeader("content-disposition", "attachment; filename=\"Download.zip\"");
                    HttpContext.Response.CacheControl = "Private";
                    HttpContext.Response.Cache.SetExpires(DateTime.Now.AddMinutes(3));

                    var zipOutputStream = new ZipOutputStream(HttpContext.Response.OutputStream);
                    zipOutputStream.SetLevel(4);

                    foreach (var documentDocument in files)
                    {
                        var entry =
                            new ZipEntry(
                                ZipEntry.CleanName(String.Format("{0} - {1}", documentDocument.DocumentName,
                                    documentDocument.Name)))
                            {
                                Size = documentDocument.Length
                            };

                        zipOutputStream.PutNextEntry(entry);
                        zipOutputStream.Write(documentDocument.Bytes, 0, documentDocument.Bytes.Length);
                    }
                    zipOutputStream.Close();
                    HttpContext.Response.Flush();
                    HttpContext.Response.End();
                }
            }
        }
    }
}