﻿namespace LionTrust.Feature.Banner.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Design;
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

    }
}