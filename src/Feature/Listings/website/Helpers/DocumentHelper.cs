namespace LionTrust.Feature.Listings.Helpers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Listings.Models;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Data;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;
    using System;
    using System.Collections.Generic;

    public static class DocumentHelper
    {
        public static IList<Document> GetDocumentFilesById(IList<string> downloadFileIds, IMvcContext mvcContext)
        {
            var result = new List<Document>();

            foreach (var id in downloadFileIds)
            {
                var documentItem = mvcContext.SitecoreService.Database.GetItem(id);

                if (documentItem == null)
                {
                    return null;
                }

                LinkField documentDownloadLink = documentItem.Fields[Foundation.Legacy.Constants.Document.DownloadLink_FieldId];

                if (documentDownloadLink == null)
                {
                    return null;
                }

                var documentMediaItemId = documentDownloadLink.TargetID;

                if (documentMediaItemId == ID.Null)
                {
                    return null;
                }
                // get the media item from Sitecore, using the ID
                MediaItem mediaItem = mvcContext.SitecoreService.Database.GetItem(documentMediaItemId);

                var documentName = documentItem[LionTrust.Foundation.Legacy.Constants.Document.DocumentName_FieldId];

                if (documentName == string.Empty)
                {
                    documentName = documentItem.Name;
                }

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
                            result.Add(new Document
                            {
                                DocumentName = documentName,
                                Name = mediaStream.FileName,
                                Bytes = FileHelper.GetByteArray(stream),
                                Length = stream.Length,
                                DocumentExtension = mediaStream.MimeType
                            });
                        }
                    }
                }
            }

            return result;
        }

        public static void TriggerGoalsForDocumentDownload(IList<string> documentMediaItemId, IMvcContext mvcContext)
        {
            foreach (var mediaItemId in documentMediaItemId)
            {
                try
                {
                    if (!Tracker.IsActive)
                    {
                        Tracker.StartTracking();
                    }

                    if (Tracker.IsActive && Tracker.Current.CurrentPage != null)
                    {
                        var documentItem = mvcContext.SitecoreService.Database.GetItem(mediaItemId);
                        if (documentItem != null)
                        {
                            LinkField documentDownloadLink = documentItem.Fields[Foundation.Legacy.Constants.Document.DownloadLink_FieldId];
                            var documentMediaTargetItemId = documentDownloadLink.TargetID;
                            if (documentMediaTargetItemId != ID.Null)
                            {
                                // get the media item from Sitecore, using the ID
                                var mediaItem = mvcContext.SitecoreService.Database.GetItem(documentMediaTargetItemId);

                                if (mediaItem != null)
                                {
                                    var trackingField = new TrackingField(mediaItem.Fields["__Tracking"]);
                                    if (trackingField != null)
                                    {
                                        var goalIds = trackingField.EventIds;
                                        foreach (var goalId in goalIds)
                                        {
                                            var goalItem = mvcContext.SitecoreService.Database.GetItem(goalId);
                                            if (goalItem != null)
                                            {
                                                var goalTrigger = Tracker.MarketingDefinitions.Goals[goalItem.ID.ToGuid()];
                                                if (goalTrigger != null)
                                                {
                                                    var goalEventData = Tracker.Current.CurrentPage.RegisterGoal(goalTrigger);
                                                    goalEventData.Data = goalItem["Description"];
                                                    Tracker.Current.Interaction.AcceptModifications();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error(string.Format("Exception occured when triggering goals for Document item: {0}.", mediaItemId), ex, typeof(DocumentHelper));
                }
            }
        }       
    }
}