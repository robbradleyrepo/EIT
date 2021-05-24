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
               
        [SitecoreField(Constants.HomePageCarousel.NumberOfItems_FieldId, SitecoreFieldType.Integer, "Homepage Carousel")]
        int NumberOfItems { get; set; }
    }
}