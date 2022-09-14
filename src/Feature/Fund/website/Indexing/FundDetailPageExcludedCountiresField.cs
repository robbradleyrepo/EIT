namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(IExcludedCountriesField), Lifetime = Lifetime.Singleton)]
    public class FundDetailPageExcludedCountiresField : IExcludedCountriesField
    {
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Constants.FundSelector.TemplateId);
        }

        public IList<string> GetExcludedCountries(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var fundField = item.Fields[Constants.FundSelector.FundFieldId];
            return fundField != null
                ? ComputedValueHelper.GetDropLinkFieldValue(fundField, Foundation.Legacy.Constants.FundAccess.ExcludedCountires_FieldId)?.Split('|')
                : null;            
        }
    }
}