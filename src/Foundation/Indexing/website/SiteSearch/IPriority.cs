namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using System;
    using System.Collections.Generic;

    public interface IPriority
    {
        bool CanHandle(IEnumerable<Guid> templateIds);

        int GetPriority();
    }    
}
