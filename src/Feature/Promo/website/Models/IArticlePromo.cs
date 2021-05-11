namespace LionTrust.Feature.Promo.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Article.Models;
    
    public interface IArticlePromo : IArticle, IPromoGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IPodcastPromo> PodcastPromo { get; set; }
    }
}