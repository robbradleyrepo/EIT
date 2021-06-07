namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.ORM.Models;
    using System;
    using System.Collections.Generic;

    public interface IFundScroller: IGlassBase
    {
        [SitecoreField(Constants.FundScroller.HeadingFieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.FundScroller.DescriptionFieldId)]
        string Description { get; set; }

        [SitecoreField(Constants.FundScroller.SubtitleFieldId)]
        string Subtitle { get; set; }

        [SitecoreField(Constants.FundScroller.FundsFieldId)]
        IEnumerable<IFundCard> Funds { get; set; }

        [SitecoreField(Constants.FundScroller.PrimaryCaFieldId)]
        Link PrimaryCta { get; set; }

        [SitecoreField(Constants.FundScroller.SecondaryCtaFieldId)]
        Link SecondaryCta { get; set; }

        [SitecoreField(Constants.FundScroller.PrimaryCtaGoalFieldId)]
        Guid PrimaryCtaGoalId { get; set; }

        [SitecoreField(Constants.FundScroller.SecondaryCtaGoalId)]
        Guid SecondaryCtaGoalId { get; set; }

        [SitecoreField(Constants.FundScroller.ViewFundFieldId)]
        string ViewFundLabel { get; set; }
    }
}