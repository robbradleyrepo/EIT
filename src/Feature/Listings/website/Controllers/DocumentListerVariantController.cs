namespace LionTrust.Feature.Listings.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;
    using ICSharpCode.SharpZipLib.Zip;
    using LionTrust.Feature.Listings.Helpers;
    using LionTrust.Feature.Listings.Models;
    using LionTrust.Foundation.Content.Repositories;
    using Sitecore.Mvc.Controllers;

    public class DocumentListerVariantController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;
        private readonly IRenderingRepository _repository;

        public DocumentListerVariantController(IMvcContext mvcContext, IRenderingRepository repository)
        {
            _mvcContext = mvcContext;
            _repository = repository;
        }

        public ActionResult Render()
        {
            var viewModel = new DocumentListerVariantViewModel();
            viewModel.Data = _repository.GetDataSourceItem<IDocumentListerVariants>();
            
            if (viewModel.Data == null)
            {
                return null;
            }

            if (viewModel.Data.DocumentVariants == null || !viewModel.Data.DocumentVariants.Any())
            {
                return View("~/Views/Listings/DocumentListerVariant.cshtml", viewModel);
            }
            else
            {
                viewModel.Years = new List<string>();
                var years = viewModel.Data.DocumentVariants.GroupBy(x => x.Date).OrderByDescending(x => x.Key);
                viewModel.FirstYear = years.FirstOrDefault().Key.ToString("yyyy");
                viewModel.DocumentsByYears = new List<DocumentVariantYears>();
                DocumentVariantYears tmpDocumentVariantYear;
                foreach (var year in years)
                {
                    tmpDocumentVariantYear = new DocumentVariantYears();
                    tmpDocumentVariantYear.Year = year.Key.ToString("yyyy");
                    viewModel.Years.Add(tmpDocumentVariantYear.Year);
                    tmpDocumentVariantYear.Documents = new List<IDocumentVariant>();

                    foreach (var document in year) 
                    {
                        tmpDocumentVariantYear.Documents.Add(document);
                    }

                    viewModel.DocumentsByYears.Add(tmpDocumentVariantYear);
                }

                return View("~/Views/Listings/DocumentListerVariant.cshtml", viewModel);
            }
        }

        [HttpPost]
        public void DownloadDocuments(List<string> downloadFileIds)
        {
            if (downloadFileIds == null || !downloadFileIds.Any())
            {
                return;
            }

            var files = DocumentHelper.GetDocumentFilesById(downloadFileIds, _mvcContext);
            if (files.Any())
            {
                DocumentHelper.TriggerGoalsForDocumentDownload(downloadFileIds, _mvcContext);
                if (files.Count == 1)
                {
                    var document = files.First();
                    WebClient client = new WebClient();
                    HttpContext.Response.ContentType = document.DocumentExtension;
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