namespace LionTrust.Feature.Fund.Repository
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Security;
    using System;
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

        public FundTeamSearchResultItem GetFundTeamByManagerId(Guid fundManagerId, string databaseName)
        {
            if (fundManagerId == null)
            {
                return null;
            }

            var id = fundManagerId.ToString().Replace("-", string.Empty);
            using (IProviderSearchContext context = ContentSearchManager
                                                            .GetIndex($"sitecore_{databaseName}_index")
                                                            .CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
            {
                var query = context.GetQueryable<FundTeamSearchResultItem>()
                    .Where(f => f.Professionals.Contains(id));
                return query.FirstOrDefault();
            }
        }
    }
}