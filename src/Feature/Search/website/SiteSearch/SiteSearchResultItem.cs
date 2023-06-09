﻿namespace LionTrust.Feature.Search.SiteSearch
{
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using Sitecore.ContentSearch;
    using System;

    public class SiteSearchResultItem : BaseSearchResultItem
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

        [IndexField("fund_factsheet_url")]
        public string FactSheetUrl { get; set; }

        [IndexField("LegacyDocument_DocumentName")]
        public string DocumentName { get; set; }

        [IndexField("LegacyDocument_DocumentDescription")]
        public string DocumentDescription { get; set; }

        [IndexField("Article_Date")]
        public DateTime ArticleDate { get; set; }

        [IndexField("article_created_date")]
        public DateTime ArticleCreatedDate { get; set; }

        [IndexField("related_fund_name")]
        public string RelatedFundName { get; set; }

        [IndexField("related_fund_url")]
        public string RelatedFundUrl { get; set; }

        [IndexField("priority")]
        public int Priority { get; set; }
    }
}