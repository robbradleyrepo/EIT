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

        private const int MaxTabs = 5;

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
                var years = viewModel.Data.DocumentVariants.Where(x => x.Date != DateTime.MinValue).GroupBy(x => x.Date.Year).OrderByDescending(x => x.Key);
                var yearTabs = years.Select(x => x.Key).ToList();
                viewModel.DocumentsByYears = new List<DocumentVariantYears>();

                foreach (var year in years)
                {
                    var yearLabel = GetYearLabel(year.Key, yearTabs);

                    if (yearTabs.Count < MaxTabs || (yearTabs.Count > MaxTabs && yearTabs[MaxTabs - 1] <= year.Key))
                    {
                        var documentVariantYears = new DocumentVariantYears
                        {
                            Id = Guid.NewGuid(),
                            Year = yearLabel,
                            IsLatestYear = years.FirstOrDefault()?.Key == year.Key,
                            Documents = new List<IDocumentVariant>()
                        };

                        viewModel.DocumentsByYears.Add(documentVariantYears);
                    }

                    var documents = year.OrderByDescending(x => x.Date).ToList();

                    var documentByYear = viewModel.DocumentsByYears.FirstOrDefault(x => x.Year == yearLabel);
                    if (documentByYear != null)
                    {
                        documentByYear.Documents.AddRange(documents);
                    }
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
                    var contentType = document.DocumentExtension;
                    if (document.DocumentExtension == Constants.DocumentTypes.SitecoreXMLType)
                    {
                        contentType = Constants.DocumentTypes.MappedXMLType;
                    }
                    HttpContext.Response.ContentType = contentType;
                    HttpContext.Response.AddHeader("content-length", document.Bytes.Length.ToString());
                    HttpContext.Response.AddHeader("document-type", document.DocumentExtension);
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

        private string GetYearLabel(int year, List<int> years)
        {
            var yearLabel = Convert.ToString(year);
            if (years.Count > MaxTabs)
            {
                if (years[MaxTabs - 1] == year)
                {
                    yearLabel = $"{year}+";
                }
                else if (years[MaxTabs - 1] < year)
                {
                    yearLabel = Convert.ToString(year);
                }
                else if (years[MaxTabs - 1] > year)
                {
                    yearLabel = $"{years[MaxTabs - 1]}+";
                }
            }

            return yearLabel;
        }
    }
}