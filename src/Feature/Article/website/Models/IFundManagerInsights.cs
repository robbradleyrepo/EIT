namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;
    public interface IFundManagerInsights : IGlassBase, IArticleFilter
    {
        [SitecoreField(Constants.FundManagerInsights.SelectedArticles_FieldId)]
        IEnumerable<IArticlePromo> SelectedArticles { get; set; }

        [SitecoreField(Constants.FundManagerInsights.CTA_FieldId)]
        Link CTA { get; set; }

        [SitecoreField(Constants.FundManagerInsights.Heading_FieldId)]
        string Heading { get; set; }
    }
}