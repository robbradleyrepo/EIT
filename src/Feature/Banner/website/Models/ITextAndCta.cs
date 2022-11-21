namespace LionTrust.Feature.Banner.Models
{
    using System;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface ITextAndCta : IBannerGlassBase
    {
        [SitecoreField(Constants.TextAndCTA.Text_FieldId)]
        string Text { get; set; }

        [SitecoreField(Constants.TextAndCTA.CTALink_FieldId)]
        Link CTALink { get; set; }

        [SitecoreField(Constants.TextAndCTA.CTALinkGoal_FieldId)]
        Guid CTALinkGoal { get; set; }
    }
}