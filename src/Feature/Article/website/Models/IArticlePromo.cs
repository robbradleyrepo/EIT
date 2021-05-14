namespace LionTrust.Feature.Article.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    
    public interface IArticlePromo : IArticle, IArticleGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IArticlePodcastPromo> PodcastPromo { get; set; }
    }
}