namespace LionTrust.Feature.Banner.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IStyleDepthValue : IBannerGlassBase
    {
        [SitecoreField(Constants.StyleDepthValue.Value_FieldId, SitecoreFieldType.SingleLineText, "StyleDepthValue")]
        string Value { get; set; }
    }
}