namespace LionTrust.Feature.Banner.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System;

    public interface IMyFundsLeadBanner: IBannerGlassBase
    {
        [SitecoreField(Banner.Constants.MyFundsLeadBanner.TitleFieldId)]
        string Title { get; set; }

        [SitecoreField(Banner.Constants.MyFundsLeadBanner.CtaFieldId)]
        Link Cta { get; set; }

        [SitecoreField(Banner.Constants.MyFundsLeadBanner.CtaGoalFieldId)]
        Guid CtaGoalId { get; set; }

        [SitecoreField(Banner.Constants.MyFundsLeadBanner.BodyFieldId)]
        string Body { get; set; }

        [SitecoreField(Banner.Constants.MyFundsLeadBanner.NoContactTitleFieldId)]
        string NoContactTitle { get; set; }

        [SitecoreField(Banner.Constants.MyFundsLeadBanner.NoContactCtaFieldId)]
        Link NoContactCta { get; set; }

        [SitecoreField(Banner.Constants.MyFundsLeadBanner.NoContactBodyFieldId)]
        string NoContactBody { get; set; }
    }
}
