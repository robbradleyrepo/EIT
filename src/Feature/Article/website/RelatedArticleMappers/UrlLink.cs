namespace LionTrust.Feature.Article.RelatedArticleMappers
{
    using LionTrust.Feature.Article.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class UrlLink
    {
        public static IEnumerable<RelatedArticle> Map(IFeaturedArticles data)
        {
            if (data.Children == null || !data.Children.Any())
            {
                return new RelatedArticle[0];
            }

            return data.Children
                .Where(c => c.Link != null)
                .Select(c => new RelatedArticle { Url = c.Link.Url, Content = c.Link.Text });
        }
    }
}