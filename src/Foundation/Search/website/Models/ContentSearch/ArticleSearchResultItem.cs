namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    using System;
    using System.Collections.Generic;

    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;

    public class ArticleSearchResultItem : SearchResultItem
    {
        //Fund Manager        
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

        //Fund Category
        [IndexField("LegacyArticle_BlogType")]
        public string ArticleCategory { get; set; }
        
        //Fund Team
        [IndexField("Article_Date")]
        public DateTime ArticleDate { get; set; }
        
        [IndexField("LegacyArticle_Team")]
        public IEnumerable<string> ArticleTeam { get; set; }

        [IndexField("Article_Topics")]
        public IEnumerable<string> Topics { get; set; }

        [IndexField("__Created")]
        public DateTime Created { get; set; }
    }
}