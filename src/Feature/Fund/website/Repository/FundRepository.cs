namespace LionTrust.Feature.Fund.Repository
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Security;
    using System.Linq;

    [Service(ServiceType = typeof(IFundRepository), Lifetime = Lifetime.Singleton)]
    public class FundRepository : IFundRepository
    {
        public FundSearchResultItem GetFundByClass(IFundClass fundClass, string databaseName)
        {
            if (fundClass == null)
            {
                return null;
            }

            var id = fundClass.Id.ToString().Replace("-", string.Empty);
            using (IProviderSearchContext context = ContentSearchManager
                                                            .GetIndex($"sitecore_{databaseName}_index")
                                                            .CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
            {
                var query = context.GetQueryable<FundSearchResultItem>()
                    .Where(f => f.FundClasses.Contains(id));
                return query.FirstOrDefault();
            }
        }
    }
}