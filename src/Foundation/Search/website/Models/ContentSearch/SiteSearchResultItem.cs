namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    using System;
    using System.Collections.Generic;

    using Sitecore.ContentSearch;

    public class SiteSearchResultItem : BaseSearchResultItem
    {       
        [IndexField("LegacyArticle_Authors")]
        public IEnumerable<string> ArticleAuthors { get; set; }

        [IndexField("LegacyArticle_Fund")]
        public string ArticleFund { get; set; }

        [IndexField("LegacyArticle_Title")]
        public string ArticleTitle { get; set; }

        [IndexField("LegacyArticle_Fund")]
        public string ArticleContent { get; set; }

        [IndexField("LegacyArticle_Subtitle")]
        public string ArticleSubtitle { get; set; }

        [IndexField("LegacyArticle_BlogType")]
        public string ArticleCategory { get; set; }

        [IndexField("LegacyArticle_Date")]
        public DateTime ArticleDate { get; set; }

        [IndexField("LegacyArticle_Team")]
        public IEnumerable<string> ArticleTeam { get; set; }
    }
}
