namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(ISalesforceFundId), Lifetime = Lifetime.Singleton)]
    public class SalesforceFundIdField : ISalesforceFundId
    {
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Constants.FundPage.TemplateId);
        }

        public string GetSalesforceFundId(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var salesforceFund = (LookupField)item.Fields[Foundation.Legacy.Constants.Fund.SalesforceFundFieldId];

            if (salesforceFund == null || salesforceFund.TargetItem == null)
            {
                return null;
            }

            return salesforceFund.TargetItem[Constants.SalesforceFund.SalesforceFundId_FieldId];
        }
    }
}