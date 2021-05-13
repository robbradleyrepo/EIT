namespace LionTrust.Feature.Article.Models
{
    using System.Collections.Generic;

    public class FeaturedArticlesViewModel
    {
        public IFeaturedArticles Data { get; set; }

        public IEnumerable<FeaturedArticle> FeaturedArticles { get; set; } = new FeaturedArticle[0];
    }
}