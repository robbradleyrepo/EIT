namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IHeader : IExmGlassBase
    {
        [SitecoreField(Constants.Header.Text_FieldID)]
        string Text { get; set; }
    }
}