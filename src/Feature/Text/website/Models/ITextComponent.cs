namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ITextComponent : ITextGlassBase
    {
        [SitecoreField(Constants.TextComponent.Text_FieldId, SitecoreFieldType.RichText, "Content")]
        string Text { get; set; }

        [SitecoreField(Constants.TextComponent.SmallSize_FieldId, SitecoreFieldType.Checkbox, "Content")]
        bool SmallSize { get; set; }

        [SitecoreField(Constants.TextComponent.TextColor_FieldId, SitecoreFieldType.Droplink, "Content")]
        Foundation.Design.ILookupValue TextColor { get; set; }
    }
}