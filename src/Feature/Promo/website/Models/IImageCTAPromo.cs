namespace LionTrust.Feature.Promo.Models
{
    using System;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface IImageCTAPromo : IPromoGlassBase
    {
        [SitecoreField(Constants.ImageCTAPromo.Image_FieldId)]
        Image Image { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.Subtitle_FieldId)]
        string Subtitle { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.Body_FieldId)]
        string Body { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.CTALink_FieldId)]
        Link CTALink { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.CTAGoal_FieldId)]
        Guid CTAGoal { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.CTALink2_FieldId)]
        Link CTALink2 { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.CTAGoal2_FieldId)]
        Guid CTAGoal2 { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.TextAlignment_FieldId)]
        IPromoLookup TextAlignment { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.TitleColor_FieldId)]
        Foundation.Design.ILookupValue TitleColor { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.LinkedInUrlFieldId)]
        Link LinkedInUrl { get; set; }

        [SitecoreField(Constants.ImageCTAPromo.TitleInSide_FieldId)]
        bool TitleInSide { get; set; }
    }
}