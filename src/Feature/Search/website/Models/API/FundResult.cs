namespace LionTrust.Feature.Search.Models.API
{
    using LionTrust.Foundation.Search.Models;
    using System;

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
    }
}