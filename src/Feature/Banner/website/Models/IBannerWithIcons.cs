namespace LionTrust.Feature.Banner.Models
{
    using System;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface IBannerWithIcons : IBannerGlassBase
    {
        [SitecoreField(Constants.BannerWithIcons.Heading_FieldId, SitecoreFieldType.SingleLineText, "Content")]
        string Heading { get; set; }

        [SitecoreChildren]
        IEnumerable<IIconWithText> Icons { get; set; }

        [SitecoreField(Constants.BannerWithIcons.PrimaryCTALink_FieldId, SitecoreFieldType.GeneralLink, "Content")]
        Link PrimaryCTALink { get; set; }

        [SitecoreField(Constants.BannerWithIcons.PrimaryCTALinkGoal_FieldId, SitecoreFieldType.Droplink, "Content")]
        Guid PrimaryCTALinkGoal { get; set; }

        [SitecoreField(Constants.BannerWithIcons.SecondaryCTALink_FieldId, SitecoreFieldType.GeneralLink, "Content")]
        Link SecondaryCTALink { get; set; }

        [SitecoreField(Constants.BannerWithIcons.SecondaryCTALinkGoal_FieldId, SitecoreFieldType.Droplink, "Content")]
        Guid SecondaryCTALinkGoal { get; set; }
    }
}