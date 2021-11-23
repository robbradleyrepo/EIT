namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System;

    public interface IPromoType : IGlassBase
    {
        [SitecoreField(Constants.PromoType.ArticleType_FieldId, SitecoreFieldType.Droplink)]
        Guid ArticleType { get; set; }    
    }
}