namespace LionTrust.Feature.Promo.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.PagePromo.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface IPagePromo : IPromoGlassBase
    {        
        [SitecoreChildren(TemplateId = Constants.PagePromoItem.TemplateId)]
        IEnumerable<IPagePromoItem> PromoItems { get; set; }
    }
}