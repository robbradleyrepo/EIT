﻿namespace LionTrust.Feature.Listings.Models
{
    using Newtonsoft.Json;
    using System;

    public class DocumentModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("documentLink")]
        public string DocumentLink { get; set; }

        [JsonProperty("documentLinkText")]
        public string DocumentLinkText { get; set; }

        [JsonProperty("documentPageLink")]
        public string DocumentPageLink { get; set; }

        [JsonProperty("documentVideoLink")]
        public string DocumentVideoLink { get; set; }

        [JsonProperty("documentId")]
        public Guid DocumentId { get; set; }
        
        [JsonProperty("customSortOrder")]
        public int CustomSortOrder { get; set; }
    }
}