namespace LionTrust.Feature.Article.Models
{
    using System.Collections.Generic;

    public class RelatedArticlesViewModel
    {
        public IFeaturedArticles Data { get; set; }

        public IEnumerable<RelatedArticle> RelatedArticles { get; set; } = new RelatedArticle[0];
    }
}