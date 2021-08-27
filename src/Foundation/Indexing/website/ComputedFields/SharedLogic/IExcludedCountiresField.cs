namespace LionTrust.Foundation.Indexing.ComputedFields.SharedLogic
{
    using System;
    using System.Collections.Generic;
    using Sitecore.Data;
    using Sitecore.Data.Items;

    public interface IExcludedCountriesField
    {
        bool CanHandle(IEnumerable<Guid> templateIds);

        IList<ID> GetExcludedCountries(Item item);
    }
}
