﻿namespace LionTrust.Feature.Article.Fields
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using System.Collections.Generic;
    using System.Globalization;

    [Service(ServiceType = typeof(IExcludedCountriesField), Lifetime = Lifetime.Singleton)]
    public class ArticleExcludedCountiesField : ArticleBaseField, IExcludedCountriesField
    {
        public IList<string> GetExcludedCountries(Item item)
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

            var results = new List<string>();

            foreach (var id in excludedCountires.TargetIDs)
            {
                var country = item.Database.GetItem(id);

                if (country != null)
                {
                    var countryIso = country.Fields[Foundation.Onboarding.Constants.Country.ISO_FieldId].Value;

                    var regionInfo = new RegionInfo(countryIso);

                    if (regionInfo != null)
                    {
                        results.Add(regionInfo.EnglishName);
                    }
                }
            }

            return results;
        }
    }
}