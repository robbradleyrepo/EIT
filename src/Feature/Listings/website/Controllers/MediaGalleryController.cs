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

        public ActionResult GetMediaItems(string mediaGalleryId, string fundCategory = "", string word = "")
        {
            if (string.IsNullOrEmpty(mediaGalleryId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Media Folder ID required!");
            }

            Guid mediaGalleryFolderGuid;

            try
            {
                mediaGalleryFolderGuid = new Guid(mediaGalleryId);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Media Folder ID!");
            }

            var mediaGallery = _mvcContext.SitecoreService.GetItem<IMediaGallery>(mediaGalleryFolderGuid);
            var mediaGalleryResponse = new MediaGalleryResponse();
            var result = mediaGallery.MediaItems;
            if (result != null)
            {
                if (!string.IsNullOrEmpty(fundCategory))
                {
                    result = result.Where(m => m.Categories.Any(x => x.Id.ToString().Equals(fundCategory)));
                }

                if (!string.IsNullOrWhiteSpace(word))
                {
                    var lowerWord = word.ToLower();
                    result = result.Where(m => m.Description.ToLower().Contains(lowerWord));
                }

                mediaGalleryResponse.SearchResults = result.Select(m => new MediaItemResponseModel()
                {
                    Description = m.Description,
                    Id = m.Id.ToString(),
                    FullBodyImage = MediaGalleryHelper.GetImageModel(m.FullBodyImage),
                    HeadShotImage = MediaGalleryHelper.GetImageModel(m.HeadshotImage)

                });

                mediaGalleryResponse.TotalResults = mediaGalleryResponse.SearchResults.Count();
                mediaGalleryResponse.StatusCode = 200;
            }

            return new JsonCamelCaseResult(mediaGalleryResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMediaFacets(string mediaGalleryId) 
        {
            if (string.IsNullOrEmpty(mediaGalleryId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Media Folder ID required!");
            }

            Guid mediaGalleryFolderGuid;

            try
            {
                mediaGalleryFolderGuid = new Guid(mediaGalleryId);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Media Folder ID!");
            }

            var mediaGallery = _mvcContext.SitecoreService.GetItem<IMediaGallery>(mediaGalleryFolderGuid);
            if (mediaGallery.FilterCategoryFolder == null 
                || mediaGallery.FilterCategoryFolder.FundTeams == null 
                || !mediaGallery.FilterCategoryFolder.FundTeams.Any())
            {
                return new HttpNotFoundResult();
            }

            var facetResponse = mediaGallery.FilterCategoryFolder.FundTeams.Select(ft => 
                                                                                new ListingFilterFacetsModel()
                                                                                {
                                                                                    Identifier = ft.Id.ToString(),
                                                                                    Name = ft.TeamName
                                                                                });

            return new JsonCamelCaseResult(facetResponse, JsonRequestBehavior.AllowGet);
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