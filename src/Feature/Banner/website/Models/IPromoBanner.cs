﻿namespace LionTrust.Feature.Banner.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Design;
    using System;

    public interface IPromoBanner : IBannerGlassBase
    {
        [SitecoreField(Banner.Constants.PromoBanner.Heading_FieldId, SitecoreFieldType.SingleLineText, "Promo Banner")]
        string Heading { get; set; }

        [SitecoreField(Banner.Constants.PromoBanner.Body_FieldId, SitecoreFieldType.MultiLineText, "Promo Banner")]
        string Body { get; set; }

        [SitecoreField(Banner.Constants.PromoBanner.Image_FieldId, SitecoreFieldType.Image, "Promo Banner")]
        Image Image { get; set; }

        [SitecoreField(Banner.Constants.PromoBanner.PrimaryCTALink_FieldId, SitecoreFieldType.GeneralLink, "Promo Banner")]
        Link PrimaryCTALink { get; set; }
              
        [SitecoreField(Banner.Constants.PromoBanner.SecondaryCTALink_FieldId, SitecoreFieldType.GeneralLink, "Promo Banner")]
        Link SecondaryCTALink { get; set; }

        [SitecoreField(Banner.Constants.PromoBanner.TextAlignment_FieldId, SitecoreFieldType.Droplink, "Design")]
        ILookupValue TextAlignment { get; set; }

        [SitecoreField(Banner.Constants.PromoBanner.PrimaryCtaGoalFieldId)]
        Guid PrimaryCtaGoal { get; set; }

        [SitecoreField(Banner.Constants.PromoBanner.SecondaryCtaGoalFieldId)]
        Guid SecondaryCtaGoal { get; set; }

        [SitecoreField(Banner.Constants.PromoBanner.MarginsContainer_FieldId)]
        ILookupValue MarginsContainer { get; set; }
    }
}