namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(IExcludedCountriesField), Lifetime = Lifetime.Singleton)]
    public class FundExcludedCountiresField : IExcludedCountriesField
    {
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Constants.FundPage.TemplateId);
        }

        public IList<string> GetExcludedCountries(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var excludedCountries = item.Fields[Foundation.Legacy.Constants.FundAccess.ExcludedCountires_FieldId];
            return excludedCountries != null
                ? ComputedValueHelper.ExtractIds(excludedCountries)?.Select(x => Convert.ToString(x))?.ToList()
                : null;
        }
    }
}