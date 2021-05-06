namespace LionTrust.Feature.Banner.Models
{
    using System;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IBannerWithSlider : IBannerGlassBase
    {
        [SitecoreField(Constants.BannerWithSlider.Heading_FieldId, SitecoreFieldType.SingleLineText, "Content")]
        string Heading { get; set; }

        [SitecoreChildren]
        IEnumerable<IImageWithTitleAndText> Images { get; set; }
    }
}