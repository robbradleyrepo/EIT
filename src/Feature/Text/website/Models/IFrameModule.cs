namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IIFrameModule : ITextGlassBase
    {
        [SitecoreField(Constants.IFrameModule.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.IFrameModule.IFrameUrl_FieldId)]
        string IFrameUrl { get; set; }

        [SitecoreField(Constants.IFrameModule.UniqueId_FieldId)]
        string UniqueId { get; set; }       
    }
}