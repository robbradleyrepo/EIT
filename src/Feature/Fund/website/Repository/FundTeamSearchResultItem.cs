namespace LionTrust.Feature.Fund.Repository
{
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using Sitecore.ContentSearch;

    public class FundTeamSearchResultItem : BaseSearchResultItem
    {
        [IndexField("fundteam_name_t")]
        public string FundTeamName { get; set; }

        [IndexField("fundteam_page_sm")]
        public string[] Page { get; set; }

        [IndexField("professionals_sm")]
        public string[] Professionals { get; set; }
    }
}