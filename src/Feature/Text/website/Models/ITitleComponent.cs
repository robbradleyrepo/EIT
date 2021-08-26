namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Design;
   
    public interface ITitleComponent : ITextGlassBase
    {
        [SitecoreField(Text.Constants.TitleComponent.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Text.Constants.TitleComponent.HeadingType_FieldId)]
        ILookupValue HeadingType { get; set; }

        [SitecoreField(Text.Constants.TitleComponent.TextAlign_FieldId)]
        ILookupValue TextAlignment { get; set; }

        [SitecoreField(Text.Constants.TitleComponent.TextColor_FieldId)]
        ILookupValue TextColor { get; set; }

        [SitecoreField(Text.Constants.TitleComponent.MarginsContainer_FieldId)]
        ILookupValue MarginsContainer { get; set; }
    }
}