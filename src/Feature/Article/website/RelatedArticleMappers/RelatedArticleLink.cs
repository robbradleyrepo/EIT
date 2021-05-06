namespace LionTrust.Feature.Article.RelatedArticleMappers
{
    using LionTrust.Feature.Article.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class RelatedArticleLink
    {
        public static IEnumerable<RelatedArticle> Map(IFeaturedArticles data)
        {
            if (data == null || data.Articles == null || !data.Articles.Any())
            {
                return new RelatedArticle[0];
            }

            return data.Articles.Select(a => new RelatedArticle { Url = a.Url, Content = a.Title });
        }
    }
}