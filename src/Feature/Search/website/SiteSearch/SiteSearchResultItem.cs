namespace LionTrust.Feature.Search.SiteSearch
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;

    public class SiteSearchResultItem: SearchResultItem
    {
        [IndexField("page_url_s")]
        public string PageUrl { get; set; }

        [IndexField("legacypresentationbase_shortdescription_s")]
        public string Copy { get; set; }

        [IndexField("legacypresentationbase_pagetitle_s")]
        public string PageTitle { get; set; }

        [IndexField("page_author_s")]
        public string Authors { get; set; }

        [IndexField("page_author_image_url_s")]
        public string AuthorImageUrl { get; set; }

        [IndexField("fund_team_name_s")]
        public string FundTeamName { get; set; }

        [IndexField("fund_team_page_s")]
        public string FundTeamPage { get; set; }

        [IndexField("search_result_type_s")]
        public string ResultType { get; set; }
    }
}