namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IWebinar : IExmGlassBase
    {
        [SitecoreField(Constants.Webinar.Title_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.Webinar.Date_FieldID)]
        string DateText { get; set; }

        [SitecoreField(Constants.Webinar.Time_FieldID)]
        string TimeText { get; set; }

        [SitecoreField(Constants.Webinar.Duration_FieldID)]
        string DurationText { get; set; }

        [SitecoreField(Constants.Webinar.Speakers_FieldID)]
        string Speakers { get; set; }

        [SitecoreField(Constants.Webinar.Image_FieldID)]
        Image Image { get; set; }

        [SitecoreField(Constants.Webinar.Link_FieldID)]
        Link Link { get; set; }
    }
}