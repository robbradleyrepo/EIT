namespace LionTrust.Feature.Promo.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    public interface IArticlePromo : IArticle, IPromoGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IPodcastPromo> PodcastPromo { get; set; }
    }
}