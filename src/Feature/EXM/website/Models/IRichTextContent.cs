namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IRichTextContent : IExmGlassBase
    {
        [SitecoreField(Constants.RichTextContent.Text_FieldID)]
        string Text { get; set; }
    }
}