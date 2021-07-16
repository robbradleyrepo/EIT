namespace LionTrust.Feature.Article.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Article.Models;

    public interface IArticlePromo : IArticle, IArticleGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IArticlePodcastPromo> PodcastPromo { get; set; }
    }
}