namespace LionTrust.Feature.Article.Fields
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using System.Collections.Generic;

    [Service(ServiceType = typeof(IExcludedCountriesField), Lifetime = Lifetime.Singleton)]
    public class ArticleExcludedCountiesField : ArticleBaseField, IExcludedCountriesField
    {
        public IList<ID> GetExcludedCountries(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var fundField = (LookupField)item.Fields[Foundation.Legacy.Constants.Article.Fund_FieldId];
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