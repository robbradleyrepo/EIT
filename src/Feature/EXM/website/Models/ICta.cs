namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface ICta : IExmGlassBase
    {
        [SitecoreField(Constants.Cta.Cta_FieldID)]
        Link Cta { get; set; }
    }
}