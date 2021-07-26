namespace LionTrust.Feature.Fund.Repository
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    using System;

    public class FundTeamSearchResultItem : SearchResultItem
    {
        [IndexField("fundteam_name_t")]
        public string FundTeamName { get; set; }

        [IndexField("fundteam_page_sm")]
        public string[] Page { get; set; }

        [IndexField("professionals_sm")]
        public string[] Professionals { get; set; }
    }
}