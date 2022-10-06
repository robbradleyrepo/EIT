namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.ORM.Models;
    using System;

    public interface IFundHeader : IGlassBase
    {
        [SitecoreField(Constants.FundHeader.FundFieldId)]
        IFund Fund { get; set; }

        [SitecoreField(Constants.FundHeader.TitleFieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.FundHeader.SubtitleFieldId)]
        string Subtitle { get; set; }

        [SitecoreField(Constants.FundHeader.FundManagerFieldId)]
        IAuthor FundManager { get; set; }

        [SitecoreField(Constants.FundHeader.BackgroundImageFieldId)]
        Image BackgroundImage { get;set; }

        [SitecoreField(Constants.FundHeader.FundSharingFieldId)]
        IFundShareLink FundSharing { get; set; }

        [SitecoreField(Constants.FundHeader.PrimaryCtaLink)]
        Link PrimaryCta { get; set; }

        [SitecoreField(Constants.FundHeader.PrimaryCtaLinkGoal)]
        Guid PrimaryCtaGoalId { get; set; }

        [SitecoreField(Constants.FundHeader.SecondaryCtaLink)]
        Link SecondaryCta { get; set; }

        [SitecoreField(Constants.FundHeader.SecondaryCtaLinkGoal)]
        Guid SecondaryCtaGoalId { get; set; }
    }
}
