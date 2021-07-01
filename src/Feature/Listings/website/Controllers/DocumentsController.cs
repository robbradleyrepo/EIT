namespace LionTrust.Feature.Listings.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using ICSharpCode.SharpZipLib.Zip;
    using LionTrust.Feature.Listings.Models;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Data;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Mvc.Controllers;
    using Sitecore.Resources.Media;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    public class DocumentsController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public DocumentsController(IMvcContext mvcContext)
        {
            _mvcContext = mvcContext;
        }

        public ActionResult GetDocuments(string documentFolderId, bool sortyByAZ = true)
        {
            if (string.IsNullOrEmpty(documentFolderId))
            {
                return null;
            }

            var documentLister = _mvcContext.SitecoreService.GetItem<IDocumentLister>(new Guid(documentFolderId));
            var response = this.BuildDocumentsList(documentLister, sortyByAZ);

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<DocumentModel> BuildDocumentsList(IDocumentLister documentLister, bool sortyByAZ, int page = 1, int resultsPerPage = 10)
        {
            IEnumerable<DocumentModel> documentsListSorted = null;
            if (documentLister.DocumentList != null && documentLister.DocumentList.Any())
            {
                if (sortyByAZ)
                {
                    documentsListSorted = documentLister.DocumentList.Select(x => new DocumentModel { Title = x.DocumentName, DocumentLink = x.DocumentLink?.Url, DocumentLinkText = x.DocumentLink?.Text, DocumentPageLink = x.Url }).OrderBy(x => x.Title).Skip((page - 1)*resultsPerPage).Take(resultsPerPage);
                }
                else
                {
                    documentsListSorted = documentLister.DocumentList.Select(x => new DocumentModel { Title = x.DocumentName, DocumentLink = x.DocumentLink?.Url, DocumentLinkText = x.DocumentLink?.Text, DocumentPageLink = x.Url }).OrderByDescending(x => x.Title).Skip((page - 1) * resultsPerPage).Take(resultsPerPage);
                }
            }

            return documentsListSorted;
        }

        [HttpPost]
        public void DownloadDocuments(List<string> downloadFileIds)
        {
            if (downloadFileIds != null && downloadFileIds.Any())
            {
                var files = this.GetMediaFilesById(downloadFileIds);
                if (files.Any())
                {
                    this.TriggerGoalsForDocumentDownload(downloadFileIds);
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

        private IList<Document> GetMediaFilesById(IList<string> downloadFileIds)
        {
            var result = new List<Document>();

            foreach (var id in downloadFileIds)
            {
                var documentItem = _mvcContext.SitecoreService.Database.GetItem(id);

                if (documentItem != null)
                {
                    LinkField documentDownloadLink = documentItem.Fields[Foundation.Legacy.Constants.Document.DownloadLink_FieldId];
                    var documentMediaItemId = documentDownloadLink.TargetID;

                    if (documentMediaItemId != ID.Null)
                    {
                        // get the media item from Sitecore, using the ID
                        MediaItem mediaItem = _mvcContext.SitecoreService.Database.GetItem(documentMediaItemId);

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
                                        Bytes = this.GetByteArray(stream),
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

        private void TriggerGoalsForDocumentDownload(IList<string> documentMediaItemId)
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
                        var documentItem = _mvcContext.SitecoreService.Database.GetItem(mediaItemId);
                        if (documentItem != null)
                        {
                            LinkField documentDownloadLink = documentItem.Fields[Foundation.Legacy.Constants.Document.DownloadLink_FieldId];
                            var documentMediaTargetItemId = documentDownloadLink.TargetID;
                            if (documentMediaTargetItemId != ID.Null)
                            {
                                // get the media item from Sitecore, using the ID
                                var mediaItem = _mvcContext.SitecoreService.Database.GetItem(documentMediaTargetItemId);

                                if (mediaItem != null)
                                {
                                    var trackingField = new TrackingField(mediaItem.Fields["__Tracking"]);
                                    if (trackingField != null)
                                    {
                                        var goalIds = trackingField.EventIds;
                                        foreach (var goalId in goalIds)
                                        {
                                            var goalItem = _mvcContext.SitecoreService.Database.GetItem(goalId);
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
                    Sitecore.Diagnostics.Log.Error(string.Format("Exception occured when triggering goals for Document item: {0}.", mediaItemId), ex, this);
                }
            }
        }

        private Byte[] GetByteArray(Stream stream)
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