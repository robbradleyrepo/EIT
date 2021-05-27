
namespace LionTrust.Feature.Article.Models
{
    using Sitecore.Annotations;
    using System.Collections.Generic;
    using System.Linq;

    public class FundManagerInsightsViewModel
    {
        public FundManagerInsightsViewModel([NotNull]IFundManagerInsights data, IEnumerable<IArticlePromo> articles)
        {
            Data = data;
            Articles = articles;
        }

        public IFundManagerInsights Data { get; private set; }

        public IEnumerable<IArticlePromo> Articles { get; private set; }
    }
}