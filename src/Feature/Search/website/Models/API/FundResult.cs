namespace LionTrust.Feature.Search.Models.API
{
    using LionTrust.Foundation.Search.Models;
    using System;
    using System.Collections.Generic;

    public class FundResult : IFundContentResult
    {
        public Guid FundId { get; set; }

        public string FundName { get; set; }

        public string Url { get; set; }

        public string CTAText { get; set; }

        public string FundCardImageUrl { get; set; }

        public string FundCardHeading { get; set; }

        public string FundCardDescription { get; set; }

        public string FundFactSheet { get; set; }

        public string FundTeam { get; set; }

        public IEnumerable<string> FundManagers { get; set; }

        public IEnumerable<string> FundRange { get; set; }

        public string FundRegion { get; set; }
    }
}