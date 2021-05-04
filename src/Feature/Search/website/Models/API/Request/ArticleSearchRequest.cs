namespace LionTrust.Feature.Search.Models.API.Request
{
    using System;
    using System.Collections.Generic;

    using LionTrust.Foundation.Search.Models.Request;

    public class ArticleSearchRequest : ITaxonomySearchRequest
    {
        public DateTime FromDate { get; set; }

        public IEnumerable<string> Funds { get; set; }

        public IEnumerable<string> FundCategories { get; set; }

        //Currently this is used against Authors
        public IEnumerable<string> FundManagers { get; set; }

        public IEnumerable<string> FundTeams { get; set; }

        public string SearchTerm { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public DateTime ToDate { get; set; }
    }
}