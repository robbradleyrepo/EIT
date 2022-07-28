namespace LionTrust.Feature.DocumentUploader.Repository
{
    using LionTrust.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;
    using Sitecore.SecurityModel;
    using Sitecore.StringExtensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DocumentUploadRepository : IDocumentUploadRepository
    {
        /// <summary>
        /// Get master DB
        /// </summary>
        public Database MasterDatabase
        {
            get
            {
                return Factory.GetDatabase("master");
            }
        }

        /// <summary>
        /// Get document type id for factsheet
        /// </summary>
        /// <returns></returns>
        public Guid GetDocuTypeIdForFactsheet()
        {
            return Guid.Parse(Constants.DocumentTypes.Factsheet);
        }

        /// <summary>
        /// Get document type id for KIID
        /// </summary>
        /// <returns></returns>
        public Guid GetDocTypeIdForKiid()
        {
            return Guid.Parse(Constants.DocumentTypes.Kiids);
        }

        /// <summary>
        /// Save file as a sitecore media library item and create/update fund document item
        /// </summary>
        /// <param name="selectedFund"></param>
        /// <param name="selectedDocType"></param>
        /// <param name="fundDocumentName"></param>
        /// <param name="documentNameFieldValue"></param>
        /// <param name="fileName"></param>
        /// <param name="fileAsBinary"></param>
        /// <param name="mediaLibraryPath"></param>
        /// <param name="overwriteMediaItem"></param>
        public void UploadDocuments(string selectedFund, string selectedDocType, string fundDocumentName, string documentNameFieldValue, string fileName, string fileAsBinary, string mediaLibraryPath, bool overwriteMediaItem)
        {
            fileAsBinary = fileAsBinary.Replace("data:application/pdf;base64,", "");
            var uploadedFileAsBytes = Convert.FromBase64String(fileAsBinary);

            fileName = ItemUtil.ProposeValidItemName(Path.GetFileNameWithoutExtension(fileName));

            Item mediaLibraryItem = null;
            using (new SecurityDisabler())
            {
                using (var stream = new MemoryStream(uploadedFileAsBytes))
                {
                    var options = new MediaCreatorOptions
                    {
                        FileBased = false,
                        IncludeExtensionInItemName = false,
                        OverwriteExisting = overwriteMediaItem,
                        Versioned = false,
                        Destination = string.Format("{0}/{1}", mediaLibraryPath, fileName),
                        Database = MasterDatabase
                    };

                    var mc = new MediaCreator();
                    mediaLibraryItem = mc.CreateFromStream(stream, string.Format("{0}.pdf", fileName), options);
                    mediaLibraryItem.PublishItemAsync(false);
                }
            }

            if (mediaLibraryItem != null)
            {
                var fundItem = MasterDatabase.GetItem(new ID(selectedFund));
                if (fundItem != null)
                {
                    fundDocumentName = fundDocumentName.Trim();
                    documentNameFieldValue = documentNameFieldValue.Trim();

                    var existingFundDocItem = GetFundDocumentByDocumentName(fundItem, fundDocumentName);

                    if (existingFundDocItem == null || !overwriteMediaItem)
                    {
                        var parentItem = fundItem.Children[Constants.Folders.Documents];
                        if (parentItem != null)
                        {
                            var documentTemplateItem = MasterDatabase.Templates[Constants.Templates.Document];
                            using (new SecurityDisabler())
                            {
                                Item newfundDocItem = parentItem.Add(fundDocumentName, documentTemplateItem);
                                newfundDocItem.Editing.BeginEdit();

                                //Set document name
                                newfundDocItem[ID.Parse(Constants.FieldIDs.DocumentName)] = documentNameFieldValue;

                                //Set download link
                                Sitecore.Data.Fields.LinkField downloadlnkField = newfundDocItem.Fields[ID.Parse(Constants.FieldIDs.DownloadLink)];
                                downloadlnkField.LinkType = "media";
                                downloadlnkField.Url = mediaLibraryItem.Paths.MediaPath;
                                downloadlnkField.TargetID = mediaLibraryItem.ID;

                                ////Set document type
                                newfundDocItem[ID.Parse(Constants.FieldIDs.DocumentTypes)] = (new ID(selectedDocType)).ToString();

                                ////Set related fund
                                newfundDocItem[ID.Parse(Constants.FieldIDs.RelatedFunds)] = fundItem.ID.ToString();

                                newfundDocItem.Editing.EndEdit();
                                newfundDocItem.PublishItemAsync(true);
                            }
                        }
                    }
                    else
                    {
                        using (new SecurityDisabler())
                        {
                            existingFundDocItem.Editing.BeginEdit();

                            //Set download link
                            Sitecore.Data.Fields.LinkField downloadlnkField = existingFundDocItem.Fields[ID.Parse(Constants.FieldIDs.DownloadLink)];
                            downloadlnkField.Clear();
                            downloadlnkField.LinkType = "media";
                            downloadlnkField.Url = mediaLibraryItem.Paths.MediaPath;
                            downloadlnkField.TargetID = mediaLibraryItem.ID;

                            //Set document name
                            existingFundDocItem[ID.Parse(Constants.FieldIDs.DocumentName)] = documentNameFieldValue;

                            //Set document type
                            existingFundDocItem[ID.Parse(Constants.FieldIDs.DocumentTypes)] = (new ID(selectedDocType)).ToString();

                            //Set related fund
                            existingFundDocItem[ID.Parse(Constants.FieldIDs.RelatedFunds)] = fundItem.ID.ToString();

                            existingFundDocItem.Editing.EndEdit();
                            existingFundDocItem.PublishItemAsync(true);
                        }
                    }
                }
            }
        }

        public Item GetFundDocumentByDocumentName(Item fundItem, string documentName)
        {
            if (fundItem == null || fundItem.Children[Constants.Folders.Documents] == null)
            {
                return null;
            }

            var documentFolderItem = fundItem.Children[Constants.Folders.Documents];
            var query = "{0}//*[@@templateid='{1}' and @@name = '{2}']".FormatWith(documentFolderItem.Paths.LongID, Constants.Templates.Document, documentName);
            var documentItems = MasterDatabase.SelectItems(query);

            return documentItems.FirstOrDefault();
        }

        public IDictionary<Guid, string> GetDoctTypesForLTAdminDocUploadModule()
        {
            var docTypes = GetFiltersWithIds(Guid.Parse(Constants.Folders.DocumentTypes), Guid.Parse(Constants.Templates.LegacyLookupItem), Guid.Parse(Constants.FieldIDs.LegacyLookupItemNameField));
            return docTypes.Where(x => x.Key.Equals(Guid.Parse(Constants.DocumentTypes.Factsheet)) || x.Key.Equals(Guid.Parse(Constants.DocumentTypes.Kiids))).ToDictionary(x => x.Key, x => x.Value);
        }


        /// <summary>
        /// Returns the Guid of the Fund Item, example: Asia Income Fund|{B0C7AC70-3895-49C4-8F3C-4DF11EFEDC46}
        /// </summary>
        /// <param name="fundName"></param>
        /// <returns></returns>
        public Guid GetFundIdByName(string fundName)
        {
            if (fundName.IsNullOrEmpty() || fundName.Trim().IsNullOrEmpty())
            {
                return Guid.Empty;
            }

            var fundFolder = MasterDatabase.GetItem(Constants.Folders.Funds);
            if (fundFolder == null)
            {
                return Guid.Empty;
            }

            fundName = fundName.Trim();
            var fundItem = fundFolder.Children.FirstOrDefault(r => r[ID.Parse(Constants.FieldIDs.FundNameField)] == fundName);

            return fundItem != null
                ? fundItem.ID.Guid
                : Guid.Empty;

        }

        private IDictionary<Guid, string> GetFiltersWithIds(Guid folder, Guid template, Guid valueField)
        {
            var result = new Dictionary<Guid, string>();

            var lookupItems = MasterDatabase.GetItem(ID.Parse(folder));

            if (lookupItems != null)
            {
                foreach (Item child in lookupItems.Children)
                {
                    if (child.TemplateID.ToGuid() == template)
                    {
                        result.Add(child.ID.ToGuid(), child[ID.Parse(valueField)]);
                    }
                }
            }

            return result;
        }
    }
}