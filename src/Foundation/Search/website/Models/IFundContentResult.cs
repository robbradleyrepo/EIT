namespace LionTrust.Foundation.Search.Models
{
    using System;
    using System.Collections.Generic;

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

        string FundTeam { get; set; }

        IEnumerable<string> FundManagers { get; set; }

        IEnumerable<string> FundRange { get; set; }

        string FundRegion { get; set; }

        string FundUpdateUrl { get; set; }

        string FundTeamName { get; set; }

        string SalesforceFundId { get; set; }

    }
}