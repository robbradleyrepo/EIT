namespace LionTrust.Feature.Promo.Models
{
    using System;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface IImageCTAPromo : IPromoGlassBase
    {
        [SitecoreField(Constants.ImageCTAPromo.Image_FieldId, SitecoreFieldType.Image, "Image CTA Promo")]
        Image Image { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.Title_FieldId, SitecoreFieldType.SingleLineText, "Image CTA Promo")]
        string Title { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.Subtitle_FieldId, SitecoreFieldType.SingleLineText, "Image CTA Promo")]
        string Subtitle { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.Body_FieldId, SitecoreFieldType.MultiLineText, "Image CTA Promo")]
        string Body { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.CTALink_FieldId, SitecoreFieldType.GeneralLink, "Image CTA Promo")]
        Link CTALink { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.CTAGoal_FieldId, SitecoreFieldType.Droplink, "Image CTA Promo")]
        Guid CTAGoal { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.CTALink2_FieldId, SitecoreFieldType.GeneralLink, "Image CTA Promo")]
        Link CTALink2 { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.CTAGoal2_FieldId, SitecoreFieldType.Droplink, "Image CTA Promo")]
        Guid CTAGoal2 { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.TextAlignment_FieldId, SitecoreFieldType.Droplink, "Design")]
        IPromoLookup TextAlignment { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.LinkedInUrlFieldId)]
        Link LinkedInUrl { get; set; }
    }
}