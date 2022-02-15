namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IIFrameModule : ITextGlassBase
    {
        [SitecoreField(Constants.IFrameModule.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.IFrameModule.IFrameUrl_FieldId)]
        string IFrameUrl { get; set; }

        [SitecoreField(Constants.IFrameModule.Height_FieldId)]
        string Height { get; set; }

        [SitecoreField(Constants.IFrameModule.Width_FieldId)]
        string Width { get; set; }
    }
}