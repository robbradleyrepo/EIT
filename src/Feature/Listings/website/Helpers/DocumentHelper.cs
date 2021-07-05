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
    using System.IO;

    public static class DocumentHelper
    {
        public static IList<Document> GetMediaFilesById(IList<string> downloadFileIds, IMvcContext mvcContext)
        {
            var result = new List<Document>();

            foreach (var id in downloadFileIds)
            {
                var documentItem = mvcContext.SitecoreService.Database.GetItem(id);

                if (documentItem != null)
                {
                    LinkField documentDownloadLink = documentItem.Fields[Foundation.Legacy.Constants.Document.DownloadLink_FieldId];
                    var documentMediaItemId = documentDownloadLink.TargetID;

                    if (documentMediaItemId != ID.Null)
                    {
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
                                        Bytes = DocumentHelper.GetByteArray(stream),
                                        Length = stream.Length
                                    });
                                }
                            }
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

        private static Byte[] GetByteArray(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                var readBuffer = new byte[4096];

                var totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            //NG: doubling the size of the array could cause troubles with large streams
                            var temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                var buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}