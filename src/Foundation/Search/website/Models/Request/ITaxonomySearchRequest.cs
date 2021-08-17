namespace LionTrust.Foundation.Search.Models.Request
{
    using System;
    using System.Collections.Generic;

    public interface ITaxonomySearchRequest : ISearchRequest
    {
        DateTime FromDate { get; set; }

        IEnumerable<string> ContentTypes { get; set; }

        IEnumerable<string> Funds { get; set; }
        
        IEnumerable<string> Categories { get; set; }
        
        //Currently this is used against Authors
        IEnumerable<string> FundManagers { get; set; }
        
        IEnumerable<string> FundTeams { get; set; }
        
        DateTime ToDate { get; set; }

        IEnumerable<Guid> Topics { get; set; }
    }
}