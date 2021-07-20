namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;

    public interface IFundFactsheetUrlField
    {
        bool CanHandle(IEnumerable<Guid> templateIds);

        string GetFundFactsheetUrl(Item item);
    }
}
