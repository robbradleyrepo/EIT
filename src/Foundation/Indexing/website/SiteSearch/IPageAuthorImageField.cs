namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using System;
    using System.Collections.Generic;
    using Sitecore.Data.Items;

    public interface IPageAuthorImageField
    {
        bool CanHandle(IEnumerable<Guid> baseTemplateIds);

        string GetAuthorImageUrl(Item item);
    }
}
