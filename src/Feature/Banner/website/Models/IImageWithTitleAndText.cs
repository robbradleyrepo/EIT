namespace LionTrust.Feature.Banner.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IImageWithTitleAndText : IBannerGlassBase
    {
        [SitecoreField(Constants.ImageWithTitleAndText.Title_FieldId, SitecoreFieldType.SingleLineText, "Content")]
        string Title { get; set; }

        [SitecoreField(Constants.ImageWithTitleAndText.Text_FieldId, SitecoreFieldType.SingleLineText, "Content")]
        string Text { get; set; }

        [SitecoreField(Constants.ImageWithTitleAndText.Image_FieldId, SitecoreFieldType.Image, "Content")]
        Image Image { get; set; }
    }
}