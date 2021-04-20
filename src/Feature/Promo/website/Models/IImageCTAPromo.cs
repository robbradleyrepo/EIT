namespace LionTrust.Feature.Promo.Models
{
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

        [SitecoreField(Constants.ImageCTAPromo.CTALabel_FieldId, SitecoreFieldType.SingleLineText, "Image CTA Promo")]
        string CTALabel { get; set; }

    }
}