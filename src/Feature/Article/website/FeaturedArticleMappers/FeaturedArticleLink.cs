namespace LionTrust.Feature.Article.FeaturedArticleMappers
{
    using LionTrust.Feature.Article.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class FeaturedArticleLink
    {
        public static IEnumerable<FeaturedArticle> Map(IFeaturedArticles data)
        {
            if (data == null || data.Articles == null || !data.Articles.Any())
            {
                return new FeaturedArticle[0];
            }

            return data.Articles.Select(a => new FeaturedArticle { Url = a.Url, Content = a.Title });
        }
    }
}