namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface ITestimonial : ITextGlassBase
    {
        [SitecoreField(Constants.Testimonial.Author_FieldId, SitecoreFieldType.SingleLineText, "Content")]
        string Author { get; set; }
        [SitecoreField(Constants.Testimonial.Quote_FieldId, SitecoreFieldType.MultiLineText, "Content")]
        string Quote { get; set; }
        [SitecoreField(Constants.Testimonial.BackgroundImage_FieldId, SitecoreFieldType.Image, "Content")]
        Image BackgroundImage { get; set; }
    }
}