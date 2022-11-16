namespace LionTrust.Feature.Banner.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IBannerWithSlider : IBannerGlassBase
    {
        [SitecoreField(Constants.BannerWithSlider.Heading_FieldId, SitecoreFieldType.SingleLineText, "Content")]
        string Heading { get; set; }

        [SitecoreField(Constants.BannerWithSlider.BackgroundImage_FieldId, SitecoreFieldType.SingleLineText, "Content")]
        Image BackgroundImage { get; set; }

        [SitecoreChildren]
        IEnumerable<IImageWithTitleAndText> Images { get; set; }
    }
}