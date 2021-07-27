namespace LionTrust.Feature.Banner.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Design;
    using System;

    public interface IHomepageLeadBannerModel: IBannerGlassBase
    {
        [SitecoreField(Banner.Constants.HomepageLeadBanner.HomepageBannerHeadingLine1FieldId)]
        string HeadingLine1 { get; set; }

        [SitecoreField(Banner.Constants.HomepageLeadBanner.HomepageBannerHeadingLine2FieldId)]
        string HeadingLine2 { get; set; }

        [SitecoreField(Banner.Constants.HomepageLeadBanner.HomepageBannerPrimaryCtaFieldId)]
        Link PrimaryCta { get; set; }

        [SitecoreField(Banner.Constants.HomepageLeadBanner.HomepageBannerSecondaryCtaFieldId)]
        Link SecondaryCta { get; set; }

        [SitecoreField(Banner.Constants.HomepageLeadBanner.HomepageBannerPrimaryCtaGoalFieldId)]
        Guid PrimaryCtaGoalId { get; set; }

        [SitecoreField(Banner.Constants.HomepageLeadBanner.HomepageBannerSecondaryCtaGoalFieldId)]
        Guid SecondaryCtaGoalId { get; set; }

        [SitecoreField(Banner.Constants.HomepageLeadBanner.HomepageBannerCopyFieldId)]
        string Copy { get; set; }

        [SitecoreField(Banner.Constants.HomepageLeadBanner.HomepageBannerTextAlignFieldId)]
        ILookupValue TextAlign { get; set; }
    }
}
