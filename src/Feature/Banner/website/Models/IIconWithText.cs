namespace LionTrust.Feature.Banner.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;    

    public interface IIconWithText : IBannerGlassBase
    {
        [SitecoreField(Constants.IconWithText.Text_FieldId, SitecoreFieldType.SingleLineText, "Content")]
        string Text { get; set; }

        [SitecoreField(Constants.IconWithText.Icon_FieldId, SitecoreFieldType.Image, "Content")]
        Image Icon { get; set; }
    }
}