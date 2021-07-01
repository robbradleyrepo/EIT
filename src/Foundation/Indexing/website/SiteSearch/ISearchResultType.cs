namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using System;
    using System.Collections.Generic;

    public interface ISearchResultType
    {
        bool CanHandle(IEnumerable<Guid> templateIds);

        string GetSearchResultType();
    }
}
