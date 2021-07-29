namespace LionTrust.Feature.Listings.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Listings.Models;
    using LionTrust.Foundation.Core.ActionResults;
    using Sitecore.Mvc.Controllers;

    public class MediaGalleryController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public MediaGalleryController(IMvcContext mvcContext)
        {
            _mvcContext = mvcContext;
        }

        public ActionResult GetMediaItems(string mediaGalleryFolderId, string fundCategory = "", string word = "")
        {
            if (string.IsNullOrEmpty(mediaGalleryFolderId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Document Folder ID required!");
            }

            Guid mediaGalleryFolderGuid;

            try
            {
                mediaGalleryFolderGuid = new Guid(mediaGalleryFolderId);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Document Folder ID!");
            }

            var mediaGallery = _mvcContext.SitecoreService.GetItem<IMediaGallery>(mediaGalleryFolderGuid);

            var mediaGalleryResponse = new MediaGalleryResponse();
            var result = mediaGallery.MediaItems;

            if (!string.IsNullOrEmpty(fundCategory)) 
            {
                result = result.Where(m => m.Categories.Any(x=>x.Id.ToString().Equals(fundCategory)));
            }
            if (!string.IsNullOrWhiteSpace(word))
            {
                result = result.Where(m => m.Description.ToLower().Contains(word.ToLower()));
            }

            mediaGalleryResponse.TotalResults = ;
            mediaGalleryResponse.StatusCode = 200;

            return new JsonCamelCaseResult(mediaGalleryResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DownloadMediaImages(List<string> downloadMediaIds)
        {

        }
    }
}