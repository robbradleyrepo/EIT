using LionTrust.Feature.DocumentUploader.Models;
using LionTrust.Feature.DocumentUploader.Repository;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;

namespace LionTrust.Feature.DocumentUploader.Services
{
    /// <summary>
    /// Functionalities related to Liontrust Admin modules
    /// </summary>
    public class LTAdminService
    {
        private readonly IDocumentUploadRepository _ltAdminDocUploadRepository;

        private IDictionary<string, string> languageDictionary = new Dictionary<string, string>()
                                            {
                                                {"DA","Danish"},
                                                {"NL","Dutch"},
                                                {"EN","English"},
                                                {"FI","Finnish"},
                                                {"CH","French-Swiss"},
                                                {"FR","French"},
                                                {"DE","German"},
                                                {"IT","Italian"},
                                                {"NO","Norwegian"},
                                                {"ES","Spanish"},
                                                {"SE","Swedish"},
                                                {"PT", "Portuguese"}
                                            };

        public LTAdminService(IDocumentUploadRepository ltAdminDocUploadRepository)
        {
            _ltAdminDocUploadRepository = ltAdminDocUploadRepository;
        }

        /// <summary>
        /// Load document upload page
        /// </summary>
        /// <returns></returns>
        public LTAdminDocumentUploadViewModel LoadDocumentUploadPage()
        {
            var documentTypesDictionary = _ltAdminDocUploadRepository.GetDoctTypesForLTAdminDocUploadModule();
            return new LTAdminDocumentUploadViewModel { DocumentTypes = documentTypesDictionary, DocTypeIdForFactsheet = _ltAdminDocUploadRepository.GetDocuTypeIdForFactsheet(), DocTypeIdForKiid = _ltAdminDocUploadRepository.GetDocTypeIdForKiid() };
        }

        /// <summary>
        /// Upload files into sitecore media library and link it to the fund
        /// </summary>
        /// <param name="documentUploadEntity"></param>
        /// <returns></returns>
        public Dictionary<string, string> UploadDocuments(LTAdminDocumentUploadViewModel documentUploadEntity)
        {
            var uploadErrorDictionary = new Dictionary<string, string>();

            var docTypeIdForFactsheet = _ltAdminDocUploadRepository.GetDocuTypeIdForFactsheet();
            var docTypeIdForKiid = _ltAdminDocUploadRepository.GetDocTypeIdForKiid();
            var basefundlIteratureMediaLibraryPath = "/sitecore/media library/LionTrust/files/fund-literature";
            var selectedDocTypeGuid = new Guid(documentUploadEntity.SeletedDocumentType);

            foreach (var fileItem in documentUploadEntity.UploadedFiles)
            {
                uploadErrorDictionary.Add(fileItem.FileName, string.Empty);
                var fundId = _ltAdminDocUploadRepository.GetFundIdByName(fileItem.FundName);
                if (fundId != Guid.Empty)
                {
                    var fundDocItemName = string.Empty;
                    var documentNameFieldValue = string.Empty;
                    var mediaLibraryPath = string.Empty;

                    if (selectedDocTypeGuid == docTypeIdForFactsheet)
                    {
                        fundDocItemName = string.Format("{0} {1}", fileItem.FactsheetName, fileItem.ShareClassName);
                        documentNameFieldValue = string.Format("{0} {1}", fileItem.FactsheetName, fileItem.ShareClassName);

                        if (fileItem.FundName.StartsWith("GF", StringComparison.InvariantCultureIgnoreCase))
                        {
                            mediaLibraryPath = string.Format("{0}/factsheets/non-uk-dom", basefundlIteratureMediaLibraryPath);
                        }
                        else
                        {
                            mediaLibraryPath = string.Format("{0}/factsheets/uk-dom", basefundlIteratureMediaLibraryPath);
                        }
                    }
                    else if (selectedDocTypeGuid == docTypeIdForKiid)
                    {
                        var languageFolder = languageDictionary.ContainsKey(fileItem.LanguageCode.ToUpper()) ? languageDictionary[fileItem.LanguageCode.ToUpper()] : string.Empty;
                        if (!string.IsNullOrEmpty(languageFolder))
                        {
                            mediaLibraryPath = string.Format("{0}/kiids/{1}", basefundlIteratureMediaLibraryPath, languageFolder.ToLower());
                            if (fileItem.LanguageCode.ToUpper().Equals("EN"))
                            {
                                fundDocItemName = string.Format("KIID {0}", fileItem.ShareClassName);
                                documentNameFieldValue = string.Format("KIID {0}", fileItem.ShareClassName);
                            }
                            else
                            {
                                fundDocItemName = string.Format("KIID {0} {1}", fileItem.ShareClassName, languageFolder);
                                documentNameFieldValue = string.Format("KIID {0} ({1})", fileItem.ShareClassName, languageFolder);
                            }
                        }
                    }

                    Log.Info(string.Format("UploadDocuments() - FileName: {0} |  FundId: {1} | Selected DocType ID: {2} | Fund Document Name: {3} | Document Name field Value: {4}  | Media Library Path: {5} | Overwrite: {6}", fileItem.FileName, fundId.ToString(), selectedDocTypeGuid, fundDocItemName, documentNameFieldValue, mediaLibraryPath, fileItem.SitecoreMediaItemOverwrite), this);

                    if (string.IsNullOrEmpty(fundDocItemName) || string.IsNullOrEmpty(documentNameFieldValue))
                    {
                        Log.Info("UploadDocuments() - Could not determine fund document item name. Continue the loop.", this);
                        uploadErrorDictionary[fileItem.FileName] = "Could not determine fund document item name";
                        continue;
                    }

                    if (string.IsNullOrEmpty(mediaLibraryPath))
                    {
                        Log.Info("UploadDocuments() - Could not determine media library path. Continue the loop.", this);
                        uploadErrorDictionary[fileItem.FileName] = "Could not determine media library path";
                        continue;
                    }

                    try
                    {
                        _ltAdminDocUploadRepository.UploadDocuments(fundId.ToString(), documentUploadEntity.SeletedDocumentType, fundDocItemName, documentNameFieldValue,  fileItem.FileName, fileItem.FileAsBinary, mediaLibraryPath, fileItem.SitecoreMediaItemOverwrite);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(string.Format("UploadDocuments() - Exception occured while uploading the document for file name : {0}. Continue the loop.", fileItem.FileName), ex, this);
                        uploadErrorDictionary[fileItem.FileName] = ex.Message;
                        continue;
                    }
                }
                else
                {
                    Log.Info(string.Format("UploadDocuments() - Uploaded File Name - {0} | No fund found with the name of - {1}. Continue the loop.", fileItem.FileName, fileItem.FundName), this);
                    uploadErrorDictionary[fileItem.FileName] = string.Format("No fund found with the name - {0}", fileItem.FundName);
                    continue;
                }
            }

            return uploadErrorDictionary;
        }
    }
}
