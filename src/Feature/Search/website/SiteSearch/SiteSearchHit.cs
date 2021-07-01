namespace LionTrust.Feature.Search.SiteSearch
{
    using System;

    public class SiteSearchHit
    {
        public string ResultType { get; set; }

        public string PageTitle { get; set; }

        public DateTime PageDate { get; set; }

        public string Team { get; set; }

        public string Author { get; set; }

        public string PageType { get; set; }

        public string Copy { get; set; }

        public string Url { get; set; }

        public string AuthorImage { get; set; }

        public string FactsheetUrl { get; set; }

        public string FundTeam { get; set; }

        public string FundTeamUrl { get; set; }
    }
}