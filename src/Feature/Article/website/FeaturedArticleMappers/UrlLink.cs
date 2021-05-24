namespace LionTrust.Feature.Article.FeaturedArticleMappers
{
    using LionTrust.Feature.Article.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class UrlLink
    {
        public static IEnumerable<FeaturedArticle> Map(IFeaturedArticles data)
        {
            if (data.Children == null || !data.Children.Any())
            {
                return new FeaturedArticle[0];
            }

            return data.Children
                .Where(c => c.Link != null)
                .Select(c => new FeaturedArticle { Url = c.Link.Url, Content = c.Link.Text });
        }
    }
}