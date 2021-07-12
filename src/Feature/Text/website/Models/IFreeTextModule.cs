namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFreeTextModule : ITextGlassBase
    {
        [SitecoreField(Constants.FreeTextModule.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.FreeTextModule.Body_FieldId)]
        string Body { get; set; }
    }
}