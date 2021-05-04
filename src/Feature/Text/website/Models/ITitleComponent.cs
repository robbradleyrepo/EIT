namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ITitleComponent : ITextGlassBase
    {
        [SitecoreField(Constants.TitleComponent.Title_FieldId, SitecoreFieldType.SingleLineText, "Content")]
        string Title { get; set; }

        [SitecoreField(Constants.TitleComponent.HeadingType_FieldId, SitecoreFieldType.Droplink, "Content")]
        Foundation.Design.ILookupValue HeadingType { get; set; }

        [SitecoreField(Constants.TitleComponent.TextAlign_FieldId, SitecoreFieldType.Droplink, "Content")]
        Foundation.Design.ILookupValue TextAlignment { get; set; }

        [SitecoreField(Constants.TitleComponent.TextColor_FieldId, SitecoreFieldType.Droplink, "Content")]
        Foundation.Design.ILookupValue TextColor { get; set; }
    }
}