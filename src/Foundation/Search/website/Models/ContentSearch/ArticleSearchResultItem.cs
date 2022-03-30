namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    using System;
    using System.Collections.Generic;

    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;

    public class ArticleSearchResultItem : BaseSearchResultItem
    {
        [IndexField("_latestversion")]
        public bool IsLatestVersion { get; set; }

        //Fund Manager        
        [IndexField("LegacyArticle_Authors")]
        public IEnumerable<string> ArticleAuthors { get; set; }

        [IndexField("article_author_image_protected")]
        public string ArticleAuthorImage { get; set; }

        [IndexField("article_url")]
        public string ArticleUrl { get; set; }

        [IndexField("article_author_names")]
        public string ArticleAuthorNames { get; set; }

        [IndexField("LegacyArticle_Fund")]
        public string ArticleFund { get; set; }

        [IndexField("article_fund_name")]
        public string ArticleFundName { get; set; }

        [IndexField("LegacyArticle_Title")]
        public string ArticleTitle { get; set; }

        [IndexField("article_content")]
        public string ArticleContent { get; set; }

        [IndexField("LegacyArticle_Subtitle")]
        public string ArticleSubtitle { get; set; }

        [IndexField("LegacyArticle_ShortDescription")]
        public string ArticleShortDescription { get; set; }

        //Fund Category
        [IndexField("LegacyArticle_BlogType")]
        public string ArticleContentType { get; set; }

        [IndexField("article_contentType_name")]
        public string ArticleContentTypeName { get; set; }

        [IndexField("Article_Date")]
        public DateTime ArticleDate { get; set; }

        [IndexField("article_listing_image_protected")]
        public string ArticleListingImage { get; set; }

        [IndexField("Article_ListingImageOpacity")]
        public string ArticleListingImageOpacity { get; set; }

        [IndexField("LegacyArticle_Team")]
        public IEnumerable<string> ArticleTeam { get; set; }

        [IndexField("Article_Topics")]
        public IEnumerable<string> Topics { get; set; }

        [IndexField("article_video")]
        public string ArticleVideoUrl { get; set; }

        [IndexField("article_podcast")]
        public string ArticlePodcast { get; set; }

        [IndexField("article_topic_names")]
        public string TopicNames { get; set; }

        [IndexField("__Created")]
        public DateTime Created { get; set; }

        [IndexField("article_author_teams")]
        public IEnumerable<string> ArticleAuthorTeams { get; set; }

        [IndexField("article_created_date")]
        public DateTime ArticleCreatedDate { get; set; }

        [IndexField("article_created_date_month")]
        public int ArticleCreatedDateMonth { get; set; }

        [IndexField("article_created_date_year")]
        public int ArticleCreatedDateYear { get; set; }
    }
}