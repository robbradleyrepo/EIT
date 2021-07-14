namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;
    public interface IFundManagerInsightsBase : IGlassBase
    {
        [SitecoreField(Constants.FundManagerInsightsBase.CTA_FieldId)]
        Link CTA { get; set; }

        [SitecoreField(Constants.FundManagerInsightsBase.Heading_FieldId)]
        string Heading { get; set; }
    }
}