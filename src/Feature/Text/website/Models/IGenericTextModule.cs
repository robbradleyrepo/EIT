namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IGenericTextModule : ITextGlassBase
    {
        [SitecoreField(Constants.GenericTextModule.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.GenericTextModule.Text_FieldId)]
        string Text { get; set; }
    }
}