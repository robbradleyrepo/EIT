namespace LionTrust.Feature.Listings.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;
    using ICSharpCode.SharpZipLib.Zip;
    using LionTrust.Feature.Listings.Helpers;
    using LionTrust.Feature.Listings.Models;
    using LionTrust.Foundation.Content.Repositories;
    using LionTrust.Foundation.LocalDatasource;
    using Sitecore.Mvc.Controllers;
   
    public class LatestResultsAndPresentationsController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;
        private readonly IRenderingRepository _repository;

        public LatestResultsAndPresentationsController(IMvcContext mvcContext, IRenderingRepository repository)
        {
            _mvcContext = mvcContext;
            _repository = repository;
        }

        public ActionResult Render()
        {
            var viewModel = new LatestResultsAndPresentationsViewModel();
            viewModel.Data = _repository.GetDataSourceItem<ILatestResultsAndPresentations>();

            if (viewModel.Data == null)
            {
                return null;
            }

            if (viewModel.Data.AllResultsPage != null 
                && viewModel.Data.AllResultsPage.Children != null
                && viewModel.Data.AllResultsPage.Children.Any())
            {
                var localContentFolder = viewModel.Data.AllResultsPage.Children.Where(x => x.TemplateId.Equals(new Guid(Settings.LocalDatasourceFolderTemplate)))?.FirstOrDefault();
                if (localContentFolder != null
                    && localContentFolder.Children != null
                    && localContentFolder.Children.Any())
                {
                    var documentListerVariantTemplateId = new Guid(Constants.DocumentListerVariant.TemplateId);
                    var component = localContentFolder.Children.Where(x => x.BaseTemplateIds.Contains(documentListerVariantTemplateId))?.FirstOrDefault();
                    if (component != null)
                    {
                        var documentListerVariantComponent = _mvcContext.SitecoreService.GetItem<IDocumentListerVariants>(component.Id);
                        if (documentListerVariantComponent != null 
                            && documentListerVariantComponent.DocumentVariants != null 
                            && documentListerVariantComponent.DocumentVariants.Any()) 
                        {
                            var latestDocumentVariants = documentListerVariantComponent.DocumentVariants
                                                            .OrderByDescending(x => x.Date != DateTime.MinValue ? x.Date : x.Created);
                            if (latestDocumentVariants.Count() > 3)
                            {
                                viewModel.LatestDocuments = latestDocumentVariants.Take(3);
                            }
                            else
                            {
                                viewModel.LatestDocuments = latestDocumentVariants;
                            }

                            foreach (var document in viewModel.LatestDocuments)
                            {
                                if (document.PressReleaseDocument != null 
                                    && document.PressReleaseDocument.TargetId != Guid.Empty
                                    && document.ReportDocument != null
                                    && document.ReportDocument.TargetId != Guid.Empty)
                                {
                                    document.ZipDocumentUrl = 
                                        Sitecore.Configuration.Settings.GetSetting(Constants.Settings.LatestResultApiRoute_SettingName) 
                                        + "/GetLatestResultDocuments?firstDoc=" + document.PressReleaseDocument.TargetId.ToString() 
                                        + "&secondDoc=" + document.ReportDocument.TargetId.ToString(); 
                                }
                            }
                        }
                    }
                }
            }

            return View("~/Views/Listings/LatestResultsAndPresentations.cshtml", viewModel);
        }

        public void GetLatestResultDocuments(string firstDoc, string secondDoc)
        {
            HttpContext.Response.ContentType = "application/zip, application/octet-stream";
            HttpContext.Response.AppendHeader("content-disposition", "attachment; filename=\"MediaGallery.zip\"");
            HttpContext.Response.CacheControl = "Private";
            HttpContext.Response.Cache.SetExpires(DateTime.Now.AddMinutes(3));

            var zipOutputStream = new ZipOutputStream(HttpContext.Response.OutputStream);
            zipOutputStream.SetLevel(4);
            var fileIds = new List<string> { firstDoc, secondDoc };
            var files = MediaGalleryHelper.GetMediaFilesById(fileIds, _mvcContext);
            foreach (var documentDocument in files)
            {
                var entry =
                    new ZipEntry(
                        ZipEntry.CleanName(String.Format("{0} - {1}", documentDocument.MediaName,
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