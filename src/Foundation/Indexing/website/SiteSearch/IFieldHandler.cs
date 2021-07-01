namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using System;
    using System.Collections.Generic;

    public interface IFieldHandler
    {
        bool CanHandle(IEnumerable<Guid> templateIds);
    }
}
