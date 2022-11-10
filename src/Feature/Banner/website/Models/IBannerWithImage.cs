namespace LionTrust.Feature.Banner.Models
{
    using System;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface IBannerWithImage : IBannerGlassBase
    {
        [SitecoreField(Constants.BannerWithImage.Heading_FieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.BannerWithImage.Copy_FieldId)]
        string Copy { get; set; }

        [SitecoreField(Constants.BannerWithImage.Image_FieldId)]
        Image Image { get; set; }

        [SitecoreField(Constants.BannerWithImage.PrimaryCTALink_FieldId)]
        Link PrimaryCTALink { get; set; }

        [SitecoreField(Constants.BannerWithImage.PrimaryCTALinkGoal_FieldId)]
        Guid PrimaryCTALinkGoal { get; set; }

        [SitecoreField(Constants.BannerWithImage.SecondaryCTALink_FieldId)]
        Link SecondaryCTALink { get; set; }

        [SitecoreField(Constants.BannerWithImage.SecondaryCTALinkGoal_FieldId)]
        Guid SecondaryCTALinkGoal { get; set; }
    }
}