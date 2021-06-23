namespace LionTrust.Foundation.Search.Models
{
    using System;

    public interface IFundContentResult
    {
        Guid FundId { get; set; }

        string FundName { get; set; }

        string FundCardHeading { get; set; }

        string FundCardDescription { get; set; }

        string FundCardImageUrl { get; set; }

        string CTAText { get; set; }

        string Url { get; set; }

        string FundFactSheet { get; set; }

    }
}