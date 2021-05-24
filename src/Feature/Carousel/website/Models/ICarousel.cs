namespace LionTrust.Feature.Carousel.Models
{
    using System;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Feature.Carousel;    

    public interface ICarousel : ICarouselGlassBase
    {
        [SitecoreField(Constants.CarouselCard.AuthorImage_FieldId, SitecoreFieldType.DropTree, "Carousel Card")]
        Image AuthorImage { get; set; }

        [SitecoreField(Constants.CarouselCard.AuthorName_FieldId, SitecoreFieldType.DropTree, "Carousel Card")]
        string AuthorName { get; set; }

        [SitecoreField(Constants.CarouselCard.Heading_FieldId, SitecoreFieldType.SingleLineText, "Carousel Card")]
        string Heading { get; set; }        

        [SitecoreField(Constants.CarouselCard.CTALink_FieldId, SitecoreFieldType.GeneralLink, "Carousel Card")]
        Link CTALink { get; set; }

        [SitecoreField(Constants.CarouselCard.CTAGoal_FieldId, SitecoreFieldType.Droplink, "Carousel Card")]
        Guid CTAGoal { get; set; }

        [SitecoreField(Constants.CarouselCard.Image_FieldId, SitecoreFieldType.Image, "Carousel Card")]
        Image Image { get; set; }

        [SitecoreField(Constants.CarouselCard.CTAIsVideo_FieldId, SitecoreFieldType.Checkbox, "Carousel Card")]
        bool CTAIsVideo { get; set; }
    }
}