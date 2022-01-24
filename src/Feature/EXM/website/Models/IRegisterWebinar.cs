namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IRegisterWebinar : IExmGlassBase
    {
        [SitecoreField(Constants.RegisterWebinar.Title_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.RegisterWebinar.Subtitle_FieldID)]
        string Subtitle { get; set; }

        [SitecoreField(Constants.RegisterWebinar.Speakers_FieldID)]
        string Speakers { get; set; }

        [SitecoreField(Constants.RegisterWebinar.PrimaryCtaLink_FieldID)]
        Link PrimaryCtaLink { get; set; }

        [SitecoreField(Constants.RegisterWebinar.PrimaryCtaText_FieldID)]
        string PrimaryCtaText { get; set; }

        [SitecoreField(Constants.RegisterWebinar.SecondaryCtaLink_FieldID)]
        Link SecondaryCtaLink { get; set; }

        [SitecoreField(Constants.RegisterWebinar.SecondaryCtaText_FieldID)]
        string SecondaryCtaText { get; set; }
    }
}