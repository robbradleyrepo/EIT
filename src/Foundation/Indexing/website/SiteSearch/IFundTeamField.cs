namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;

    public interface IFundTeamField
    {
        bool CanHandle(IEnumerable<Guid> templateIds);

        string GetFundTeam(Item item);
    }
}
