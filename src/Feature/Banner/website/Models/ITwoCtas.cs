namespace LionTrust.Feature.Banner.Models
{
    using System;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface ITwoCtas : IBannerGlassBase
    {
        [SitecoreField(Constants.TwoCTAs.PrimaryCTALink_FieldId)]
        Link PrimaryCTALink { get; set; }

        [SitecoreField(Constants.TwoCTAs.PrimaryCTALinkGoal_FieldId)]
        Guid PrimaryCTALinkGoal { get; set; }

        [SitecoreField(Constants.TwoCTAs.SecondaryCTALink_FieldId)]
        Link SecondaryCTALink { get; set; }

        [SitecoreField(Constants.TwoCTAs.SecondaryCTALinkGoal_FieldId)]
        Guid SecondaryCTALinkGoal { get; set; }
    }
}