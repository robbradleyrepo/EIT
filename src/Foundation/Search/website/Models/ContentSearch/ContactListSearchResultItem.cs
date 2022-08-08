using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;

namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    public class ContactListSearchResultItem : BaseSearchResultItem
    {
        [IndexField("name")]
        public new string Name { get; set; }

        [IndexField("createddate")]
        public DateTime CreatedDate { get; set; }

        [IndexField("__created_by")]
        public string CreatedBy { get; set; }

        [IndexField("type")]
        public string Type { get; set; }

        [IndexField("_templatename")]
        public string TypeName { get; set; }

        [IndexField("lastmodifieddate")]
        public DateTime Updated { get; set; }

        [IndexField("active")]
        public bool Active { get; set; }
    }
}