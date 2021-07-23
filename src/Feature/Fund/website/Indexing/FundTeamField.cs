namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(IFundTeamField), Lifetime = Lifetime.Singleton)]
    public class FundTeamField : IFundTeamField
    {
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Constants.FundPage.TemplateId);
        }

        public string GetFundTeam(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var fundField = (Sitecore.Data.Fields.LookupField) item.Fields[Constants.FundSelector.FundFieldId];
            if (fundField == null || fundField.TargetItem == null)
            {
                return null;
            }

            var fundTeamField = (Sitecore.Data.Fields.LookupField) fundField.TargetItem.Fields[Foundation.Legacy.Constants.Fund.FundTeamFieldId];
            if (fundTeamField == null)
            {
                return null;
            }

            return fundTeamField.TargetItem[Foundation.Legacy.Constants.FundTeam.NameFieldId];
        }
    }
}