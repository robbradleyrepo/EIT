namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IRichText : ITextGlassBase
    {
        [SitecoreField(Constants.RichText.Text_FieldId)]
        string Text { get; set; }

        [SitecoreField(Constants.RichText.MarginsContainer_FieldId)]
        Foundation.Design.ILookupValue MarginsContainer { get; set; }

        [SitecoreField(Constants.RichText.TextColor_FieldId)]
        Foundation.Design.ILookupValue TextColor { get; set; }
    }
}