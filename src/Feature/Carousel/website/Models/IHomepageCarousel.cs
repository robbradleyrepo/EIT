namespace LionTrust.Feature.Carousel.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Feature.Carousel;
    using System;
    using System.Collections.Generic;

    public interface IHomepageCarousel : ICarouselGlassBase
    {
        [SitecoreChildren]
        IEnumerable<ICarousel> CarouselCards { get; set; }

        [SitecoreField(Constants.HomePageCarousel.Heading_FieldId, SitecoreFieldType.SingleLineText, "Homepage Carousel")]
        string Heading { get; set; }

        [SitecoreField(Constants.HomePageCarousel.Body_FieldId, SitecoreFieldType.MultiLineText, "Homepage Carousel")]
        string Body { get; set; }

        [SitecoreField(Constants.HomePageCarousel.PrimaryCTALink_FieldId, SitecoreFieldType.GeneralLink, "Homepage Carousel")]
        Link PrimaryCTALink { get; set; }

        [SitecoreField(Constants.HomePageCarousel.PrimaryCTAGoal_FieldId, SitecoreFieldType.Droplink, "Homepage Carousel")]
        Guid PrimaryCTAGoal { get; set; }

        [SitecoreField(Constants.HomePageCarousel.PrimaryCTALabel_FieldId, SitecoreFieldType.SingleLineText, "Homepage Carousel")]
        string PrimaryCTALabel { get; set; }

        [SitecoreField(Constants.HomePageCarousel.SecondaryCTALink_FieldId, SitecoreFieldType.GeneralLink, "Homepage Carousel")]
        Link SecondaryCTALink { get; set; }

        [SitecoreField(Constants.HomePageCarousel.SecondaryCTAGoal_FieldId, SitecoreFieldType.Droplink, "Homepage Carousel")]
        Guid SecondaryCTAGoal { get; set; }

        [SitecoreField(Constants.HomePageCarousel.SecondaryCTALabel_FieldId, SitecoreFieldType.SingleLineText, "Homepage Carousel")]
        string SecondaryCTALabel { get; set; }

        [SitecoreField(Constants.HomePageCarousel.NumberOfItems_FieldId, SitecoreFieldType.Integer, "Homepage Carousel")]
        int NumberOfItems { get; set; }
    }
}