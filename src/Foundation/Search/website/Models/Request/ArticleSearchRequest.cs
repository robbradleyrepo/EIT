namespace LionTrust.Foundation.Search.Models.Request
{
    using System;
    using System.Collections.Generic;

    public class ArticleSearchRequest : ITaxonomySearchRequest
    {
        public string DatabaseName { get; set; }

        public DateTime FromDate { get; set; }

        public IEnumerable<string> ContentTypes { get; set; }

        public IEnumerable<string> Funds { get; set; }

        public IEnumerable<string> Categories { get; set; }

        //Currently this is used against Authors
        public IEnumerable<string> FundManagers { get; set; }

        public IEnumerable<string> FundTeams { get; set; }

        public string SearchTerm { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public DateTime ToDate { get; set; }

        public IEnumerable<Guid> Topics { get; set; }

        public string Country { get; set; }
    }
}