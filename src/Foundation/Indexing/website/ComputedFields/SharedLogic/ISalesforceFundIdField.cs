namespace LionTrust.Foundation.Indexing.ComputedFields.SharedLogic
{
    using System;
    using System.Collections.Generic;
    using Sitecore.Data.Items;

    public interface ISalesforceFundId
    {
        bool CanHandle(IEnumerable<Guid> templateIds);

        string GetSalesforceFundId(Item item);
    }
}
