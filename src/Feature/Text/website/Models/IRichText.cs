namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IRichText : ITextGlassBase
    {
        [SitecoreField(Constants.RichText.Text_FieldId, SitecoreFieldType.RichText, "Content")]
        string Text { get; set; }

        [SitecoreField(Constants.RichText.SmallSize_FieldId, SitecoreFieldType.Checkbox, "Content")]
        bool SmallSize { get; set; }

        [SitecoreField(Constants.RichText.TextColor_FieldId, SitecoreFieldType.Droplink, "Content")]
        Foundation.Design.ILookupValue TextColor { get; set; }
    }
}