namespace LionTrust.Feature.Article.Fields
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Data.Items;
    using System.Collections.Generic;

    [Service(ServiceType = typeof(IExcludedCountriesField), Lifetime = Lifetime.Singleton)]
    public class ArticleExcludedCountiesField : ArticleBaseField, IExcludedCountriesField
    {
        public IList<string> GetExcludedCountries(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var multiValueField = item.Fields[Foundation.Legacy.Constants.Article.Fund_FieldId];
            return multiValueField != null
                ? ComputedValueHelper.GetMultiListValue(multiValueField, Foundation.Legacy.Constants.FundAccess.ExcludedCountires_FieldId)?.Split('|')
                : null;
        }
    }
}