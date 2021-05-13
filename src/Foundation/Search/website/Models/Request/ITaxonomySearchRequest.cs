namespace LionTrust.Foundation.Search.Models.Request
{
    using System;
    using System.Collections.Generic;

    public interface ITaxonomySearchRequest
    {
        string DatabaseName { get; set; }

        DateTime FromDate { get; set; }

        IEnumerable<string> Funds { get; set; }
        
        IEnumerable<string> FundCategories { get; set; }
        
        //Currently this is used against Authors
        IEnumerable<string> FundManagers { get; set; }
        
        IEnumerable<string> FundTeams { get; set; }

        IEnumerable<string> Topics { get; set; }
        
        string SearchTerm { get; set; }

        int Skip { get; set; }

        int Take { get; set; }

        DateTime ToDate { get; set; }
    }
}