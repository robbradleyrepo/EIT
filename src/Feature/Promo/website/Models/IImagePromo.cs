namespace LionTrust.Feature.Promo.Models
{
    using System;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
  
    public interface IImagePromo : IPromoGlassBase
    {
        [SitecoreField(Constants.ImagePromo.Heading_FieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.ImagePromo.Body_FieldId)]
        string Body { get; set; }

        [SitecoreField(Constants.ImagePromo.FirstImage_FieldId)]
        Image FirstImage { get; set; }

        [SitecoreField(Constants.ImagePromo.SecondImage_FieldId)]
        Image SecondImage { get; set; }

        [SitecoreField(Constants.ImagePromo.ThirdImage_FieldId)]
        Image ThirdImage { get; set; }

        [SitecoreField(Constants.ImagePromo.FourthImage_FieldId)]
        Image FourthImage { get; set; }

        [SitecoreField(Constants.ImagePromo.PrimaryCTAGoal_FieldId)]
        Guid PrimaryCTAGoal { get; set; }

        [SitecoreField(Constants.ImagePromo.PrimaryCTALink_FieldId)]
        Link PrimaryCTALink { get; set; }

        [SitecoreField(Constants.ImagePromo.SecondaryCTAGoal_FieldId)]
        Guid SecondaryCTAGoal { get; set; }

        [SitecoreField(Constants.ImagePromo.SecondaryCTALink_FieldId)]
        Link SecondaryCTALink { get; set; }

        [SitecoreField(Constants.ImagePromo.HideFirstImage_FieldId)]
        bool HideFirstImage { get; set; }

        [SitecoreField(Constants.ImagePromo.HideSecondImage_FieldId)]
        bool HideSecondImage { get; set; }

        [SitecoreField(Constants.ImagePromo.HideThirdImage_FieldId)]
        bool HideThirdImage { get; set; }

        [SitecoreField(Constants.ImagePromo.HideFourthImage_FieldId)]
        bool HideFourthImage { get; set; }

        [SitecoreField(Constants.ImagePromo.ShowPrimaryCTA_FieldId)]
        bool ShowPrimaryCTA { get; set; }

        [SitecoreField(Constants.ImagePromo.ShowSecondaryCTA_FieldId)]
        bool ShowSecondaryCTA { get; set; }

        [SitecoreField(Constants.ImagePromo.TextAlignment_FieldId)]
        IPromoLookup TextAlignment { get; set; }

        [SitecoreField(Constants.ImagePromo.TextColour_FieldId)]
        Foundation.Design.ILookupValue TextColour { get; set; }

        [SitecoreField(Constants.ImagePromo.TitleColor_FieldId)]
        Foundation.Design.ILookupValue TitleColor { get; set; }
    }
}