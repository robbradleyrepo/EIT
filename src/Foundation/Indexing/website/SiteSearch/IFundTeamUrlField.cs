namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;

    public interface IFundTeamUrlField
    {
        bool CanHandle(IEnumerable<Guid> templateIds);

        string GetFundTeamUrl(Item item);
    }
}
