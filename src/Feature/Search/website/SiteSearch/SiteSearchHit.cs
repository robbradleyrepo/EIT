namespace LionTrust.Feature.Search.SiteSearch
{
    using System;
    using System.Collections.Generic;

    public class SiteSearchHit
    {
        public string ResultType { get; set; }

        public string PageTitle { get; set; }

        public string PageDate { get; set; }

        public string Author { get; set; }

        public string Copy { get; set; }

        public string Url { get; set; }

        public string AuthorImage { get; set; }

        public string FactsheetUrl { get; set; }

        public string FundTeam { get; set; }

        public string FundTeamUrl { get; set; }

        public Guid TemplateId { get; set; }
    }
}