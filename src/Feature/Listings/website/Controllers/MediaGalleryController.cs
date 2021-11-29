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
    using LionTrust.Feature.Listings.Models.MediaGallery;
    using LionTrust.Foundation.Core.ActionResults;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Mvc.Controllers;

    public class MediaGalleryController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public MediaGalleryController(IMvcContext mvcContext)
        {
            _mvcContext = mvcContext;
        }        

        [HttpPost]
        public void DownloadMediaImages(List<string> downloadMediaIds)
        {
            if (downloadMediaIds == null || !downloadMediaIds.Any())
            {
                return;
            }

            var files = MediaGalleryHelper.GetMediaFilesById(downloadMediaIds, _mvcContext);
            if (files.Any())
            {
                HttpContext.Response.ContentType = "application/zip, application/octet-stream";
                HttpContext.Response.AppendHeader("content-disposition", "attachment; filename=\"MediaGallery.zip\"");
                HttpContext.Response.CacheControl = "Private";
                HttpContext.Response.Cache.SetExpires(DateTime.Now.AddMinutes(3));

                var zipOutputStream = new ZipOutputStream(HttpContext.Response.OutputStream);
                zipOutputStream.SetLevel(4);

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
}