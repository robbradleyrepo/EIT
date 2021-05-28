namespace LionTrust.Feature.Promo.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    [SitecoreType(TemplateId = Constants.PagePromoItem.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface IPagePromoItem : IPromoGlassBase
    {
        [SitecoreField(Constants.PagePromoItem.HeadingFieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.PagePromoItem.TextFieldId)]
        string Text { get; set; }

        [SitecoreField(Constants.PagePromoItem.ImageFieldId)]
        Image Image { get; set; }

        [SitecoreField(Constants.PagePromoItem.CTAFieldId)]
        Link CTA { get; set; }
    }
}