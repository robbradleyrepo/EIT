namespace LionTrust.Foundation.Search.Models.Request
{
    using System.Collections.Generic;

    public class FundSearchRequest : ISearchRequest
    {
        public string DatabaseName { get; set; }

        public IEnumerable<string> FundManagers { get; set; }

        public IEnumerable<string> FundTeams { get; set; }

        public IEnumerable<string> FundRanges { get; set; }

        public IEnumerable<string> FundRegions { get; set; }

        public IEnumerable<string> CitiCodes { get; set; }

        public IEnumerable<string> ExcludeCitiCodes { get; set; }

        public string SearchTerm { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}