namespace LionTrust.Feature.DocumentUploader.Repository
{
    using LionTrust.Feature.DocumentUploader.Models;
    using Sitecore.Configuration;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Globalization;
    using Sitecore.Resources.Media;
    using Sitecore.SecurityModel;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    public class DocumentUploadRepository : IDocumentUploadRepository
    {
        /// <summary>
        /// Constructor init
        /// </summary>
        public DocumentUploadRepository()
        {
        }

        /// <summary>
        /// Method to upload files into sitecore media library
        /// </summary>
        /// <param name="documentUpload"></param>
        /// <returns></returns>
        public UploadedDocumentsResult UploadDocuments(DocumentUploaderViewModel documentUpload)
        {
            var itemsCreated = new UploadedDocumentsResult
            {
                DocumentsUploaded = new List<UploadedFileViewModel>()
            };
            try
            {
                foreach (var file in documentUpload.UploadedFiles)
                {
                    file.FileAsBinary = file.FileAsBinary.Replace("data:text/xml;base64,", "").Replace("data:application/pdf;base64,", "");
                    var uploadedFileAsBytes = Convert.FromBase64String(file.FileAsBinary);
                    using (var memoryStream = new MemoryStream(uploadedFileAsBytes))
                    {
                        var fileExtension = GetFileExtension(file.FileName);
                        var mediaName = GenerateMediaName(file.FileName);
                        var destination = string.Format("/sitecore/media library/project/liontrust/documents/{0}", mediaName);
                        var mediaCreator = new MediaCreator();
                        var options = new MediaCreatorOptions
                        {
                            IncludeExtensionInItemName = false,
                            Database = Factory.GetDatabase("master"),
                            Destination = destination,
                            Versioned = false,
                            Language = Language.Parse(Settings.DefaultLanguage),
                            FileBased = false
                        };

                        using (new SecurityDisabler())
                        {
                            var createdItem = mediaCreator.CreateFromStream(memoryStream, string.Format("{0}{1}", mediaName, fileExtension), options);
                            if (createdItem != null)
                            {
                                itemsCreated.DocumentsUploaded.Add(file);
                            }  
                        }
                    }
                }
                if(itemsCreated.DocumentsUploaded.Count > 0)
                {
                    itemsCreated.Result = true;
                }
                else
                {
                    itemsCreated.Result = false;
                }
                return itemsCreated;
            }
            catch (Exception ex)
            {
                Log.Error("Upload documents failed", ex);
                itemsCreated.Result = false;
                return itemsCreated;
            }

        }

        private string GenerateMediaName(string fileName)
        {
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var regexPattern = @"\([^)]*\)";
            var filteredName = Regex.Replace(nameWithoutExtension, regexPattern, "");

            return string.Format(
                "{0}-{1}", ItemUtil.ProposeValidItemName(
                filteredName),
                Guid.NewGuid().ToString().Substring(0, 4)
                );
        }

        private string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }
    }
}