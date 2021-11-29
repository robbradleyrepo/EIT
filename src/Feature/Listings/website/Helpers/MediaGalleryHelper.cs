namespace LionTrust.Feature.Listings.Helpers
{
    using System;
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Listings.Models.MediaGallery;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;

    public static class MediaGalleryHelper
    {
        public static IList<MediaFileModel> GetMediaFilesById(IList<string> downloadFileIds, IMvcContext mvcContext)
        {
            var result = new List<MediaFileModel>();

            foreach (var id in downloadFileIds)
            {
                // get the media item from Sitecore, using the ID
                MediaItem mediaItem = mvcContext.SitecoreService.Database.GetItem(id);

                var mediaName = string.IsNullOrEmpty(mediaItem.Title) ? mediaItem.Title : mediaItem.Name;

                Media media = MediaManager.GetMedia(mediaItem);

                if (media != null)
                {
                    // initiate media stream
                    var options = new MediaOptions();
                    options.UseMediaCache = true;

                    using (MediaStream mediaStream = media.GetStream(options))
                    {
                        using (var stream = mediaStream.Stream)
                        {
                            result.Add(new MediaFileModel
                            {
                                MediaName = mediaName,
                                Name = mediaStream.FileName,
                                Bytes = FileHelper.GetByteArray(stream),
                                Length = stream.Length
                            });
                        }
                    }
                }
            }

            return result;
        }      
    }
}