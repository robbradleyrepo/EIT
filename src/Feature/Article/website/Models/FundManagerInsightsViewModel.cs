
namespace LionTrust.Feature.Article.Models
{
    using Sitecore.Annotations;
    using System.Collections.Generic;

    public class FundManagerInsightsViewModel
    {
        public FundManagerInsightsViewModel([NotNull]IFundManagerInsightsBase data, IEnumerable<IArticlePromo> articles)
        {
            Data = data;
            Articles = articles;
        }

        public IFundManagerInsightsBase Data { get; private set; }

        public IEnumerable<IArticlePromo> Articles { get; private set; }
    }
}