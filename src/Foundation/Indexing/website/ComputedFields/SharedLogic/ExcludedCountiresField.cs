namespace LionTrust.Foundation.Indexing.ComputedFields.SharedLogic
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using System.Collections.Generic;
    using System.Linq;

    public class ExcludedCountiresField : IComputedIndexField
    {
        private readonly IEnumerable<IExcludedCountriesField> fields;

        public ExcludedCountiresField()
        {
            this.fields = Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetServices<IExcludedCountriesField>();
        }

        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var indexItem = indexable as SitecoreIndexableItem;

            if (indexItem == null)
            {
                return null;
            }

            var item = indexItem.Item;

            var field = fields.FirstOrDefault(f => f.CanHandle(item.Template.BaseTemplates.Select(t => t.ID.Guid)));

            if (field == null)
            {
                return null;
            }

            var excludedCountries = field.GetExcludedCountries(item);

            if(excludedCountries == null)
            {
                return null;
            }

            var results = new List<string>();

            foreach (var id in excludedCountries)
            {
                var country = item.Database.GetItem(id);

                if (country != null)
                {
                    var countryIso = country.Fields[Onboarding.Constants.Country.ISO_FieldId].Value;

                    if (!string.IsNullOrWhiteSpace(countryIso))
                    {
                        results.Add(countryIso);
                    }
                }
            }

            return results;
        }
    }
}