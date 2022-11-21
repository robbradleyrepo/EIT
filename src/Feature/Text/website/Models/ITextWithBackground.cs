namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
   
    public interface ITextWithBackground : ITextGlassBase
    {
        [SitecoreField(Text.Constants.TextWithBackground.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Text.Constants.TextWithBackground.Subtitle_FieldId)]
        string Subtitle { get; set; }

        [SitecoreField(Text.Constants.TextWithBackground.Body_FieldId)]
        string Body { get; set; }

        [SitecoreField(Text.Constants.TextWithBackground.BackgroundImage_FieldId)]
        Image BackgroundImage { get; set; }
    }
}