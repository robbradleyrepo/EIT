namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    [Service(ServiceType = typeof(IExcludedCountriesField), Lifetime = Lifetime.Singleton)]
    public class FundDetailPageExcludedCountiresField : IExcludedCountriesField
    {
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Constants.FundSelector.TemplateId);
        }

        public IList<ID> GetExcludedCountries(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var fundField = (LookupField)item.Fields[Constants.FundSelector.FundFieldId];
            if (fundField == null || fundField.TargetItem == null)
            {
                return null;
            }

            var excludedCountires = (MultilistField)fundField.TargetItem.Fields[Foundation.Legacy.Constants.FundAccess.ExcludedCountires_FieldId];
            if (excludedCountires == null || excludedCountires.TargetIDs == null)
            {
                return null;
            }

            return excludedCountires.TargetIDs;
        }
    }
}