namespace LionTrust.Feature.Promo.Models
{
    using System;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;    

    public interface IImagePromo : IPromoGlassBase
    {
        [SitecoreField(Constants.ImagePromo.Heading_FieldId, SitecoreFieldType.SingleLineText, "Promo Banner")]
        string Heading { get; set; }

        [SitecoreField(Constants.ImagePromo.Body_FieldId, SitecoreFieldType.MultiLineText, "Promo Banner")]
        string Body { get; set; }

        [SitecoreField(Constants.ImagePromo.FirstImage_FieldId, SitecoreFieldType.Image, "Promo Banner")]
        Image FirstImage { get; set; }

        [SitecoreField(Constants.ImagePromo.SecondImage_FieldId, SitecoreFieldType.Image, "Promo Banner")]
        Image SecondImage { get; set; }

        [SitecoreField(Constants.ImagePromo.ThirdImage_FieldId, SitecoreFieldType.Image, "Promo Banner")]
        Image ThirdImage { get; set; }

        [SitecoreField(Constants.ImagePromo.FourthImage_FieldId, SitecoreFieldType.Image, "Promo Banner")]
        Image FourthImage { get; set; }

        [SitecoreField(Constants.ImagePromo.PrimaryCTAGoal_FieldId, SitecoreFieldType.Droplink, "Promo Banner")]
        Guid PrimaryCTAGoal { get; set; }

        [SitecoreField(Constants.ImagePromo.PrimaryCTALink_FieldId, SitecoreFieldType.GeneralLink, "Promo Banner")]
        Link PrimaryCTALink { get; set; }

        [SitecoreField(Constants.ImagePromo.SecondaryCTAGoal_FieldId, SitecoreFieldType.Droplink, "Promo Banner")]
        Guid SecondaryCTAGoal { get; set; }

        [SitecoreField(Constants.ImagePromo.SecondaryCTALink_FieldId, SitecoreFieldType.GeneralLink, "Promo Banner")]
        Link SecondaryCTALink { get; set; }

        [SitecoreField(Constants.ImagePromo.HideFirstImage_FieldId, SitecoreFieldType.Checkbox, "Design")]
        bool HideFirstImage { get; set; }

        [SitecoreField(Constants.ImagePromo.HideSecondImage_FieldId, SitecoreFieldType.Checkbox, "Design")]
        bool HideSecondImage { get; set; }

        [SitecoreField(Constants.ImagePromo.HideThirdImage_FieldId, SitecoreFieldType.Checkbox, "Design")]
        bool HideThirdImage { get; set; }

        [SitecoreField(Constants.ImagePromo.HideFourthImage_FieldId, SitecoreFieldType.Checkbox, "Design")]
        bool HideFourthImage { get; set; }

        [SitecoreField(Constants.ImagePromo.ShowPrimaryCTA_FieldId, SitecoreFieldType.Checkbox, "Design")]
        bool ShowPrimaryCTA { get; set; }

        [SitecoreField(Constants.ImagePromo.ShowSecondaryCTA_FieldId, SitecoreFieldType.Checkbox, "Design")]
        bool ShowSecondaryCTA { get; set; }

        [SitecoreField(Constants.ImagePromo.TextAlignment_FieldId, SitecoreFieldType.Droplink, "Design")]
        IPromoLookup TextAlignment { get; set; }
    }
}