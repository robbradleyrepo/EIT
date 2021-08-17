namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    using System;
    using System.Collections.Generic;

    public class BaseSearchResultItem : SearchResultItem
    {
        [IndexField("alltemplates")]
        public IList<Guid> Templates { get; set; }

        [IndexField("excluded_countries")]
        public IList<string> ExcludedCountries { get; set; }
    }
}
