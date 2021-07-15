namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System.Collections.Generic;
    public interface IFundManagerInsights : IFundManagerInsightsBase, IArticleFilter
    {
        [SitecoreField(Constants.FundManagerInsights.SelectedArticles_FieldId)]
        IEnumerable<IArticlePromo> SelectedArticles { get; set; }
    }
}