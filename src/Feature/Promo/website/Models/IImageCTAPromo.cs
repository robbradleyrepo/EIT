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

        [SitecoreField(Constants.ImageCTAPromo.TextAlignment_FieldId, SitecoreFieldType.Droplink, "Design")]
        IPromoLookup TextAlignment { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.HideImage_FieldId, SitecoreFieldType.Checkbox, "Design")]
        bool HideImage { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.HideTitle_FieldId, SitecoreFieldType.Checkbox, "Design")]
        bool HideTitle { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.HideSubtitle_FieldId, SitecoreFieldType.Checkbox, "Design")]
        bool HideSubtitle { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.HideBody_FieldId, SitecoreFieldType.Checkbox, "Design")]
        bool HideBody { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.HideCTA_FieldId, SitecoreFieldType.Checkbox, "Design")]
        bool HideCTA { get; set; }
    }
}